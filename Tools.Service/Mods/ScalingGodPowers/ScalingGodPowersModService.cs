using Microsoft.Extensions.Logging;
using Tools.Persistence;

namespace Tools.Service.Mods.ScalingGodPowers;

public partial class ScalingGodPowersModService : BaseModService
{
    private readonly ToolsDatabaseContext db;
    private readonly ILogger<ScalingGodPowersModService> logger;

    /// <inheritdoc />
    protected override string ModFolderName => "ScalingGodPowers";

    private AtlanteanGodPowersService AtlanteanGodPowers { get; }
    private AztecGodPowersService AztecGodPowers { get; }
    private ChineseGodPowersService ChineseGodPowers { get; }
    private EgyptianGodPowersService EgyptianGodPowers { get; }
    private GreekGodPowersService GreekGodPowers { get; }
    private JapaneseGodPowersService JapaneseGodPowers { get; }
    private NorseGodPowersService NorseGodPowers { get; }
    private SharedGodPowersService SharedGodPowers { get; }

    public ScalingGodPowersModService(ToolsDatabaseContext db, ILoggerFactory loggerFactory)
    {
        this.db = db;
        logger = loggerFactory.CreateLogger<ScalingGodPowersModService>();
        AtlanteanGodPowers = new AtlanteanGodPowersService(db, loggerFactory.CreateLogger<AtlanteanGodPowersService>());
        AztecGodPowers = new AztecGodPowersService(db, loggerFactory.CreateLogger<AztecGodPowersService>());
        ChineseGodPowers = new ChineseGodPowersService(db, loggerFactory.CreateLogger<ChineseGodPowersService>());
        EgyptianGodPowers = new EgyptianGodPowersService(db, loggerFactory.CreateLogger<EgyptianGodPowersService>());
        GreekGodPowers = new GreekGodPowersService(db, loggerFactory.CreateLogger<GreekGodPowersService>());
        JapaneseGodPowers = new JapaneseGodPowersService(db, loggerFactory.CreateLogger<JapaneseGodPowersService>());
        NorseGodPowers = new NorseGodPowersService(db, loggerFactory.CreateLogger<NorseGodPowersService>());
        SharedGodPowers = new SharedGodPowersService(db, loggerFactory.CreateLogger<SharedGodPowersService>());
    }

    public void AddScalingData()
    {
        AtlanteanGodPowers.AddScalingData();
        AztecGodPowers.AddScalingData();
        ChineseGodPowers.AddScalingData();
        EgyptianGodPowers.AddScalingData();
        GreekGodPowers.AddScalingData();
        JapaneseGodPowers.AddScalingData();
        NorseGodPowers.AddScalingData();
        SharedGodPowers.AddScalingData();
    }
}
