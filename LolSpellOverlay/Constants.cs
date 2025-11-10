namespace LolSpellOverlay
{
    public static class Constants
    {
        public class SummonerSpellData
        {
            public string Name { get; set; }
            public string Icon { get; set; }
            public int Cooldown { get; set; }

            public SummonerSpellData(string name, string icon, int cooldown)
            {
                Name = name;
                Icon = icon;
                Cooldown = cooldown;
            }
        }

        public static class SummonerSpells
        {
            public static readonly LinkedList<SummonerSpellData> All = new(new[]
            {
                new SummonerSpellData(
                    "Flash",
                    "pack://application:,,,/LolSpellOverlay;component/Icons/SummonerSpells/Flash.png",
                    300),

                new SummonerSpellData(
                    "Heal",
                    "pack://application:,,,/LolSpellOverlay;component/Icons/SummonerSpells/Heal.png",
                    240),

                new SummonerSpellData(
                    "Exhaust",
                    "pack://application:,,,/LolSpellOverlay;component/Icons/SummonerSpells/Exhaust.png",
                    240),

                new SummonerSpellData(
                    "Barrier",
                    "pack://application:,,,/LolSpellOverlay;component/Icons/SummonerSpells/Barrier.png",
                    180),

                new SummonerSpellData(
                    "Cleanse",
                    "pack://application:,,,/LolSpellOverlay;component/Icons/SummonerSpells/Cleanse.png",
                    240),

                new SummonerSpellData(
                    "Teleport",
                    "pack://application:,,,/LolSpellOverlay;component/Icons/SummonerSpells/Teleport.png",
                    300),

                new SummonerSpellData(
                    "UnleashedTeleport",
                    "pack://application:,,,/LolSpellOverlay;component/Icons/SummonerSpells/UnleashedTeleport.png",
                    260),

                new SummonerSpellData(
                    "Ignite",
                    "pack://application:,,,/LolSpellOverlay;component/Icons/SummonerSpells/Ignite.png",
                    180),

                new SummonerSpellData(
                    "Ghost",
                    "pack://application:,,,/LolSpellOverlay;component/Icons/SummonerSpells/Ghost.png",
                    240),

                new SummonerSpellData(
                    "Smite",
                    "pack://application:,,,/LolSpellOverlay;component/Icons/SummonerSpells/Smite.png",
                    90)
            });
        }
    }
}