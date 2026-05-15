using Tools.Abstraction.Enum;

namespace Tools.Service.Mods.ScalingGodPowers;

public partial class ScalingGodPowersModService
{
    private class SharedGodPowersService
    {
        private static readonly HashSet<GodPowerName> GodPowers =
        [
            GodPowerName.TITAN_GATE, // TitanGate
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
                case GodPowerName.TITAN_GATE:
                    AddTitanGateScalingData();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void AddTitanGateScalingData()
        {
        }
    }
}
