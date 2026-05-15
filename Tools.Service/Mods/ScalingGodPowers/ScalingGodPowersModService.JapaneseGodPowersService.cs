using Microsoft.Extensions.Logging;
using Tools.Abstraction.Enum;
using Tools.Persistence;

namespace Tools.Service.Mods.ScalingGodPowers;

public partial class ScalingGodPowersModService
{
    private class JapaneseGodPowersService
    {
        private readonly ToolsDatabaseContext toolsDatabaseContext;
        private readonly ILogger<JapaneseGodPowersService> logger;

        public JapaneseGodPowersService(
            ToolsDatabaseContext toolsDatabaseContext, ILogger<JapaneseGodPowersService> logger)
        {
            this.toolsDatabaseContext = toolsDatabaseContext;
            this.logger = logger;
        }

        private static readonly HashSet<GodPowerName> GodPowers =
        [
            GodPowerName.SOLAR_SHIELD, // SolarShield
            GodPowerName.KUSANAGI, // Kusanagi
            GodPowerName.NEW_MOON, // NewMoon
            GodPowerName.SHRINE_OF_THE_HUNT, // ShrineOfTheHunt
            GodPowerName.GOSHINBOKU, // Goshinboku
            GodPowerName.SWAMPLAND, // Swampland
            GodPowerName.SHOGUN, // Shogun
            GodPowerName.SMITING_GUST, // SmitingGust
            GodPowerName.THUNDER_BURST, // ThunderBurst
            GodPowerName.SACRED_GATE, // SacredGate
            GodPowerName.DRAGON_TYPHOON, // DragonTyphoon
            GodPowerName.DIVINE_SLASH, // DivineSlash
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
                case GodPowerName.SOLAR_SHIELD:
                    AddSolarShieldScalingData();
                    break;
                case GodPowerName.KUSANAGI:
                    AddKusanagiScalingData();
                    break;
                case GodPowerName.NEW_MOON:
                    AddNewMoonScalingData();
                    break;
                case GodPowerName.SHRINE_OF_THE_HUNT:
                    AddShrineOfTheHuntScalingData();
                    break;
                case GodPowerName.GOSHINBOKU:
                    AddGoshinbokuScalingData();
                    break;
                case GodPowerName.SWAMPLAND:
                    AddSwamplandScalingData();
                    break;
                case GodPowerName.SHOGUN:
                    AddShogunScalingData();
                    break;
                case GodPowerName.SMITING_GUST:
                    AddSmitingGustScalingData();
                    break;
                case GodPowerName.THUNDER_BURST:
                    AddThunderBurstScalingData();
                    break;
                case GodPowerName.SACRED_GATE:
                    AddSacredGateScalingData();
                    break;
                case GodPowerName.DRAGON_TYPHOON:
                    AddDragonTyphoonScalingData();
                    break;
                case GodPowerName.DIVINE_SLASH:
                    AddDivineSlashScalingData();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddSolarShieldScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddKusanagiScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddNewMoonScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddShrineOfTheHuntScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddGoshinbokuScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddSwamplandScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddShogunScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddSmitingGustScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddThunderBurstScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddSacredGateScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddDragonTyphoonScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddDivineSlashScalingData()
        {
        }
    }
}
