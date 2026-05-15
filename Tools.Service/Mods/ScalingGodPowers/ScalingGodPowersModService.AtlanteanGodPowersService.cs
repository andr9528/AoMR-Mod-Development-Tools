using Microsoft.Extensions.Logging;
using Tools.Abstraction.Enum;
using Tools.Persistence;

namespace Tools.Service.Mods.ScalingGodPowers;

public partial class ScalingGodPowersModService
{
    private class AtlanteanGodPowersService
    {
        private readonly ToolsDatabaseContext toolsDatabaseContext;
        private readonly ILogger<AtlanteanGodPowersService> logger;

        public AtlanteanGodPowersService(
            ToolsDatabaseContext toolsDatabaseContext, ILogger<AtlanteanGodPowersService> logger)
        {
            this.toolsDatabaseContext = toolsDatabaseContext;
            this.logger = logger;
        }

        private static readonly HashSet<GodPowerName> GodPowers =
        [
            GodPowerName.DECONSTRUCTION, // Deconstruction
            GodPowerName.SHOCKWAVE, // Shockwave
            GodPowerName.GAIA_FOREST, // GaiaForest
            GodPowerName.CARNIVORA, // Carnivora
            GodPowerName.VALOR, // Valor
            GodPowerName.SPIDER_LAIR, // SpiderLair
            GodPowerName.TRAITOR, // Traitor
            GodPowerName.CHAOS, // Chaos
            GodPowerName.HESPERIDES_TREE, // HesperidesTree
            GodPowerName.VORTEX, // Vortex
            GodPowerName.TARTARIAN_GATE, // TartarianGate
            GodPowerName.IMPLODE, // Implode
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
                case GodPowerName.DECONSTRUCTION:
                    AddDeconstructionScalingData();
                    break;
                case GodPowerName.SHOCKWAVE:
                    AddShockwaveScalingData();
                    break;
                case GodPowerName.GAIA_FOREST:
                    AddGaiaForestScalingData();
                    break;
                case GodPowerName.CARNIVORA:
                    AddCarnivoraScalingData();
                    break;
                case GodPowerName.VALOR:
                    AddValorScalingData();
                    break;
                case GodPowerName.SPIDER_LAIR:
                    AddSpiderLairScalingData();
                    break;
                case GodPowerName.TRAITOR:
                    AddTraitorScalingData();
                    break;
                case GodPowerName.CHAOS:
                    AddChaosScalingData();
                    break;
                case GodPowerName.HESPERIDES_TREE:
                    AddHesperidesTreeScalingData();
                    break;
                case GodPowerName.VORTEX:
                    AddVortexScalingData();
                    break;
                case GodPowerName.TARTARIAN_GATE:
                    AddTartarianGateScalingData();
                    break;
                case GodPowerName.IMPLODE:
                    AddImplodeScalingData();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddDeconstructionScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddShockwaveScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddGaiaForestScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddCarnivoraScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddValorScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddSpiderLairScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddTraitorScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddChaosScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddHesperidesTreeScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddVortexScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddTartarianGateScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddImplodeScalingData()
        {
        }
    }
}
