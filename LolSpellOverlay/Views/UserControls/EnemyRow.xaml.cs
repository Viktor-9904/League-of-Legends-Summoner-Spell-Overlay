using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace LolSpellOverlay.Views.UserControls
{
    public partial class EnemyRow : UserControl
    {
        public EnemyRow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public static readonly DependencyProperty LaneProperty =
            DependencyProperty.Register(
                nameof(Lane),
                typeof(string),
                typeof(EnemyRow),
                new PropertyMetadata(string.Empty, OnInitialLaneIcon));

        public static readonly DependencyProperty Spell_D_Property =
            DependencyProperty.Register(
                nameof(Spell_D),
                typeof(string),
                typeof(EnemyRow),
                new PropertyMetadata(string.Empty, OnInitialSpell_D));

        public static readonly DependencyProperty Spell_F_Property =
            DependencyProperty.Register(
                nameof(Spell_F),
                typeof(string),
                typeof(EnemyRow),
                new PropertyMetadata(string.Empty, OnInitialSpell_F));

        public string Lane
        {
            get => (string)GetValue(LaneProperty);
            set => SetValue(LaneProperty, value);
        }

        public string Spell_D
        {
            get => (string)GetValue(Spell_D_Property);
            set => SetValue(Spell_D_Property, value);
        }

        public string Spell_F
        {
            get => (string)GetValue(Spell_F_Property);
            set => SetValue(Spell_F_Property, value);
        }

        private static void OnInitialLaneIcon(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (EnemyRow)d;
            var laneName = (string)e.NewValue;

            if (!string.IsNullOrEmpty(laneName))
            {
                control.LaneImage = new BitmapImage(
                    new Uri(
                        $"pack://application:,,,/LolSpellOverlay;component/Icons/Lanes/{laneName}.png",
                        UriKind.Absolute));
            }
        }

        private static void OnInitialSpell_D(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (EnemyRow)d;
            var spellName = (string)e.NewValue;

            if (!string.IsNullOrEmpty(spellName))
            {
                control.Spell_D = spellName;
            }
        }

        private static void OnInitialSpell_F(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (EnemyRow)d;
            var spellName = (string)e.NewValue;

            if (!string.IsNullOrEmpty(spellName))
            {
                control.Spell_F = spellName;
            }
        }

        private void MainImage_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                DropPopup.IsOpen = true;
            }
        }

        public BitmapImage LaneImage { get; set; }
    }
}
