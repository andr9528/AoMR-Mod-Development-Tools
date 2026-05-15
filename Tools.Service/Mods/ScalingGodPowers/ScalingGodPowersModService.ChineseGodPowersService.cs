using Tools.Abstraction.Enum;

namespace Tools.Service.Mods.ScalingGodPowers;

public partial class ScalingGodPowersModService
{
    private class ChineseGodPowersService
    {
        private static readonly HashSet<GodPowerName> GodPowers =
        [
            GodPowerName.THE_PEACH_BLOSSOM_SPRING, // ThePeachBlossomSpring
            GodPowerName.CREATION, // Creation
            GodPowerName.PROSPEROUS_SEEDS, // ProsperousSeeds
            GodPowerName.LIGHTNING_WEAPONS, // LightningWeapons
            GodPowerName.EARTH_WALL, // EarthWall
            GodPowerName.VANISH, // Vanish
            GodPowerName.FOREST_PROTECTION, // ForestProtection
            GodPowerName.DROUGHT_LAND, // DroughtLand
            GodPowerName.VENOM_BEAST, // VenomBeast
            GodPowerName.GREAT_FLOOD, // GreatFlood
            GodPowerName.BLAZING_PRAIRIE, // BlazingPrairie
            GodPowerName.YINGLONGS_WRATH, // YinglongsWrath
        ];

        public void AddScalingData()
        {
            foreach (GodPowerName godPower in GodPowers)
            {
                AddScalingData(godPower);
            }
        }

        private void AddScalingData(GodPowerName godPower)
        {
            switch (godPower)
            {
                case GodPowerName.THE_PEACH_BLOSSOM_SPRING:
                    AddThePeachBlossomSpringScalingData();
                    break;
                case GodPowerName.CREATION:
                    AddCreationScalingData();
                    break;
                case GodPowerName.PROSPEROUS_SEEDS:
                    AddProsperousSeedsScalingData();
                    break;
                case GodPowerName.LIGHTNING_WEAPONS:
                    AddLightningWeaponsScalingData();
                    break;
                case GodPowerName.EARTH_WALL:
                    AddEarthWallScalingData();
                    break;
                case GodPowerName.VANISH:
                    AddVanishScalingData();
                    break;
                case GodPowerName.FOREST_PROTECTION:
                    AddForestProtectionScalingData();
                    break;
                case GodPowerName.DROUGHT_LAND:
                    AddDroughtLandScalingData();
                    break;
                case GodPowerName.VENOM_BEAST:
                    AddVenomBeastScalingData();
                    break;
                case GodPowerName.GREAT_FLOOD:
                    AddGreatFloodScalingData();
                    break;
                case GodPowerName.BLAZING_PRAIRIE:
                    AddBlazingPrairieScalingData();
                    break;
                case GodPowerName.YINGLONGS_WRATH:
                    AddYinglongsWrathScalingData();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddThePeachBlossomSpringScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddCreationScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddProsperousSeedsScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddLightningWeaponsScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddEarthWallScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddVanishScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddForestProtectionScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddDroughtLandScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddVenomBeastScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddGreatFloodScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddBlazingPrairieScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddYinglongsWrathScalingData()
        {
        }
    }
}
