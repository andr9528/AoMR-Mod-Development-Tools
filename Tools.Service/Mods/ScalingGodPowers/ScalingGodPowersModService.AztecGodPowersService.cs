using Microsoft.Extensions.Logging;
using Tools.Abstraction.Enum;
using Tools.Persistence;

namespace Tools.Service.Mods.ScalingGodPowers;

public partial class ScalingGodPowersModService
{
    private class AztecGodPowersService
    {
        private readonly ToolsDatabaseContext toolsDatabaseContext;
        private readonly ILogger<AztecGodPowersService> logger;

        public AztecGodPowersService(ToolsDatabaseContext toolsDatabaseContext, ILogger<AztecGodPowersService> logger)
        {
            this.toolsDatabaseContext = toolsDatabaseContext;
            this.logger = logger;
        }

        private static readonly HashSet<GodPowerName> GodPowers =
        [
            GodPowerName.BLOOD_PACT, // BloodPact
            GodPowerName.TAILWIND, // Tailwind
            GodPowerName.OBSIDIAN_MIRROR, // ObsidianMirror
            GodPowerName.LULLABY, // Lullaby
            GodPowerName.INFESTATION, // Infestation
            GodPowerName.AGAVE_BLOOM, // AgaveBloom
            GodPowerName.EARTH_MONSTER, // EarthMonster
            GodPowerName.STARFALL, // Starfall
            GodPowerName.PURGE, // Purge
            GodPowerName.CORRUPTED_GROUND, // CorruptedGround
            GodPowerName.MONOLITH_OF_TLALOC, // MonolithOfTlaloc
            GodPowerName.VOLCANO, // Volcano
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
                case GodPowerName.BLOOD_PACT:
                    AddBloodPactScalingData();
                    break;
                case GodPowerName.TAILWIND:
                    AddTailwindScalingData();
                    break;
                case GodPowerName.OBSIDIAN_MIRROR:
                    AddObsidianMirrorScalingData();
                    break;
                case GodPowerName.LULLABY:
                    AddLullabyScalingData();
                    break;
                case GodPowerName.INFESTATION:
                    AddInfestationScalingData();
                    break;
                case GodPowerName.AGAVE_BLOOM:
                    AddAgaveBloomScalingData();
                    break;
                case GodPowerName.EARTH_MONSTER:
                    AddEarthMonsterScalingData();
                    break;
                case GodPowerName.STARFALL:
                    AddStarfallScalingData();
                    break;
                case GodPowerName.PURGE:
                    AddPurgeScalingData();
                    break;
                case GodPowerName.CORRUPTED_GROUND:
                    AddCorruptedGroundScalingData();
                    break;
                case GodPowerName.MONOLITH_OF_TLALOC:
                    AddMonolithOfTlalocScalingData();
                    break;
                case GodPowerName.VOLCANO:
                    AddVolcanoScalingData();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddBloodPactScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddTailwindScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddObsidianMirrorScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddLullabyScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddInfestationScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddAgaveBloomScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddEarthMonsterScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddStarfallScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddPurgeScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddCorruptedGroundScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddMonolithOfTlalocScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddVolcanoScalingData()
        {
        }
    }
}
