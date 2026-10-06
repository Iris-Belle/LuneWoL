namespace LuneWoL.Common.Global_Tiles;

public partial class LuneWoL_GT : GlobalTile
{
    public override bool CanDrop(int i, int j, int type)
    {
        if (LuneLib.LuneLib.instance.CalamityModLoaded)
            return CalOreDropChance(i, j, type);
        if (LuneLib.LuneLib.instance.ThoriumModLoaded)
            return ThoOreDropChance(i, j, type);
        if (LuneLib.LuneLib.instance.SpiritModLoaded)
            return SpiOreDropChance(i, j, type);
        return VanOreDropChance(i, j, type);
    }

    public bool VanOreDropChance(int i, int j, int type)
    {

        var cfg = AdvServerConfig.Adv_Environment.OreDropChance;
        int chance = type switch
        {
            //Extractinator
            TileID.Silt => cfg.SiltDropChance,
            TileID.Slush => cfg.SlushDropChance,
            TileID.DesertFossil => cfg.DesertFossilDropChance,
            //Pre-Hardmode1
            TileID.Copper => cfg.CopperDropChance,
            TileID.Iron => cfg.IronDropChance,
            TileID.Silver => cfg.SilverDropChance,
            TileID.Gold => cfg.GoldDropChance,
            //Pre-Hardmode2
            TileID.Tin => cfg.TinDropChance,
            TileID.Lead => cfg.LeadDropChance,
            TileID.Tungsten => cfg.TungstenDropChance,
            TileID.Platinum => cfg.PlatinumDropChance,
            //Pre-Hardmode3
            TileID.Meteorite => cfg.MeteoriteDropChance,
            TileID.Demonite => cfg.DemoniteDropChance,
            TileID.Crimtane => cfg.CrimtaneDropChance,
            //Pre-Hardmode4
            TileID.Obsidian => cfg.ObsidianDropChance,
            TileID.Hellstone => cfg.HellstoneDropChance,
            //Hardmode1
            TileID.Cobalt => cfg.CobaltDropChance,
            TileID.Mythril => cfg.MythrilDropChance,
            TileID.Titanium => cfg.TitaniumDropChance,
            //Hardmode2
            TileID.Palladium => cfg.PalladiumDropChance,
            TileID.Orichalcum => cfg.OrichalcumDropChance,
            TileID.Adamantite => cfg.AdamantiteDropChance,
            //Hardmode3
            TileID.Chlorophyte => cfg.ChlorophyteDropChance,
            //Post-Moonlord
            TileID.LunarOre => cfg.LunarOreDropChance,
            _ => 0
        };

        if (chance == 0)
            return base.CanDrop(i, j, type);

        return Main.rand.I4Chance(chance) && base.CanDrop(i, j, type);
    }

    [JITWhenModsEnabled("CalamityMod")]
    public bool CalOreDropChance(int i, int j, int type)
    {
        var cfg = AdvServerConfig.Adv_Environment.OreDropChance.CalamityMod;

        int chance = type switch
        {
            //Pre-Hardmode
            int t when t == ModContent.TileType<SeaPrism>() => cfg.SeaPrismDropChance,
            int t when t == ModContent.TileType<AerialiteOre>() => cfg.AerialiteOreDropChance,
            //Hardmode
            int t when t == ModContent.TileType<AerialiteOreDisenchanted>() => cfg.AerialiteOreDisenchantedDropChance,
            int t when t == ModContent.TileType<InfernalSuevite>() => cfg.InfernalSueviteDropChance,
            int t when t == ModContent.TileType<CryonicOre>() => cfg.CryonicOreDropChance,
            int t when t == ModContent.TileType<HallowedOre>() => cfg.HallowedOreDropChance,
            int t when t == ModContent.TileType<PerennialOre>() => cfg.PerennialOreDropChance,
            int t when t == ModContent.TileType<ScoriaOre>() => cfg.ScoriaOreDropChance,
            int t when t == ModContent.TileType<AstralOre>() => cfg.AstralOreDropChance,
            //Post-Moonlord
            int t when t == ModContent.TileType<ExodiumOre>() => cfg.ExodiumOreDropChance,
            int t when t == ModContent.TileType<UelibloomOre>() => cfg.UelibloomOreDropChance,
            int t when t == ModContent.TileType<AuricOre>() => cfg.AuricOreDropChance,
            _ => 0
        };

        if (chance == 0)
            return base.CanDrop(i, j, type);

        return Main.rand.I4Chance(chance) && base.CanDrop(i, j, type);
    }

    [JITWhenModsEnabled("ThoriumMod")]
    public bool ThoOreDropChance(int i, int j, int type)
    {
        var cfg = AdvServerConfig.Adv_Environment.OreDropChance.ThoriumMod;

        int chance = type switch
        {
            //Pre-Hardmode
            int t when t == ModContent.TileType<SynthGold>() => cfg.SynthGoldDropChance,
            int t when t == ModContent.TileType<SynthPlatinum>() => cfg.SynthPlatinumDropChance,
            int t when t == ModContent.TileType<SmoothCoal>() => cfg.SmoothCoalDropChance,
            int t when t == ModContent.TileType<LifeQuartz>() => cfg.LifeQuartzDropChance,
            int t when t == ModContent.TileType<ThoriumOre>() => cfg.ThoriumOreDropChance,
            int t when t == ModContent.TileType<Aquaite>() => cfg.AquaiteDropChance,
            //Hardmode
            int t when t == ModContent.TileType<LodeStone>() => cfg.LodeStoneDropChance,
            int t when t == ModContent.TileType<ValadiumChunk>() => cfg.ValadiumChunkDropChance,
            int t when t == ModContent.TileType<IllumiteChunk>() => cfg.IllumiteChunkDropChance,
            _ => 0
        };

        if (chance == 0)
            return base.CanDrop(i, j, type);

        return Main.rand.I4Chance(chance) && base.CanDrop(i, j, type);
    }

    [JITWhenModsEnabled("SpiritMod")]
    public bool SpiOreDropChance(int i, int j, int type)
    {
        var cfg = AdvServerConfig.Adv_Environment.OreDropChance.SpiritMod;

        int chance = type switch
        {
            //Pre-Hardmode
            int t when t == ModContent.TileType<BismiteCrystalOre>() => cfg.BismiteCrystalOreDropChance,
            int t when t == ModContent.TileType<FloranOreTile>() => cfg.FloranOreTileDropChance,
            int t when t == ModContent.TileType<MarbleOre>() => cfg.MarbleOreDropChance,
            int t when t == ModContent.TileType<GraniteOre>() => cfg.GraniteOreDropChance,
            int t when t == ModContent.TileType<Glowstone>() => cfg.GlowstoneDropChance,
            int t when t == ModContent.TileType<CryoliteOreTile>() => cfg.CryoliteOreTileDropChance,
            //Hardmode
            int t when t == ModContent.TileType<SpiritOreTile>() => cfg.SpiritOreTileDropChance,
            _ => 0
        };

        if (chance == 0)
            return base.CanDrop(i, j, type);

        return Main.rand.I4Chance(chance) && base.CanDrop(i, j, type);
    }
}