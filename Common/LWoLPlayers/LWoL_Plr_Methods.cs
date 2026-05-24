
namespace LuneWoL.Common.WoL_Plrs;

public partial class LWoL_Plr : ModPlayer
{
    public void ApplyZoneBuff(int buffId, int duration = 180)
    {
        Main.buffNoTimeDisplay[buffId] = true;
        Player.AddBuff(buffId, duration, true, false);
        if (Player.buffTime[buffId] > duration)
            Player.buffTime[buffId] = duration;
    }

    public void ApplySpaceVacuum()
    {
        if (!LuneWoL.LWoLServerConfig.Environment.SpacePain)
            return;
        if (Player.whoAmI != Main.myPlayer)
            return;
        if (!Player.ZoneSkyHeight)
            return;
        if (Player.behindBackWall)
            return;

        Main.buffNoTimeDisplay[ModContent.BuffType<SpaceVacuum>()] = true;
        Player.AddBuff(ModContent.BuffType<SpaceVacuum>(), 15, true, false);
    }

    public void HellIsHot()
    {
        if (!LuneWoL.LWoLServerConfig.Environment.HellIsHot)
            return;
        if (!Player.ZoneUnderworldHeight)
            return;
        if (Player.buffImmune[BuffID.Burning] || Player.fireWalk || Player.buffImmune[BuffID.OnFire] || Player.lavaImmune || Player.wet || (Player.honeyWet && !Player.lavaWet))
            return;
        Main.buffNoTimeDisplay[BuffID.OnFire] = true;
        Player.AddBuff(BuffID.OnFire, 120, false, false);
    }

    public void ViscousWater()
    {
        if (Player.Submerged() && LuneWoL.LWoLServerConfig.Water.SlowWater && !IrisPlayer)
        {
            if (Player.velocity.Length() > 5f)
            {
                Player.velocity = Vector2.Normalize(Player.velocity) * 5f;
            }
        }
    }

    public void PoisonedWater()
    {
        LWoLAdvancedServerSettings.WaterPoisionDented cfg = LuneWoL.LWoLAdvancedServerSettings.WaterPoision;

        if (!LuneWoL.LWoLServerConfig.Water.WaterPoison)
            return;
        if (!Player.wet || Player.lavaWet || Player.honeyWet)
            return;
        if (IrisPlayer)
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

    public void WeatherChanges()
    {
        if (!LuneWoL.LWoLServerConfig.Environment.WeatherPain)
            return;

        if ((Main.raining && Player.ZoneCrimson) || (Player.ZoneCorrupt && !Player.behindBackWall))
        {
            Main.buffNoTimeDisplay[BuffID.Bleeding] = true;
            Player.AddBuff(BuffID.Bleeding, 60, true, false);
        }

        Player.LibPlayer().StormEyeCovered = Sandstorm.Happening && Player.ZoneDesert && !Player.behindBackWall;
        Player.blackout = Player.LibPlayer().StormEyeCovered;

        if (!Player.LibPlayer().WearingFullEskimo && Main.raining && Player.ZoneSnow && !Player.behindBackWall && !Player.HasBuff(BuffID.Campfire))
        {
            if (TundraBlizzardCounter < 0)
                TundraBlizzardCounter = 0;

            TundraBlizzardCounter += 1;

            if (TundraBlizzardCounter >= 180)
                TundraBlizzardCounter = 180;

            if (TundraBlizzardCounter >= 180)
            {
                Player.LibPlayer().BlizzardFrozen = true;
                Main.buffNoTimeDisplay[BuffID.Frozen] = true;
                Player.AddBuff(BuffID.Frozen, 60, true, false);
            }
        }
        else
        {
            TundraBlizzardCounter = 0;
            Player.LibPlayer().BlizzardFrozen = false;
        }
    }

    public void FreezingTundra()
    {
        if (!LuneWoL.LWoLServerConfig.Environment.Chilly)
            return;

        if (Player.LibPlayer().WearingFullEskimo)
            return;

        if (Player.ZoneSnow
            && !Player.HasBuff(BuffID.Campfire)
            && !Player.behindBackWall
            && !Player.HasBuff(BuffID.OnFire)
            && !Player.HasBuff(BuffID.Burning)
            && !Player.HasBuff(BuffID.Warmth))
        {
            if (TundraChilledCounter < 0)
                TundraChilledCounter = 0;

            TundraChilledCounter += 1;

            if (TundraChilledCounter >= 180)
                TundraChilledCounter = 180;

            if (TundraChilledCounter >= 180)
            {
                Player.LibPlayer().Chilly = true;
                Main.buffNoTimeDisplay[BuffID.Chilled] = true;
                Player.AddBuff(BuffID.Chilled, 180, true, false);
            }
        }
        else
        {
            TundraChilledCounter = 0;
            Player.LibPlayer().Chilly = false;
        }
    }

    public void OnlyEnterEvilAtDay()
    {
        if (Main.dayTime)
            return;

        if (!LuneWoL.LWoLServerConfig.Environment.NoEvilDayTime)
            return;

        Player.LibPlayer().CrimtuptionzoneNight = Player.ZoneCorrupt || Player.ZoneCrimson;
    }

    public class Windproj : GlobalProjectile
    {
        public override void PostAI(Projectile Projectile)
        {
            if (LuneWoL.LWoLServerConfig.Environment.WindArrows && Projectile.arrow &&
                Projectile.Center.Y < Main.worldSurface * 16.0
                && Main.tile[(int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16] != null
                && Main.tile[(int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16].WallType == WallID.None
                && ((Projectile.velocity.X > 0f
                && Main.windSpeedCurrent < 0f)
                || (Projectile.velocity.X < 0f
                && Main.windSpeedCurrent > 0f)
                || Math.Abs(Projectile.velocity.X) < Math.Abs(Main.windSpeedCurrent * Main.windPhysicsStrength) * 180f)
                && Math.Abs(Projectile.velocity.X) < 16f)
            {
                Projectile.velocity.X += Main.windSpeedCurrent * Main.windPhysicsStrength;
                Projectile.velocity.X = MathHelper.Clamp(Projectile.velocity.X, -16f, 16f);
                ;
            }
            base.PostAI(Projectile);
        }
    }

    public void MurkyWater()
    {
        if (IrisPlayer)
            return;
        if (Player.whoAmI != Main.myPlayer)
            return;

        LWoLServerConfig.WaterDented Config = LuneWoL.LWoLServerConfig.Water;
        
        if (Player.Submerged() && Config.DarkWaters && Config.DepthPressureMode == 0)
        {
            ScreenObstruction.screenObstruction = MathHelper.Lerp(ScreenObstruction.screenObstruction, 1f, LuneWoL.LWoLAdvancedServerSettings.DarkWaters.DarkWaterIntensity);
        }
        else if (Player.Submerged() && Config.DarkWaters && Config.DepthPressureMode != 0)
        {
            Player.LibPlayer().WaterEyes = true;
            Lighting.GlobalBrightness *= 0.8f;
        }
        else
        {
            Player.LibPlayer().WaterEyes = false;
        }
    }

    public void DeathPenaltyConsumedCrystals()
    {
        if (LuneWoL.LWoLServerConfig.LPlayer.DeathPenaltyMode != 2)
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
        if (LuneWoL.LWoLServerConfig.LPlayer.DeathPenaltyMode != 3)
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

    public void DeathPenaltyAppliedOnRespawn()
    {
        if (LuneWoL.LWoLServerConfig.LPlayer.DeathPenaltyMode != 1)
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

    public void ResetDeathPenalty()
    {
        if (LuneWoL.LWoLServerConfig.LPlayer.DeathPenaltyMode != 1)
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

    public void DeathPenaltyStatmod(out StatModifier health, out StatModifier mana)
    {
        health = StatModifier.Default;
        mana = StatModifier.Default;

        if (LuneWoL.LWoLServerConfig.LPlayer.DeathPenaltyMode != 1)
            return;

        health.Base -= HealthCache * 5;
        mana.Base -= ManaCache * 5;
    }

    public void ReciveDeathPenalty(BinaryReader rd)
    {
        if (LuneWoL.LWoLServerConfig.LPlayer.DeathPenaltyMode != 1)
            return;

        HealthCache = rd.ReadByte();
        ManaCache = rd.ReadByte();
    }

    public void SyncDeathPenalty(int toWho, int fromWho, bool newPlayer)
    {
        if (LuneWoL.LWoLServerConfig.LPlayer.DeathPenaltyMode != 1)
            return;

        ModPacket packet = Mod.GetPacket();
        packet.Write((byte)MessageType.dedsec);
        packet.Write((byte)Player.whoAmI);
        packet.Write((byte)HealthCache);
        packet.Write((byte)ManaCache);
        packet.Send(toWho, fromWho);
    }

    public void CloneClientsDeathPenalty(ModPlayer targetCopy)
    {
        if (LuneWoL.LWoLServerConfig.LPlayer.DeathPenaltyMode != 1)
            return;

        LWoL_Plr clone = (LWoL_Plr)targetCopy;
        clone.HealthCache = HealthCache;
        clone.ManaCache = ManaCache;
    }

    public void SendDeathPenalty(ModPlayer clientPlayer)
    {
        if (LuneWoL.LWoLServerConfig.LPlayer.DeathPenaltyMode != 1)
            return;

        LWoL_Plr clone = (LWoL_Plr)clientPlayer;

        if (HealthCache != clone.HealthCache || ManaCache != clone.ManaCache)
            SyncPlayer(toWho: -1, fromWho: Main.myPlayer, newPlayer: false);
    }

    public void SaveDeathPenaltyTag(TagCompound tag)
    {
        if (LuneWoL.LWoLServerConfig.LPlayer.DeathPenaltyMode != 1)
            return;

        tag["LostHealth"] = LostHealth;
        tag["LostMana"] = LostMana;
        tag["HealthCache"] = HealthCache;
        tag["ManaCache"] = ManaCache;
    }

    public void LoadDeathPenaltyTag(TagCompound tag)
    {
        if (LuneWoL.LWoLServerConfig.LPlayer.DeathPenaltyMode != 1)
            return;

        LostHealth = tag.GetInt("LostHealth");
        LostMana = tag.GetInt("LostMana");
        HealthCache = tag.GetInt("HealthCache");
        ManaCache = tag.GetInt("ManaCache");
    }
}
