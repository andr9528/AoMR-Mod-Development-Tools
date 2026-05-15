using Microsoft.Extensions.Logging;
using Tools.Abstraction.Enum;
using Tools.Persistence;

namespace Tools.Service.Mods.ScalingGodPowers;

public partial class ScalingGodPowersModService
{
    private class SharedGodPowersService
    {
        private readonly ToolsDatabaseContext toolsDatabaseContext;
        private readonly ILogger<SharedGodPowersService> logger;

        public SharedGodPowersService(ToolsDatabaseContext toolsDatabaseContext, ILogger<SharedGodPowersService> logger)
        {
            this.toolsDatabaseContext = toolsDatabaseContext;
            this.logger = logger;
        }

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
        /// Modifies ´AbstractTitan´, affecting currently active ones.
        /// Adds + 10 Percent Health.
        /// Adds + 10 Base Percent Damage.
        /// </summary>
        private void AddTitanGateScalingData()
        {
        }
    }
}
