using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace LolSpellOverlay
{
    public class SummonerAction : INotifyPropertyChanged
    {
        private MediaPlayer player;

        public SummonerAction()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sounds", "notificationSound.wav");

            player = new MediaPlayer();
            player.Open(new Uri(path));
            player.Volume = 0.15;
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

        private int initialCooldown;

        public int InitialCooldown
        {
            get { return initialCooldown; }
            set
            {
                initialCooldown = value;
                OnPropertyChanged();
            }
        }

        private bool isOnCooldown;

        public bool IsOnCooldown
        {
            get { return isOnCooldown; }
            set { isOnCooldown = value; }
        }

        private bool isItem;

        public bool IsItem
        {
            get { return isItem; }
            set { isItem = value; }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        { 
            if (RemainingCooldown == 20)
            {
                player.Stop();
                player.Play();
            }

            if (RemainingCooldown > 0 && RemainingCooldown - 1 > 0)
            {
                RemainingCooldown--;
                OnPropertyChanged(nameof(CooldownDisplay));
            }
            else
            {
                ResetCooldown();
            }
        }

        public void StartCooldown()
        {
            RemainingCooldown = this.InitialCooldown;
            OnPropertyChanged(nameof(CooldownDisplay));
            IsOnCooldown = true;
            timer.Start();
        }

        public void ResetCooldown()
        {
            timer.Stop();
            RemainingCooldown = null;
            IsOnCooldown = false;
            OnManualCooldownUpdate();
        }

        public void OnManualCooldownUpdate()
        {
            OnPropertyChanged(nameof(RemainingCooldown));
            OnPropertyChanged(nameof(CooldownDisplay));
        }

        public string? CooldownDisplay => RemainingCooldown == 0 ? null : (RemainingCooldown / 60 > 0 ? $"{RemainingCooldown / 60}:{RemainingCooldown % 60:D2}" : $"{RemainingCooldown % 60}");

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
