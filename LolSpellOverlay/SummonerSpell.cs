using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace LolSpellOverlay
{
    public class SummonerSpell : INotifyPropertyChanged
    {
        public SummonerSpell()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        private string name = string.Empty;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage icon = new BitmapImage();
        public BitmapImage Icon
        {
            get => icon;
            set
            {
                icon = value;
                OnPropertyChanged();
            }
        }

        private DispatcherTimer timer;
        private int? remainingCooldown;

        public int? RemainingCooldown
        {
            get { return remainingCooldown; }
            set
            {
                remainingCooldown = value;
                OnPropertyChanged();
            }
        }

        private bool isOnCooldown;

        public bool IsOnCooldown
        {
            get { return isOnCooldown; }
            set { isOnCooldown = value; }
        }


        private void Timer_Tick(object? sender, EventArgs e)
        {
            if (RemainingCooldown > 0)
            {
                RemainingCooldown--;
                OnPropertyChanged(nameof(CooldownDisplay));
            }
            else
            {
                timer.Stop();
                RemainingCooldown = null;
                IsOnCooldown = false;
            }
        }

        public void StartCooldown(int seconds)
        {
            RemainingCooldown = seconds;
            OnPropertyChanged(nameof(CooldownDisplay));
            IsOnCooldown = true;
            timer.Start();
        }


        public void ResetCooldown()
        {
            timer.Stop();
            RemainingCooldown = 0;
            OnPropertyChanged(nameof(CooldownDisplay));
            IsOnCooldown = false;
        }

        public void OnManualCooldownUpdate()
        {
            OnPropertyChanged(nameof(RemainingCooldown));
            OnPropertyChanged(nameof(CooldownDisplay));
        }

        public string? CooldownDisplay => RemainingCooldown == 0 ? null : (RemainingCooldown / 60 > 0 ? $"{RemainingCooldown / 60}:{RemainingCooldown % 60:D2}" : $"{RemainingCooldown % 60}");

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
