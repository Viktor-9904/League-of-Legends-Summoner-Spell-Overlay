using System.Security.RightsManagement;

namespace LolSpellOverlay
{
    public static class Constants
    {
        public class SummonerActionData
        {
            public string Name { get; set; }
            public string Icon { get; set; }
            public int Cooldown { get; set; }
            public bool IsItem { get; set; }

            public SummonerActionData(string name, string icon, int cooldown, bool isItem)
            {
                Name = name;
                Icon = icon;
                Cooldown = cooldown;
                IsItem = isItem;
            }
        }

        public static class SummonerSpells
        {
            public static readonly LinkedList<SummonerActionData> All = new(new[]
            {
                new SummonerActionData(
                    "Flash",
                    "pack://application:,,,/LolSpellOverlay;component/Icons/SummonerSpells/Flash.png",
                    300,
                    false),


                new SummonerActionData(
                    "Barrier",
                    "pack://application:,,,/LolSpellOverlay;component/Icons/SummonerSpells/Barrier.png",
                    180,
                    false),

                new SummonerActionData(
                    "Heal",
                    "pack://application:,,,/LolSpellOverlay;component/Icons/SummonerSpells/Heal.png",
                    240,
                    false),

                new SummonerActionData(
                    "Exhaust",
                    "pack://application:,,,/LolSpellOverlay;component/Icons/SummonerSpells/Exhaust.png",
                    240,
                    false),

                new SummonerActionData(
                    "Cleanse",
                    "pack://application:,,,/LolSpellOverlay;component/Icons/SummonerSpells/Cleanse.png",
                    240,
                    false),

                new SummonerActionData(
                    "Teleport",
                    "pack://application:,,,/LolSpellOverlay;component/Icons/SummonerSpells/Teleport.png",
                    300,
                    false),

                //new SummonerSpellData(
                //    "UnleashedTeleport",
                //    "pack://application:,,,/LolSpellOverlay;component/Icons/SummonerSpells/UnleashedTeleport.png",
                //    260,
                //    false),

                new SummonerActionData(
                    "Ignite",
                    "pack://application:,,,/LolSpellOverlay;component/Icons/SummonerSpells/Ignite.png",
                    180,
                    false),

                new SummonerActionData(
                    "Ghost",
                    "pack://application:,,,/LolSpellOverlay;component/Icons/SummonerSpells/Ghost.png",
                    240,
                    false),

                new SummonerActionData(
                    "Smite",
                    "pack://application:,,,/LolSpellOverlay;component/Icons/SummonerSpells/Smite.png",
                    90,
                    false),

                new SummonerActionData(
                    "TopQuestTeleport",
                    "pack://application:,,,/LolSpellOverlay;component/Icons/SummonerSpells/TopQuestTeleport.png",
                    420,
                    false),

                new SummonerActionData(
                    "noIcon",
                    "pack://application:,,,/LolSpellOverlay;component/Icons/Items/noIcon.jpg",
                    0,
                    false),

                new SummonerActionData(
                    "GuardianAngel",
                    "pack://application:,,,/LolSpellOverlay;component/Icons/Items/GuardianAngel.png",
                    300,
                    true),

                new SummonerActionData(
                    "ZhonyaHourglass",
                    "pack://application:,,,/LolSpellOverlay;component/Icons/Items/ZhonyaHourglass.png",
                    120,
                    true),

                new SummonerActionData(
                    "QSS",
                    "pack://application:,,,/LolSpellOverlay;component/Icons/Items/QSS.png",
                    90,
                    true),

                new SummonerActionData(
                    "MawOfMalmortius",
                    "pack://application:,,,/LolSpellOverlay;component/Icons/Items/MawOfMalmortius.png",
                    90,
                    true),

                new SummonerActionData(
                    "MikaelsBlessing",
                    "pack://application:,,,/LolSpellOverlay;component/Icons/Items/MikaelsBlessing.png",
                    120,
                    true),

                new SummonerActionData(
                    "SeraphsEmbrace",
                    "pack://application:,,,/LolSpellOverlay;component/Icons/Items/SeraphsEmbrace.png",
                    90,
                    true),

                new SummonerActionData(
                    "ImmortalShieldbow",
                    "pack://application:,,,/LolSpellOverlay;component/Icons/Items/ImmortalShieldbow.png",
                    90,
                    true),

                new SummonerActionData(
                    "SteraksGage",
                    "pack://application:,,,/LolSpellOverlay;component/Icons/Items/SteraksGage.png",
                    90,
                    true),
            });
        }

        public static class Runes
        {
            public const int CosmicInsightSpellHaste = 18;
            public const int CosmicInsightItemHaste = 10;
        }

        public static class Items
        {
            public const int IonianBootsOfLuciditySpellHaste = 10;
        }
    }
}