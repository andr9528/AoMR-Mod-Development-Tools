using Tools.Abstraction.Enum;

namespace Tools.Service.Mods.ScalingGodPowers;

public partial class ScalingGodPowersModService
{
    private class EgyptianGodPowersService
    {
        private static readonly HashSet<GodPowerName> GodPowers =
        [
            GodPowerName.RAIN, // Rain
            GodPowerName.PROSPERITY, // Prosperity
            GodPowerName.VISION, // Vision
            GodPowerName.ECLIPSE, // Eclipse
            GodPowerName.SHIFTING_SANDS, // ShiftingSands
            GodPowerName.PLAGUE_OF_SERPENTS, // PlagueOfSerpents
            GodPowerName.LOCUST_SWARM, // LocustSwarm
            GodPowerName.ANCESTORS, // Ancestors
            GodPowerName.CITADEL, // Citadel
            GodPowerName.SON_OF_OSIRIS, // SonOfOsiris
            GodPowerName.METEOR, // Meteor
            GodPowerName.TORNADO, // Tornado
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
                case GodPowerName.RAIN:
                    AddRainScalingData();
                    break;
                case GodPowerName.PROSPERITY:
                    AddProsperityScalingData();
                    break;
                case GodPowerName.VISION:
                    AddVisionScalingData();
                    break;
                case GodPowerName.ECLIPSE:
                    AddEclipseScalingData();
                    break;
                case GodPowerName.SHIFTING_SANDS:
                    AddShiftingSandsScalingData();
                    break;
                case GodPowerName.PLAGUE_OF_SERPENTS:
                    AddPlagueOfSerpentsScalingData();
                    break;
                case GodPowerName.LOCUST_SWARM:
                    AddLocustSwarmScalingData();
                    break;
                case GodPowerName.ANCESTORS:
                    AddAncestorsScalingData();
                    break;
                case GodPowerName.CITADEL:
                    AddCitadelScalingData();
                    break;
                case GodPowerName.SON_OF_OSIRIS:
                    AddSonOfOsirisScalingData();
                    break;
                case GodPowerName.METEOR:
                    AddMeteorScalingData();
                    break;
                case GodPowerName.TORNADO:
                    AddTornadoScalingData();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddRainScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddProsperityScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddVisionScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddEclipseScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddShiftingSandsScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddPlagueOfSerpentsScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddLocustSwarmScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddAncestorsScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddCitadelScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddSonOfOsirisScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddMeteorScalingData()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddTornadoScalingData()
        {
        }
    }
}
