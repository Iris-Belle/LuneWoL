namespace LuneWoL.Common.Mod_Player;

public partial class LuneWoL_Plr : ModPlayer
{
    #region Fields

    public int
        SpaceDoT = 50;

    internal float _SurfaceDecay, _CorruptionDecay, _JungleDecay, _HallowDecay, _SnowDecay, _DesertDecay, _BloodMoonDecay, _CrimsonDecay, _UndergroundDesertDecay, _OceanDecay, _ModdedDecay;

    internal int
        LostHealth,
        LostMana,
        HealthCache,
        ManaCache;

    internal bool
        DeathFlag0 = false,
        DeathFlag1 = false;

    private enum Timers
    {
        TundraBlizzardCounter,
        TundraChilledCounter,
        UpdateConfig
    }

    private readonly TimerSet<Timers> _timers = TimerSet<Timers>.ForEnum();

    #endregion

    #region Hooks

    public override void UpdateBadLifeRegen()
    {
        float totalNegativeLifeRegen = 0;

        void ApplyDoTDebuff(bool hasDebuff, int negativeLifeRegenToApply, bool immuneCondition = false)
        {
            if (!hasDebuff || immuneCondition)
                return;

            if (Player.lifeRegen > 0)
                Player.lifeRegen = 0;

            Player.lifeRegenTime = 0;
            totalNegativeLifeRegen += negativeLifeRegenToApply;
        }
        LibPlayer pLib = Player.LibPlayer();
        var SpaceCfg = AdvServerConfig.Adv_Environment.SpaceVacuum;

        SpaceDoT = SpaceCfg.BaseDoTRate -
            (pLib.WearingAstroHelm ? SpaceCfg.AstroHelmReduction : 0) -
            (pLib.WearingAstraliteVisor ? SpaceCfg.AstraliteVisorReduction : 0) -
            (pLib.IsWearingFishBowl ? SpaceCfg.FishBowlReduction : 0);

        ApplyDoTDebuff(Player.LibPlayer().SpaceVacuum, SpaceDoT, pLib.WearingFullAstralite || pLib.WearingFullAstro);

        ApplyDoTDebuff(Player.LibPlayer().DepthWaterPressure, Player.LibPlayer().CurrentDepthPressure);

        ApplyDoTDebuff(Player.LibPlayer().BlizzardGivesFrozen, AdvServerConfig.Adv_Environment.WeatherEffects.BlizzardFrozenDamage*2, Player.buffImmune[BuffID.Frozen]);

        ApplyDoTDebuff(Player.LibPlayer().TundraGivesChilled, AdvServerConfig.Adv_Environment.WeatherEffects.TundraChilledDamage*2, Player.buffImmune[BuffID.Chilled]);

        ApplyDoTDebuff(Player.LibPlayer().InEvilBiomeAtNight, 100, false);

        Player.lifeRegen -= (int)totalNegativeLifeRegen;

        if (Player.lifeRegen <= 0 && ServerConfig.Player.DebuffMultiplier != 1)
            Player.lifeRegen = (int)(Player.lifeRegen * (ServerConfig.Player.DebuffMultiplier * 2));
    }
    
    public override async void OnEnterWorld()
    {
        await EnterWorldMessage();
    }

    public override void OnRespawn()
    {
        DeathPenaltyAppliedOnRespawn();
        DeathPenaltyConsumedCrystals();
        DeathPenaltyConsumedFloor();
    }

    public override void PostUpdateEquips()
    {
        UnderworldGivesOnFire();
    }

    public override void PostUpdateMiscEffects()
    {
        PlrStats();
    }

    public override void PostUpdateRunSpeeds()
    {
        ViscousWater();
    }

    public override void PreUpdateBuffs()
    {
        PoisonedWater();

        ApplySpaceVacuum();
        WeatherChanges();
        TundraAppliesChilled();

        OnlyEnterEvilAtDay();
    }

    public override void PostUpdate()
    {
        ResetDeathPenalty();

        UpdateConfigState();

        // https://steamcommunity.com/sharedfiles/filedetails/?id=2395507804
    }

    public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genDust, ref PlayerDeathReason damageSource)
    {
        if (Player.LibPlayer().InEvilBiomeAtNight && Player.whoAmI == Main.myPlayer)
        {
            damageSource = PlayerDeathReason.ByCustomReason(GetText("Status.Death.CrimtuptionzoneDeath").ToNetworkText(Player.name));
        }

        return true;
    }

    public override void ModifyMaxStats(out StatModifier health, out StatModifier mana) => DeathPenaltyStatmod(out health, out mana);

    public override void SyncPlayer(int toWho, int fromWho, bool newPlayer) => SyncDeathPenalty(toWho, fromWho, newPlayer);

    public override void CopyClientState(ModPlayer targetCopy) => CloneClientsDeathPenalty(targetCopy);

    public override void SendClientChanges(ModPlayer clientPlayer) => SendDeathPenalty(clientPlayer);

    public override void SaveData(TagCompound tag) => SaveDeathPenaltyTag(tag);

    public override void LoadData(TagCompound tag) => LoadDeathPenaltyTag(tag);

    #endregion

    #region Methods

    public void ApplySpaceVacuum()
    {
        if (!ServerConfig.Environment.SpaceDoT || Player.Submerged() || Player.whoAmI != Main.myPlayer || !Player.ZoneSkyHeight || Player.behindBackWall)
            return;

        Main.buffNoTimeDisplay[ModContent.BuffType<SpaceVacuum>()] = true;
        Player.AddBuff(ModContent.BuffType<SpaceVacuum>(), 15, true, false);
    }

    public void ApplyZoneBuff(int buffId, int duration = 180)
    {
        Main.buffNoTimeDisplay[buffId] = true;
        Player.AddBuff(buffId, duration, true, false);

        if (Player.buffTime[buffId] > duration)
            Player.buffTime[buffId] = duration;
    }

    public void CloneClientsDeathPenalty(ModPlayer targetCopy)
    {
        if (ServerConfig.Player.DeathPenaltyMode != 1)
            return;

        LuneWoL_Plr clone = (LuneWoL_Plr)targetCopy;
        clone.HealthCache = HealthCache;
        clone.ManaCache = ManaCache;
    }

    public void DeathPenaltyAppliedOnRespawn()
    {
        if (ServerConfig.Player.DeathPenaltyMode != 1)
            return;

        LostHealth += 5;
        LostMana += 5;

        if (Player.statLifeMax2 <= 500 && Player.statLifeMax2 > 20)
        {
            HealthCache++;
        }
        if (Player.statManaMax2 <= 200 && Player.statManaMax2 > 20)
        {
            ManaCache++;
        }

        if (LostHealth >= 20 && Player.statLifeMax2 <= 400 && Player.statLifeMax2 > 100)
        {
            LostHealth = 0;
            HealthCache = 0;
            Player.ConsumedLifeCrystals--;
        }
        else if (LostHealth >= 5 && Player.statLifeMax2 > 400 && Player.statLifeMax2 <= 500)
        {
            LostHealth = 0;
            HealthCache = 0;
            Player.ConsumedLifeFruit--;
        }

        if (LostMana >= 20 && Player.statManaMax2 <= 200 && Player.statManaMax2 > 20)
        {
            LostMana = 0;
            ManaCache = 0;
            Player.ConsumedManaCrystals--;
        }
    }

    public void DeathPenaltyConsumedCrystals()
    {
        if (ServerConfig.Player.DeathPenaltyMode != 2)
            return;

        if (Player.statLifeMax2 <= 400 && Player.statLifeMax2 > 100)
        {
            Player.ConsumedLifeCrystals--;
        }
        else if (Player.statLifeMax2 > 400 && Player.statLifeMax2 <= 500)
        {
            Player.ConsumedLifeFruit--;
        }

        if (Player.statManaMax2 <= 200 && Player.statManaMax2 >= 5)
        {
            Player.ConsumedManaCrystals--;
        }
    }

    public void DeathPenaltyConsumedFloor()
    {
        if (ServerConfig.Player.DeathPenaltyMode != 3)
            return;

        if (Player.statLifeMax2 <= 400 && Player.statLifeMax2 > 100)
        {
            Player.ConsumedLifeCrystals = 0;
        }
        else if (Player.statLifeMax2 > 400 && Player.statLifeMax2 <= 500)
        {
            Player.ConsumedLifeFruit = 0;
        }

        if (Player.statManaMax2 <= 200 && Player.statManaMax2 >= 5)
        {
            Player.ConsumedManaCrystals = 0;
        }
    }

    public void DeathPenaltyStatmod(out StatModifier health, out StatModifier mana)
    {
        health = StatModifier.Default;
        mana = StatModifier.Default;

        if (ServerConfig.Player.DeathPenaltyMode != 1)
            return;

        health.Base -= HealthCache * 5;
        mana.Base -= ManaCache * 5;
    }

    public async Task EnterWorldMessage()
    {
        if (ClientConfig.STFUCHAT) 
            return;

        await Task.Delay(5000);

        if (Player.whoAmI == Main.myPlayer)
        {
            Main.NewText($"{((!LuneLib.LuneLib.instance.ChatSourceLoaded) ? "[LuneWoL] " : "")}If you encounter any bugs IMMEDIATLY REPORT them so i can fix them please please pelase!!!1\nContact me in my discord server, in my discord DMs at iris_lune, or on the mods steam page.\nYou can turn this message off in the client config.", 70, 80, 150);
        }
    }

    public void TundraAppliesChilled()
    {
        if (!ServerConfig.Environment.WeatherEffects)
            return;

        if (!AdvServerConfig.Adv_Environment.WeatherEffects.TundraAppliesChilled || Player.LibPlayer().WearingFullEskimo || Player.HasBuff(BuffID.Campfire) || Player.behindBackWall || Player.HasBuff(BuffID.OnFire) || Player.HasBuff(BuffID.Burning) || Player.HasBuff(BuffID.Warmth))
        {
            _timers.Reset(Timers.TundraChilledCounter);
            Player.LibPlayer().TundraGivesChilled = false;
        }
        else if (Player.ZoneSnow)
        {
            if (_timers.Tick(Timers.TundraChilledCounter, 180))
            {
                Player.LibPlayer().TundraGivesChilled = true;
                Main.buffNoTimeDisplay[BuffID.Chilled] = true;
                Player.AddBuff(BuffID.Chilled, 180, true, false);
            }
        }
        else
        {
            _timers.Reset(Timers.TundraChilledCounter);
            Player.LibPlayer().TundraGivesChilled = false;
        }
    }

    public void UnderworldGivesOnFire()
    {
        if (!ServerConfig.Environment.UnderworldGivesOnFire || !Player.ZoneUnderworldHeight || Player.buffImmune[BuffID.Burning] || Player.fireWalk || Player.buffImmune[BuffID.OnFire] || Player.lavaImmune || Player.wet || (Player.honeyWet && !Player.lavaWet))
            return;

        Main.buffNoTimeDisplay[BuffID.OnFire] = true;
        Player.AddBuff(BuffID.OnFire, 120, false, false);
    }

    public void UpdateConfigState()
    {
        if (!ServerConfig.Environment.MurkyWater)
            return;

        if (AdvClientConfig.ClientDepthPressure.MurkyWaterUpdateTicks == 0 || _timers.Repeat(Timers.UpdateConfig, (uint)AdvClientConfig.ClientDepthPressure.MurkyWaterUpdateTicks))
        {
            _SurfaceDecay = MathHelper.Lerp(AdvServerConfig.Adv_Environment.MurkyWater.SurfaceDecay, 0f, Player.DepthPlayer().lightDepthDiff);
            _CorruptionDecay = MathHelper.Lerp(AdvServerConfig.Adv_Environment.MurkyWater.CorruptionDecay, 0f, Player.DepthPlayer().lightDepthDiff);
            _JungleDecay = MathHelper.Lerp(AdvServerConfig.Adv_Environment.MurkyWater.JungleDecay, 0f, Player.DepthPlayer().lightDepthDiff);
            _HallowDecay = MathHelper.Lerp(AdvServerConfig.Adv_Environment.MurkyWater.HallowDecay, 0f, Player.DepthPlayer().lightDepthDiff);
            _SnowDecay = MathHelper.Lerp(AdvServerConfig.Adv_Environment.MurkyWater.SnowDecay, 0f, Player.DepthPlayer().lightDepthDiff);
            _DesertDecay = MathHelper.Lerp(AdvServerConfig.Adv_Environment.MurkyWater.DesertDecay, 0f, Player.DepthPlayer().lightDepthDiff);
            _BloodMoonDecay = MathHelper.Lerp(AdvServerConfig.Adv_Environment.MurkyWater.BloodMoonDecay, 0f, Player.DepthPlayer().lightDepthDiff);
            _CrimsonDecay = MathHelper.Lerp(AdvServerConfig.Adv_Environment.MurkyWater.CrimsonDecay, 0f, Player.DepthPlayer().lightDepthDiff);
            _UndergroundDesertDecay = MathHelper.Lerp(AdvServerConfig.Adv_Environment.MurkyWater.UndergroundDesertDecay, 0f, Player.DepthPlayer().lightDepthDiff);
            _OceanDecay = MathHelper.Lerp(AdvServerConfig.Adv_Environment.MurkyWater.OceanDecay, 0f, Player.DepthPlayer().lightDepthDiff);
            _ModdedDecay = MathHelper.Lerp(AdvServerConfig.Adv_Environment.MurkyWater.ModdedDecay, 0f, Player.DepthPlayer().lightDepthDiff);
        }
    }

    public void LoadDeathPenaltyTag(TagCompound tag)
    {
        if (ServerConfig.Player.DeathPenaltyMode != 1)
            return;

        LostHealth = tag.GetInt("LostHealth");
        LostMana = tag.GetInt("LostMana");
        HealthCache = tag.GetInt("HealthCache");
        ManaCache = tag.GetInt("ManaCache");
    }

    public void OnlyEnterEvilAtDay()
    {
        if (Main.dayTime || !ServerConfig.Environment.EvilBiomeDoT)
            return;

        if (Player.ZoneCorrupt || Player.ZoneCrimson)
            Player.LibPlayer().InEvilBiomeAtNight = true;
        else
            Player.LibPlayer().InEvilBiomeAtNight = false;
    }

    public void PlrStats()
    {
        LuneWoL_AdvServerConfig.Adv_PlayerPage.PlayerStatPage cfg = AdvServerConfig.Adv_Player.PlayerStats;

        if (cfg.DisablePlayerStatChanges)
            return;

        #region Vitality
        if (cfg.LifePercent != 100)
            Player.statLifeMax2 = Player.statLifeMax2 * cfg.LifePercent / 100;
        if (cfg.LifeRegenPercent != 100)
            Player.lifeRegen = Player.lifeRegen * cfg.LifeRegenPercent / 100;
        if (cfg.DefensePercent != 100)
            Player.statDefense *= cfg.DefensePercent / 100;
        if (cfg.EndurancePercent != 100)
            Player.endurance *= cfg.EndurancePercent / 100;
        #endregion

        #region Offense
        if (cfg.DamagePercent != 100)
            Player.GetDamage(DamageClass.Generic) *= cfg.DamagePercent / 100;
        if (cfg.ArmorPenetrationPercent != 100)
            Player.GetArmorPenetration(DamageClass.Generic) *= cfg.ArmorPenetrationPercent / 100;
        if (cfg.AttackSpeedPercent != 100)
            Player.GetAttackSpeed(DamageClass.Generic) *= cfg.AttackSpeedPercent / 100f;
        #endregion

        #region Magic
        if (cfg.ManaPercent != 100)
            Player.statManaMax2 = Player.statManaMax2 * cfg.ManaPercent / 100;
        if (cfg.ManaRegenPercent != 100)
            Player.manaRegenBonus = Player.manaRegenBonus * cfg.ManaRegenPercent / 100;
        if (cfg.ManaCostPercent != 100)
            Player.manaCost /= cfg.ManaCostPercent / 100;
        #endregion

        #region Slavery
        if (cfg.MaxMinionsPercent != 100)
            Player.maxMinions = Player.maxMinions * cfg.MaxMinionsPercent / 100;
        if (cfg.MaxTurretsPercent != 100)
            Player.maxTurrets = Player.maxTurrets * cfg.MaxTurretsPercent / 100;
        #endregion

        #region Mobility
        if (cfg.MoveSpeedPercent != 100)
            Player.moveSpeed *= cfg.MoveSpeedPercent / 100;
        if (cfg.JumpSpeedPercent != 100)
            Player.jumpSpeed *= cfg.JumpSpeedPercent / 100;
        if (cfg.JumpHeightPercent != 100)
            Player.jumpHeight = Player.jumpHeight * cfg.JumpHeightPercent / 100;
        if (cfg.WingTimePercent != 100)
            Player.wingTimeMax = Player.wingTimeMax * cfg.WingTimePercent / 100;
        #endregion

        #region World Shaping
        if (cfg.PickSpeedPercent != 100)
            Player.pickSpeed /= cfg.PickSpeedPercent / 100;
        if (cfg.TileSpeedPercent != 100)
            Player.tileSpeed *= cfg.TileSpeedPercent / 100;
        if (cfg.WallSpeedPercent != 100)
            Player.wallSpeed *= cfg.WallSpeedPercent / 100;
        #endregion

    }

    public void PoisonedWater()
    {
        LuneWoL_AdvServerConfig.Adv_EnvironmentPage.WaterPoisionPage cfg = AdvServerConfig.Adv_Environment.WaterPoision;

        if (!ServerConfig.Environment.PoisonousWater || !Player.wet || Player.lavaWet || Player.honeyWet)
            return;

        if (Player.ZoneCrimson && cfg.CrimsonIchor)
        {
            ApplyZoneBuff(BuffID.Ichor);
        }
        else if (Player.ZoneCorrupt && cfg.CorruptFlames)
        {
            ApplyZoneBuff(BuffID.CursedInferno);
        }
        else if (Player.ZoneJungle && cfg.JunglePoison)
        {
            ApplyZoneBuff(BuffID.Poisoned);
        }
        else if (Player.ZoneHallow && cfg.HallowConfusion)
        {
            ApplyZoneBuff(BuffID.Confused);
        }
    }

    public void ReciveDeathPenalty(BinaryReader rd)
    {
        if (ServerConfig.Player.DeathPenaltyMode != 1)
            return;

        HealthCache = rd.ReadByte();
        ManaCache = rd.ReadByte();
    }

    public void ResetDeathPenalty()
    {
        if (ServerConfig.Player.DeathPenaltyMode != 1)
            return;

        if (DeathFlag0)
        {
            HealthCache = 0;
            DeathFlag0 = false;
        }
        if (DeathFlag1)
        {
            ManaCache = 0;
            DeathFlag1 = false;
        }
    }

    public void SaveDeathPenaltyTag(TagCompound tag)
    {
        if (ServerConfig.Player.DeathPenaltyMode != 1)
            return;

        tag["LostHealth"] = LostHealth;
        tag["LostMana"] = LostMana;
        tag["HealthCache"] = HealthCache;
        tag["ManaCache"] = ManaCache;
    }

    public void SendDeathPenalty(ModPlayer clientPlayer)
    {
        if (ServerConfig.Player.DeathPenaltyMode != 1)
            return;

        LuneWoL_Plr clone = (LuneWoL_Plr)clientPlayer;
        if (HealthCache != clone.HealthCache || ManaCache != clone.ManaCache)
            SyncPlayer(toWho: -1, fromWho: Main.myPlayer, newPlayer: false);
    }

    public void SyncDeathPenalty(int toWho, int fromWho, bool newPlayer)
    {
        if (ServerConfig.Player.DeathPenaltyMode != 1)
            return;

        ModPacket packet = Mod.GetPacket();
        packet.Write((byte)MessageType.dedsec);
        packet.Write((byte)Player.whoAmI);
        packet.Write((byte)HealthCache);
        packet.Write((byte)ManaCache);
        packet.Send(toWho, fromWho);
    }

    public void ViscousWater()
    {
        if (!ServerConfig.Environment.ViscousWater)
            return;

        if (Player.Submerged())
        {
            Player.velocity.X *= AdvServerConfig.Adv_Environment.ViscousWater.WaterVelocityX;
            Player.velocity.Y *= AdvServerConfig.Adv_Environment.ViscousWater.WaterVelocityY;
        }
    }

    public void WeatherChanges()
    {
        if (!ServerConfig.Environment.WeatherEffects)
            return;

        if (Player.LibPlayer().WearingFullEskimo || Player.HasBuff(BuffID.Campfire) || Player.behindBackWall || Player.HasBuff(BuffID.OnFire) || Player.HasBuff(BuffID.Burning) || Player.HasBuff(BuffID.Warmth))
        {
            _timers.Reset(Timers.TundraBlizzardCounter);
            Player.LibPlayer().BlizzardGivesFrozen = false;

        }
        else if (Main.raining && Player.ZoneSnow && _timers.Tick(Timers.TundraBlizzardCounter, 180) && AdvServerConfig.Adv_Environment.WeatherEffects.BlizzardAppliesFrozen)
        {
                Player.LibPlayer().BlizzardGivesFrozen = true;
                Main.buffNoTimeDisplay[BuffID.Frozen] = true;
                Player.AddBuff(BuffID.Frozen, 60, true, false);
        }
        else
        {
            _timers.Reset(Timers.TundraBlizzardCounter);
            Player.LibPlayer().BlizzardGivesFrozen = false;
        }
    }

    #endregion
}