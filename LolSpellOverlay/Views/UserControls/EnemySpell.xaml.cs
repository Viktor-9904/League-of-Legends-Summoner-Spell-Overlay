using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using static LolSpellOverlay.Constants;

namespace LolSpellOverlay.Views.UserControls
{
    /// <summary>
    /// Interaction logic for EnemySpell.xaml
    /// </summary>
    public partial class EnemySpell : UserControl, INotifyPropertyChanged
    {
        public EnemySpell()
        {
            InitializeComponent();
            DataContext = this;
        }

        public static readonly DependencyProperty SpellNameProperty =
            DependencyProperty.Register(
                nameof(SpellName),
                typeof(string),
                typeof(EnemySpell),
                new PropertyMetadata(string.Empty, OnSpellNameChanged));

        public string SpellName
        {
            get => (string)GetValue(SpellNameProperty);
            set => SetValue(SpellNameProperty, value);
        }

        private static void OnSpellNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (EnemySpell)d;
            var spellName = (string)e.NewValue;

            if (string.IsNullOrWhiteSpace(spellName))
                return;

            var currentSpell = SummonerSpells.All.FirstOrDefault(s => s.Name == spellName);

            bool isSpellNameValid = IsSpellNameValid(currentSpell?.Name, out SummonerActionData newSpell);

            if (!isSpellNameValid)
                return;

            Uri uri = new Uri(
                currentSpell!.Icon,
                UriKind.Absolute);

            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = uri;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.EndInit();
            image.Freeze();

            control.Spell = new SummonerAction
            {
                Name = currentSpell.Name,
                Icon = image,
                RemainingCooldown = null,
                InitialCooldown = currentSpell.Cooldown,
                IsOnCooldown = false,
            };

            control.OnPropertyChanged(nameof(Spell));
        }

        public SummonerAction Spell { get; private set; }

        private void Spell_LeftClick(object sender, MouseButtonEventArgs e)
        {
            int summonerSpellHaste = 0;
            int summonerItemHaste = 0;

            bool cosmicInsightActive = false;
            bool ionianBootsActive = false;

            var parent = FindParent<EnemyRow>(this);
            if (parent != null)
            {
                cosmicInsightActive = parent.CosmicInsightActive;
                ionianBootsActive = parent.IonianBootsActive;
            }

            if (cosmicInsightActive)
            {
                summonerSpellHaste += Runes.CosmicInsightSpellHaste;
                summonerItemHaste += Runes.CosmicInsightItemHaste;
            }

            if (ionianBootsActive)
            {
                summonerSpellHaste += Items.IonianBootsOfLuciditySpellHaste;
            }

            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                var nextSpell = SummonerSpells.All.Find(SummonerSpells.All.FirstOrDefault(s => s.Name == Spell.Name)!)!.Next?.Value
                    ?? SummonerSpells.All.First!.Value;

                Spell.ResetCooldown();
                Spell.Name = nextSpell.Name;
                Spell.Icon = new BitmapImage(new Uri(nextSpell.Icon, UriKind.Absolute));
                Spell.InitialCooldown = nextSpell.Cooldown;
                Spell.IsItem = nextSpell.IsItem;
                return;
            }

            if (Spell == null)
                return;

            bool isSpellNameValid = IsSpellNameValid(Spell.Name, out SummonerActionData newSpell);

            if (!isSpellNameValid)
                return;

            if (!Spell.IsOnCooldown && Spell.RemainingCooldown == null)
            {
                Spell.IsOnCooldown = true;
                Spell.StartCooldown();
                Spell.RemainingCooldown -= ReducedSpellCooldown(Spell.IsItem, Spell.InitialCooldown, summonerSpellHaste, summonerItemHaste);
                Spell.OnManualCooldownUpdate();
            }
            else if (Spell.RemainingCooldown - 1 > 0)
            {
                Spell.RemainingCooldown = Spell.RemainingCooldown - 1;
                Spell.OnManualCooldownUpdate();
            }
        }

        private static bool IsSpellNameValid(string? name, out SummonerActionData newSpell)
        {
            newSpell = null;

            if (string.IsNullOrEmpty(name))
                return false;

            foreach (var spell in SummonerSpells.All)
            {
                if (spell == null)
                    return false;

                if (spell.Name == name)
                {
                    newSpell = spell;
                    return true;
                }
            }

            return false;
        }

        private void Spell_RightClick(object sender, MouseButtonEventArgs e)
        {
            int summonerSpellHaste = 0;
            int summonerItemHaste = 0;

            bool cosmicInsightActive = false;
            bool ionianBootsActive = false;

            var parent = FindParent<EnemyRow>(this);
            if (parent != null)
            {
                cosmicInsightActive = parent.CosmicInsightActive;
                ionianBootsActive = parent.IonianBootsActive;
            }

            if (cosmicInsightActive)
            {
                summonerSpellHaste += Runes.CosmicInsightSpellHaste;
                summonerItemHaste += Runes.CosmicInsightItemHaste;
            }

            if (ionianBootsActive)
            {
                summonerSpellHaste += Items.IonianBootsOfLuciditySpellHaste;
            }

            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                var nextSpell = SummonerSpells.All.Find(SummonerSpells.All.FirstOrDefault(s => s.Name == Spell.Name)!)!.Previous?.Value
                    ?? SummonerSpells.All.Last!.Value;

                Spell.Name = nextSpell.Name;
                Spell.Icon = new BitmapImage(new Uri(nextSpell.Icon, UriKind.Absolute));
                Spell.InitialCooldown = nextSpell.Cooldown;
                Spell.ResetCooldown();
                return;
            }

            Spell.ResetCooldown();
        }

        private void Spell_Scroll(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta < 0 && Spell.RemainingCooldown > 0)
            {
                Spell_LeftClick(sender, null);
            }

            if (e.Delta > 0 && Spell.RemainingCooldown > 0 && Spell.RemainingCooldown < Spell.InitialCooldown)
            {
                Spell.RemainingCooldown++;
                Spell.OnManualCooldownUpdate();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public static T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject current = child;
            while (current != null)
            {
                if (current is T parent)
                    return parent;
                current = VisualTreeHelper.GetParent(current);
            }
            return null;
        }
        private int ReducedSpellCooldown(bool isItem, int baseCooldown, int spellHaste, int itemHaste)
        {
            int result;

            if (isItem)
            {
                result = (int)Math.Round(baseCooldown - ((double)baseCooldown * 100 / (100 + itemHaste)));
            }
            else
            {
                result = (int)Math.Round(baseCooldown - ((double)baseCooldown * 100 / (100 + spellHaste)));
            }

            return result;
        }
    }
}
