namespace Tools.Service.Mods.ScalingGodPowers;

public partial class ScalingGodPowersModService : BaseModService
{
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

    public ScalingGodPowersModService()
    {
        AtlanteanGodPowers = new AtlanteanGodPowersService();
        AztecGodPowers = new AztecGodPowersService();
        ChineseGodPowers = new ChineseGodPowersService();
        EgyptianGodPowers = new EgyptianGodPowersService();
        GreekGodPowers = new GreekGodPowersService();
        JapaneseGodPowers = new JapaneseGodPowersService();
        NorseGodPowers = new NorseGodPowersService();
        SharedGodPowers = new SharedGodPowersService();
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
