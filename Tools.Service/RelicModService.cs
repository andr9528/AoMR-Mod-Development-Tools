using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using Tools.Abstraction.Enum;
using Tools.Model;
using Tools.Model.Mod;
using Tools.Persistence;

// ReSharper disable CommentTypo

namespace Tools.Service;

public class RelicModService
{
    private readonly ToolsDatabaseContext _db;

    // list of techs we care about, based on enum
    private readonly HashSet<TechName> _watchedTechs = new()
    {
        TechName.PLOW_BONUS, // PlowBonus
        TechName.IRRIGATION_BONUS, // IrrigationBonus
        TechName.FLOOD_CONTROL_BONUS, // FloodControlBonus
        TechName.TAIL_OF_FEI_RESPAWN, // TailOfFeiRespawn
        TechName.TUSK_OF_DANGKANG_SPAWN, // TuskOfDangkangSpawn
        TechName.BRIDLE_OF_PEGASUS_RESPAWN, // BridleOfPegasusRespawn
        TechName.GOLDEN_LIONS_RESPAWN, // GoldenLionsRespawn
        TechName.RELIC_MONKEY_RESPAWN, // RelicMonkeyRespawn
        TechName.RELIC_ANKHOF_RA, // RelicAnkhofRa
        TechName.RELIC_EYE_OF_HORUS, // RelicEyeOfHorus
        TechName.RELIC_SISTRUM_OF_BAST, // RelicSistrumOfBast
        TechName.RELIC_HEAD_OF_ORPHEUS, // RelicHeadOfOrpheus
        TechName.RELIC_RING_OF_NIBELUNG, // RelicRingOfNibelung
        TechName.RELIC_STAFF_OF_DIONYSUS, // RelicStaffOfDionysus
        TechName.RELIC_FETTERS_OF_FENRIR, // RelicFettersOfFenrir
        TechName.RELIC_GUNGNIR_ODINS_SPEAR, // RelicGungnirOdinsSpear
        TechName.RELIC_KITHARA_OF_APOLLO, // RelicKitharaOfApollo
        TechName.RELIC_DWARVEN_HORSESHOES, // RelicDwarvenHorseshoes
        TechName.RELIC_BOW_OF_ARTEMIS, // RelicBowOfArtemis
        TechName.RELIC_SCALES_OF_ECHIDNA, // RelicScalesOfEchidna
        TechName.RELIC_NOSE_OF_THE_SPHINX, // RelicNoseOfTheSphinx
        TechName.RELIC_ARROWS_OF_ALFAR, // RelicArrowsOfAlfar
        TechName.RELIC_ARROWS_OF_HERACLES, // RelicArrowsOfHeracles
        TechName.RELIC_GAMBANTEINN_ODINS_WAND, // RelicGambanteinnOdinsWand
        TechName.RELIC_ULLRS_BOW, // RelicUllrsBow
        TechName.RELIC_BLUE_CRYSTAL_SHARD, // RelicBlueCrystalShard
        TechName.RELIC_ARMOR_OF_ACHILLES, // RelicArmorOfAchilles
        TechName.RELIC_SHIP_OF_FINGERNAILS, // RelicShipOfFingernails
        TechName.RELIC_ADAMANTITE_SHINGLES, // RelicAdamantiteShingles
        TechName.RELIC_EYE_OF_ORNLU, // RelicEyeOfOrnlu
        TechName.RELIC_TUSK_OF_THE_IRON_BOAR, // RelicTuskOfTheIronBoar
        TechName.RELIC_PANDORAS_BOX, // RelicPandorasBox
        TechName.RELIC_THUNDERCLOUD_SHAWL, // RelicThundercloudShawl
        TechName.RELIC_HARMONIAS_NECKLACE, // RelicHarmoniasNecklace
        TechName.RELIC_DWARVEN_CALIPERS, // RelicDwarvenCalipers
        TechName.RELIC_CANOPIC_JAR_OF_IMSETY, // RelicCanopicJarOfImsety
        TechName.RELIC_TOWER_OF_SESTUS, // RelicTowerOfSestus
        TechName.RELIC_TROJAN_GATE_HINGE, // RelicTrojanGateHinge
        TechName.RELIC_GIRDLE_OF_HIPPOLYTA, // RelicGirdleOfHippolyta
        TechName.RELIC_PYGMALIONS_STATUE, // RelicPygmalionsStatue
        TechName.RELIC_BLACK_LOTUS, // RelicBlackLotus
        TechName.RELIC_SCARAB_PENDANT, // RelicScarabPendant
        TechName.RELIC_HERMES_WINGED_SANDALS, // RelicHermesWingedSandals
        TechName.RELIC_ANVIL_OF_HEPHAESTUS, // RelicAnvilOfHephaestus
        TechName.RELIC_PELT_OF_ARGUS, // RelicPeltOfArgus
        TechName.RELIC_OSEBERG_WAGON, // RelicOsebergWagon
        TechName.RELIC_SCALES_OF_CATOBLEPAS, // RelicScalesOfCatoblepas
        TechName.RELIC_TAIL_OF_CERBERUS, // RelicTailOfCerberus
        TechName.RELIC_BLANKET_OF_EMPRESS_ZOE, // RelicBlanketOfEmpressZoe
        TechName.RELIC_KHOPESH_OF_HORUS, // RelicKhopeshOfHorus
        TechName.RELIC_TITANS_TREASURE, // RelicTitansTreasure
        TechName.RELIC_GAIAS_BOOK_OF_KNOWLEDGE, // RelicGaiasBookOfKnowledge
        TechName.RELIC_ARCHIMEDES_LEDGER, // RelicArchimedesLedger
        TechName.RELIC_DEMETERS_THRONE, // RelicDemetersThrone
        TechName.RELIC_HESTIAS_HEARTH, // RelicHestiasHearth
        TechName.RELIC_PTAHS_SCEPTRE, // RelicPtahsSceptre
        TechName.RELIC_TYRFING_ANGANTYRS_SWORD, // RelicTyrfingAngantyrsSword
        TechName.RELIC_RHEIAS_CROWN, // RelicRheiasCrown
        TechName.RELIC_TEXTS_OF_IMHOTEP, // RelicTextsOfImhotep
        TechName.RELIC_CHARONS_OBOL, // RelicCharonsObol
        TechName.RELIC_HEKATES_TORCHES, // RelicHekatesTorches
        TechName.RELIC_SHEN_OF_NEKHBET, // RelicShenOfNekhbet
        TechName.RELIC_GLAUCUS_BOOK_OF_PROPHECIES, // RelicGlaucusBookOfProphecies
        TechName.RELIC_PROW_OF_THE_ARGO, // RelicProwOfTheArgo
        TechName.RELIC_GOLDEN_CAMEL_STATUE, // RelicGoldenCamelStatue
        TechName.RELIC_ORACLE_BONE, // RelicOracleBone
        TechName.RELIC_XUAN_YUAN_SWORD, // RelicXuanYuanSword
        TechName.RELIC_KUI_DRUM, // RelicKuiDrum
        TechName.RELIC_FIVE_COLORED_STONE_OF_NUWA, // RelicFiveColoredStoneOfNuwa
        TechName.RELIC_NINE_CAULDRONS, // RelicNineCauldrons
        TechName.RELIC_FUR_OF_BOYI, // RelicFurOfBoyi
        TechName.RELIC_WUHAO_BOW_OF_HUANGDI, // RelicWuhaoBowOfHuangdi
        TechName.RELIC_FEATHER_OF_BIFANG, // RelicFeatherOfBifang
        TechName.RELIC_FEATHER_OF_JINGWEI, // RelicFeatherOfJingwei
        TechName.RELIC_JADE_HARE, // RelicJadeHare
        TechName.RELIC_XIRANG, // RelicXirang
        TechName.RELIC_BRIDLE_OF_PEGASUS, // RelicBridleOfPegasus
        TechName.RELIC_CHARIOT_OF_CYBELE, // RelicChariotOfCybele
        TechName.RELIC_SKULLS_OF_THE_CERCOPES, // RelicSkullsOfTheCercopes
        TechName.RELIC_HARTERS_FOLLY, // RelicHartersFolly
        TechName.RELIC_FLAGSTONE_OF_BUHEN, // RelicFlagstoneOfBuhen
        TechName.RELIC_SVADILFARIS_SLEDGE, // RelicSvadilfarisSledge
        TechName.RELIC_TUSK_OF_DANGKANG, // RelicTuskOfDangkang
        TechName.RELIC_TAIL_OF_FEI, // RelicTailOfFei
        TechName.RELIC_OCHRE_WHIP_OF_SHENNONG, // RelicOchreWhipOfShennong
        TechName.RELIC_TAIL_FEATHERS_OF_HEAVEN, // RelicTailFeathersOfHeaven
        TechName.RELIC_LITTLE_CROW_CIRCLE, // RelicLittleCrowCircle
        TechName.RELIC_HEAVENLY_JEWELED_SPEAR, // RelicHeavenlyJeweledSpear
        TechName.RELIC_OKUNINUSHIS_COVENANT, // RelicOkuninushisCovenant
        TechName.RELIC_HOORIS_HUNTING_BOW, // RelicHoorisHuntingBow
        TechName.RELIC_NINE_TAILED_FOX_CHARM, // RelicNineTailedFoxCharm
        TechName.RELIC_GRASS_CUTTING_SWORD, // RelicGrassCuttingSword
        TechName.RELIC_TAWARA_TODAS_LAST_ARROW, // RelicTawaraTodasLastArrow
        TechName.RELIC_OGRE_BITTEN_HELM, // RelicOgreBittenHelm
        TechName.RELIC_DRAGON_PALACE_CRYSTAL, // RelicDragonPalaceCrystal
        TechName.RELIC_AME_NO_MURAKUMO_NO_TSURUGI, // RelicAmeNoMurakumoNoTsurugi
        TechName.RELIC_CUP_OF_DIONYSUS, // RelicCupOfDionysus
        TechName.RELIC_SHIELD_OF_ATHENA, // RelicShieldOfAthena
        TechName.RELIC_CROWN_OF_HERA, // RelicCrownOfHera
        TechName.RELIC_ZEUS_THUNDERBOLTS, // RelicZeusThunderbolts
        TechName.RELIC_RELIC_OF_PROSPERITY, // RelicRelicOfProsperity
        TechName.RELIC_RELIC_OF_BRONZE, // RelicRelicOfBronze
        TechName.RELIC_RELIC_OF_EARTHQUAKE, // RelicRelicOfEarthquake
        TechName.RELIC_RELIC_OF_ANCESTORS, // RelicRelicOfAncestors
        TechName.RELIC_RELIC_OF_TORNADO, // RelicRelicOfTornado
        TechName.RELIC_ONIKIRI_DEMON_SLAYER, // RelicOnikiriDemonSlayer
        TechName.RELIC_FRAGMENT_OF_THE_KILLING_STONE, // RelicFragmentOfTheKillingStone
        TechName.RELIC_HODERIS_LOST_FISH_HOOK, // RelicHoderisLostFishHook
        TechName.RELIC_DECEPTIVE_WOODEN_SWORD, // RelicDeceptiveWoodenSword
        TechName.RELIC_SHACHIHOKO_ORNAMENT, // RelicShachihokoOrnament
        TechName.RELIC_CARTOGRAPHERS_HOURGLASS, // RelicCartographersHourglass
        // ... add all other relic techs you want
    };

    public RelicModService(ToolsDatabaseContext db)
    {
        _db = db;
    }

    public async Task ApplyMultiplierAsync(double multiplier)
    {
        var techs = _db.Techs.ToList();

        foreach (Tech tech in techs)
        {
            if (!Enum.TryParse<TechName>(ToScreamingSnake(tech.Name), out TechName techName))
            {
                continue;
            }

            if (!_watchedTechs.Contains(techName))
            {
                continue;
            }

            // Custom logic per-tech could live here
            ApplyMultiplierToTech(tech, multiplier);
        }

        await _db.SaveChangesAsync();
    }

    private void ApplyMultiplierToTech(Tech tech, double multiplier)
    {
        foreach (Effect effect in tech.Effects)
        {
            if (effect.MergeMode == MergeMode.ADD || effect.Amount == 0)
            {
                continue; // only modify effects that add values
            }

            double newAmount = CalculateNewAmount(effect.Relativity, effect.Amount, multiplier);

            var newEffect = new Effect
            {
                MergeMode = MergeMode.ADD,
                Type = effect.Type,
                Action = effect.Action,
                Subtype = effect.Subtype,
                Resource = effect.Resource,
                Unit = effect.Unit,
                Generator = effect.Generator,
                Relativity = effect.Relativity,
                Amount = newAmount,
            };

            foreach (Target target in effect.Targets)
            {
                newEffect.Targets.Add(new Target
                {
                    Type = target.Type,
                    Value = target.Value,
                });
            }

            tech.Effects.Add(newEffect);
        }
    }

    private double CalculateNewAmount(Relativity relativity, double oldAmount, double multiplier)
    {
        double result;

        switch (relativity)
        {
            case Relativity.ABSOLUTE:
            case Relativity.ASSIGN:
                result = oldAmount * multiplier;
                break;

            case Relativity.PERCENT:
                result = 1 - (1 - oldAmount) * multiplier;
                if (result < 0.05)
                {
                    result = 0.05;
                }

                break;

            case Relativity.BASE_PERCENT:
                result = (oldAmount - 1) * multiplier + 1;
                break;

            default:
                result = oldAmount * multiplier;
                break;
        }

        // 🔹 Normalize to 2 decimals before saving
        return Math.Round(result, 2);
    }


    private string ToScreamingSnake(string name)
    {
        // match enum naming convention
        string s1 = System.Text.RegularExpressions.Regex.Replace(name, "(.)([A-Z][a-z]+)", "$1_$2");
        string s2 = System.Text.RegularExpressions.Regex.Replace(s1, "([a-z0-9])([A-Z])", "$1_$2");
        return s2.ToUpper();
    }

    public XDocument ExportToXml()
    {
        // Gets all techs that have 1+ ADD effects
        var techs = _db.Techs.Where(t => t.Effects.Any(e => e.MergeMode == MergeMode.ADD)).Include(tech => tech.Effects)
            .ThenInclude(effect => effect.Targets).ToList();

        var root = new XElement("techtreemods");

        foreach (Tech tech in techs)
        {
            var techElem = new XElement("tech", new XAttribute("name", tech.Name),
                new XAttribute("type", tech.Type ?? "Normal"));

            var effectsElem = new XElement("effects");

            foreach ((Effect add, Effect remove) in FindEffectPairs(tech))
            {
                AddEffectToXml(effectsElem, remove);
                AddEffectToXml(effectsElem, add);
            }

            techElem.Add(effectsElem);
            root.Add(techElem);
        }

        return new XDocument(root);
    }

    private bool HasMatchingTarget(Effect add, Effect remove)
    {
        return add.Targets.Any(t1 => remove.Targets.Any(t2 => t1.Value == t2.Value));
    }

    private IEnumerable<(Effect Add, Effect Remove)> FindEffectPairs(Tech tech)
    {
        var effectPairs = from add in tech.Effects
            from remove in tech.Effects
            where add.MergeMode == MergeMode.ADD && remove.MergeMode == MergeMode.REMOVE &&
                  HasMatchingTarget(add, remove)
            select (Add: add, Remove: remove);

        return effectPairs;
    }

    private void AddEffectToXml(XElement effectsElement, Effect effect)
    {
        var effectElem = new XElement("effect", new XAttribute("mergeMode", effect.MergeMode.ToString().ToLower()),
            new XAttribute("amount", effect.Amount.ToString("0.00")), // 🔹 round to 2 decimals
            new XAttribute("subtype", effect.Subtype), new XAttribute("relativity", effect.Relativity));

        if (!string.IsNullOrEmpty(effect.Action))
        {
            effectElem.Add(new XAttribute("action", effect.Action));
        }

        if (!string.IsNullOrEmpty(effect.Resource))
        {
            effectElem.Add(new XAttribute("resource", effect.Resource));
        }

        if (!string.IsNullOrEmpty(effect.Unit))
        {
            effectElem.Add(new XAttribute("unit", effect.Unit));
        }

        if (!string.IsNullOrEmpty(effect.Generator))
        {
            effectElem.Add(new XAttribute("generator", effect.Generator));
        }

        foreach (Target target in effect.Targets)
        {
            effectElem.Add(new XElement("target", new XAttribute("type", target.Type), target.Value ?? string.Empty));
        }

        effectsElement.Add(effectElem);
    }
}
