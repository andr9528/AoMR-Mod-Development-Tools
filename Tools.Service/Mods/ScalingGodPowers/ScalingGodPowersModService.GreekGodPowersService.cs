using Microsoft.Extensions.Logging;
using Tools.Abstraction.Enum;
using Tools.Persistence;

namespace Tools.Service.Mods.ScalingGodPowers;

public partial class ScalingGodPowersModService
{
    private class GreekGodPowersService
    {
        private readonly ToolsDatabaseContext toolsDatabaseContext;
        private readonly ILogger<GreekGodPowersService> logger;

        public GreekGodPowersService(ToolsDatabaseContext toolsDatabaseContext, ILogger<GreekGodPowersService> logger)
        {
            this.toolsDatabaseContext = toolsDatabaseContext;
            this.logger = logger;
        }

        private static readonly HashSet<GodPowerName> GodPowers =
        [
            GodPowerName.BOLT, // Bolt
            GodPowerName.SENTINEL, // Sentinel
            GodPowerName.LURE, // Lure
            GodPowerName.RESTORATION, // Restoration
            GodPowerName.CEASE_FIRE, // CeaseFire
            GodPowerName.PESTILENCE, // Pestilence
            GodPowerName.UNDERWORLD_INVASION, // UnderworldInvasion
            GodPowerName.BRONZE, // Bronze
            GodPowerName.CURSE, // Curse
            GodPowerName.UNDERWORLD_PASSAGE, // UnderworldPassage
            GodPowerName.PLENTY_VAULT, // PlentyVault
            GodPowerName.LIGHTNING_STORM, // LightningStorm
            GodPowerName.EARTHQUAKE, // Earthquake
            GodPowerName.WITHER, // Wither
            GodPowerName.ARCADIAN_MEADOW, // ArcadianMeadow
            GodPowerName.COMMUNAL_HEARTH, // CommunalHearth
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
                case GodPowerName.BOLT:
                    AddBoltScalingData();
                    break;
                case GodPowerName.SENTINEL:
                    AddSentinelScalingData();
                    break;
                case GodPowerName.LURE:
                    AddLureScalingData();
                    break;
                case GodPowerName.RESTORATION:
                    AddRestorationScalingData();
                    break;
                case GodPowerName.CEASE_FIRE:
                    AddCeaseFireScalingData();
                    break;
                case GodPowerName.PESTILENCE:
                    AddPestilenceScalingData();
                    break;
                case GodPowerName.UNDERWORLD_INVASION:
                    AddUnderworldInvasionScalingData();
                    break;
                case GodPowerName.BRONZE:
                    AddBronzeScalingData();
                    break;
                case GodPowerName.CURSE:
                    AddCurseScalingData();
                    break;
                case GodPowerName.UNDERWORLD_PASSAGE:
                    AddUnderworldPassageScalingData();
                    break;
                case GodPowerName.PLENTY_VAULT:
                    AddPlentyVaultScalingData();
                    break;
                case GodPowerName.LIGHTNING_STORM:
                    AddLightningStormScalingData();
                    break;
                case GodPowerName.EARTHQUAKE:
                    AddEarthquakeScalingData();
                    break;
                case GodPowerName.WITHER:
                    AddWitherScalingData();
                    break;
                case GodPowerName.ARCADIAN_MEADOW:
                    AddArcadianMeadowScalingData();
                    break;
                case GodPowerName.COMMUNAL_HEARTH:
                    AddCommunalHearthScalingData();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddBoltScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddSentinelScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddLureScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddRestorationScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddCeaseFireScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddPestilenceScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddUnderworldInvasionScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddBronzeScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddCurseScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddUnderworldPassageScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddPlentyVaultScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddLightningStormScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddEarthquakeScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddWitherScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddArcadianMeadowScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddCommunalHearthScalingData()
        {
        }
    }
}
