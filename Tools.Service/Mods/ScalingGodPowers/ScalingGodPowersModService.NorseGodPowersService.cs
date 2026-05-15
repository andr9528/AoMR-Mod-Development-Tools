using Microsoft.Extensions.Logging;
using Tools.Abstraction.Enum;
using Tools.Persistence;

namespace Tools.Service.Mods.ScalingGodPowers;

public partial class ScalingGodPowersModService
{
    private class NorseGodPowersService
    {
        private readonly ToolsDatabaseContext toolsDatabaseContext;
        private readonly ILogger<NorseGodPowersService> logger;

        public NorseGodPowersService(ToolsDatabaseContext toolsDatabaseContext, ILogger<NorseGodPowersService> logger)
        {
            this.toolsDatabaseContext = toolsDatabaseContext;
            this.logger = logger;
        }

        private static readonly HashSet<GodPowerName> GodPowers =
        [
            GodPowerName.DWARVEN_MINE, // DwarvenMine
            GodPowerName.SPY, // Spy
            GodPowerName.GREAT_HUNT, // GreatHunt
            GodPowerName.GULLINBURSTI, // Gullinbursti
            GodPowerName.FOREST_FIRE, // ForestFire
            GodPowerName.HEALING_SPRING, // HealingSpring
            GodPowerName.UNDERMINE, // Undermine
            GodPowerName.ASGARDIAN_BASTION, // AsgardianBastion
            GodPowerName.FROST, // Frost
            GodPowerName.FLAMING_WEAPONS, // FlamingWeapons
            GodPowerName.WALKING_WOODS, // WalkingWoods
            GodPowerName.TEMPEST, // Tempest
            GodPowerName.RAGNAROK, // Ragnarok
            GodPowerName.FIMBULWINTER, // Fimbulwinter
            GodPowerName.NIDHOGG, // Nidhogg
            GodPowerName.INFERNO, // Inferno
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
                case GodPowerName.DWARVEN_MINE:
                    AddDwarvenMineScalingData();
                    break;
                case GodPowerName.SPY:
                    AddSpyScalingData();
                    break;
                case GodPowerName.GREAT_HUNT:
                    AddGreatHuntScalingData();
                    break;
                case GodPowerName.GULLINBURSTI:
                    AddGullinburstiScalingData();
                    break;
                case GodPowerName.FOREST_FIRE:
                    AddForestFireScalingData();
                    break;
                case GodPowerName.HEALING_SPRING:
                    AddHealingSpringScalingData();
                    break;
                case GodPowerName.UNDERMINE:
                    AddUndermineScalingData();
                    break;
                case GodPowerName.ASGARDIAN_BASTION:
                    AddAsgardianBastionScalingData();
                    break;
                case GodPowerName.FROST:
                    AddFrostScalingData();
                    break;
                case GodPowerName.FLAMING_WEAPONS:
                    AddFlamingWeaponsScalingData();
                    break;
                case GodPowerName.WALKING_WOODS:
                    AddWalkingWoodsScalingData();
                    break;
                case GodPowerName.TEMPEST:
                    AddTempestScalingData();
                    break;
                case GodPowerName.RAGNAROK:
                    AddRagnarokScalingData();
                    break;
                case GodPowerName.FIMBULWINTER:
                    AddFimbulwinterScalingData();
                    break;
                case GodPowerName.NIDHOGG:
                    AddNidhoggScalingData();
                    break;
                case GodPowerName.INFERNO:
                    AddInfernoScalingData();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddDwarvenMineScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddSpyScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddGreatHuntScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddGullinburstiScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddForestFireScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddHealingSpringScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddUndermineScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddAsgardianBastionScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddFrostScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddFlamingWeaponsScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddWalkingWoodsScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddTempestScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddRagnarokScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddFimbulwinterScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddNidhoggScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddInfernoScalingData()
        {
        }
    }
}
