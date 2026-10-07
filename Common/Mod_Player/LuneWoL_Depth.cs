namespace LuneWoL.Common.Mod_Player;

#region Depth

internal class LuneWoL_Depth : ModPlayer
{
    public bool InWaterBody { get; set; }

    private enum Once
    {
        DrownWarning
    }
    private readonly RunOneTimeLib<Once> _once = new();

    public static bool Disabled => ServerConfig.Environment.DepthPressureMode == 0;

    public static bool UsingModeOne => ServerConfig.Environment.DepthPressureMode == 1;

    public static bool UsingModeTwo => ServerConfig.Environment.DepthPressureMode == 2;

    public int breathCooldown, maxDepth, pressureDamageToApply, reducedDepthDiff;
    public float reducedDepth, lightDepthDiff, tileDiffCalced, tileDiff, entryY;

    public Depth_I ModeOnePlayer => Player.GetModPlayer<Depth_I>();

    public Depth_II ModeTwoPlayer => Player.GetModPlayer<Depth_II>();

    [JITWhenModsEnabled("LuneLibAssets")] //private mod with copyrighted content. you cant has this, sorry :c
    public void CopyrightSound() => SoundEngine.PlaySound(DrownSound, Player.Center);

    public void Sound() => SoundEngine.PlaySound(SoundID.Drown, Player.Center);

    public void DamageChecker()
    {
        if (Disabled)
            return;
        LibPlayer pLib = Player.LibPlayer();

        LuneWoL_AdvServerConfig.Adv_EnvironmentPage.ServerDepthPressurePage cfg = AdvServerConfig.Adv_Environment.ServerDepthPressure;
        LuneWoL_AdvServerConfig.Adv_EnvironmentPage.ServerDepthPressurePage.LifeResistPage Lcfg = AdvServerConfig.Adv_Environment.ServerDepthPressure.LifeResistValues;

        if (reducedDepthDiff >= maxDepth)
        {
            double depthPastMax = Math.Max(reducedDepthDiff - maxDepth, 0.0);
            double lifeTier = Math.Max(Math.Floor(depthPastMax / cfg.LifeLossTileInterval), 1.0);
            double perTileRate = cfg.LifeLossPerTile + (cfg.LifeLossPerInterval * (lifeTier - 1));
            double baseLifeLoss = cfg.BaseLifelossRate + (perTileRate * depthPastMax);

            int lifeLossResist = 0;
            if (LuneLib.LuneLib.instance.CalamityModLoaded)
            {
                if (pLib.WearingAbyssalDivingSuit) lifeLossResist = Lcfg.LifelossResistAbyssalDivingSuit;
                else if (pLib.WearingAbyssalDivingGear) lifeLossResist = Lcfg.LifelossResistAbyssalDivingGear;
                else if (pLib.WearingArcticDivingGear) lifeLossResist = Lcfg.LifelossResistArcticDivingGear;
                else if (pLib.WearingJellyfishDivingGear) lifeLossResist = Lcfg.LifelossResistJellyfishDivingGear;
                else if (pLib.WearingDivingGear) lifeLossResist = Lcfg.LifelossResistDivingGear;
                else if (pLib.WearingDivingHelm) lifeLossResist = Lcfg.LifelossResistDivingHelm;
                else if (Player.accMerman) lifeLossResist = Lcfg.LifelossResistMerman;
                else if (Player.gills) lifeLossResist = Lcfg.LifelossResistGills;
            }
            else
            {
                if (pLib.WearingArcticDivingGear) lifeLossResist = Lcfg.LifelossResistArcticDivingGear;
                else if (pLib.WearingJellyfishDivingGear) lifeLossResist = Lcfg.LifelossResistJellyfishDivingGear;
                else if (pLib.WearingDivingGear) lifeLossResist = Lcfg.LifelossResistDivingGear;
                else if (pLib.WearingDivingHelm) lifeLossResist = Lcfg.LifelossResistDivingHelm;
                else if (Player.accMerman) lifeLossResist = Lcfg.LifelossResistMerman;
                else if (Player.gills) lifeLossResist = Lcfg.LifelossResistGills;
            }

            if (LuneWoL.AdvClientConfig.ClientDepthPressure.DebugText)
                Main.NewText($"baseLifeLoss={baseLifeLoss:F2}, lifeLossResist={lifeLossResist}, depthPastMax={depthPastMax:F2}, lifeTier={lifeTier:F2}");

            pressureDamageToApply = Math.Max((int)(baseLifeLoss - lifeLossResist), 0);

            Player.LibPlayer().DepthWaterPressure = true;
            Player.LibPlayer().CurrentDepthPressure = pressureDamageToApply;
        }
        else
        {
            Player.LibPlayer().DepthWaterPressure = false;
            Player.LibPlayer().CurrentDepthPressure = 0;
        }
    }

    private void BreathChecker()
    {
        LuneLib.Common.Players.LuneLibPlayer.LibPlayer pLib = Player.LibPlayer();

        LuneWoL_AdvServerConfig.Adv_EnvironmentPage.ServerDepthPressurePage cfg = AdvServerConfig.Adv_Environment.ServerDepthPressure;
        LuneWoL_AdvServerConfig.Adv_EnvironmentPage.ServerDepthPressurePage.StairValuesPage Scfg = AdvServerConfig.Adv_Environment.ServerDepthPressure.StairValues;
        LuneWoL_AdvServerConfig.Adv_EnvironmentPage.ServerDepthPressurePage.BreathValuesPage Bcfg = AdvServerConfig.Adv_Environment.ServerDepthPressure.BreathValues;
        LuneWoL_AdvServerConfig.Adv_EnvironmentPage.ServerDepthPressurePage.TickValuesPage Tcfg = AdvServerConfig.Adv_Environment.ServerDepthPressure.TickValues;

        float breathLossMult = 1f;
        float tickRateMult = 0f;
        int stepSize = cfg.BaseStepSize;
        if (LuneLib.LuneLib.instance.CalamityModLoaded)
        {
            if (pLib.WearingAbyssalDivingSuit)
            {
                stepSize += Scfg.StairAbyssalDivingSuit;
                breathLossMult = Bcfg.BreathlossAbyssalDivingSuit;
                tickRateMult = Tcfg.TickRateAbyssalDivingSuit;
            }
            else if (pLib.WearingAbyssalDivingGear)
            {
                stepSize += Scfg.StairAbyssalDivingGear;
                breathLossMult = Bcfg.BreathlossAbyssalDivingGear;
                tickRateMult = Tcfg.TickRateAbyssalDivingGear;
            }
            else if (pLib.WearingArcticDivingGear)
            {
                stepSize += Scfg.StairArcticDivingGear;
                breathLossMult = Bcfg.BreathlossArcticDivingGear;
                tickRateMult = Tcfg.TickRateArcticDivingGear;
            }
            else if (pLib.WearingJellyfishDivingGear)
            {
                stepSize += Scfg.StairJellyfishDivingGear;
                breathLossMult = Bcfg.BreathlossJellyfishDivingGear;
                tickRateMult = Tcfg.TickRateJellyfishDivingGear;
            }
            else if (pLib.WearingDivingGear)
            {
                stepSize += Scfg.StairDivingGear;
                breathLossMult = Bcfg.BreathlossDivingGear;
                tickRateMult = Tcfg.TickRateDivingGear;
            }
            else if (pLib.WearingDivingHelm)
            {
                stepSize += Scfg.StairDivingHelm;
                breathLossMult = Bcfg.BreathlossDivingHelm;
                tickRateMult = Tcfg.TickRateDivingHelm;
            }
            else if (Player.accMerman)
            {
                stepSize += Scfg.StairMerman;
                breathLossMult = Bcfg.BreathlossMerman;
                tickRateMult = Tcfg.TickRateMerman;
            }
            else if (Player.gills)
            {
                stepSize += Scfg.StairGills;
                breathLossMult = Bcfg.BreathlossGills;
                tickRateMult = Tcfg.TickRateGills;
            }
        }
        else
        {
            if (pLib.WearingArcticDivingGear)
            {
                stepSize += Scfg.StairArcticDivingGear;
                breathLossMult = Bcfg.BreathlossArcticDivingGear;
                tickRateMult = Tcfg.TickRateArcticDivingGear;
            }
            else if (pLib.WearingJellyfishDivingGear)
            {
                stepSize += Scfg.StairJellyfishDivingGear;
                breathLossMult = Bcfg.BreathlossJellyfishDivingGear;
                tickRateMult = Tcfg.TickRateJellyfishDivingGear;
            }
            else if (pLib.WearingDivingGear)
            {
                stepSize += Scfg.StairDivingGear;
                breathLossMult = Bcfg.BreathlossDivingGear;
                tickRateMult = Tcfg.TickRateDivingGear;
            }
            else if (pLib.WearingDivingHelm)
            {
                stepSize += Scfg.StairDivingHelm;
                breathLossMult = Bcfg.BreathlossDivingHelm;
                tickRateMult = Tcfg.TickRateDivingHelm;
            }
            else if (Player.accMerman)
            {
                stepSize += Scfg.StairMerman;
                breathLossMult = Bcfg.BreathlossMerman;
                tickRateMult = Tcfg.TickRateMerman;
            }
            else if (Player.gills)
            {
                stepSize += Scfg.StairGills;
                breathLossMult = Bcfg.BreathlossGills;
                tickRateMult = Tcfg.TickRateGills;
            }
        }

        double tier = Math.Max(Math.Floor(tileDiff / stepSize), 1.0);
        double breathLoss = (cfg.BaseBreathAmount + (cfg.BreathLossPerTile * tileDiff * tier)) * breathLossMult;
        double tickRate = Math.Max((cfg.BaseTickRate - (cfg.TickReductionPerTile * tileDiff * tier)) * (1f + tickRateMult), 1.0);

        breathCooldown++;
        if (breathCooldown >= (int)tickRate && tileDiff >= 2.0)
        {
            breathCooldown = 0;

            if (Player.breath > 0)
            {
                int breathToSubtract = (int)(breathLoss + 1.0);
                Player.breath -= breathToSubtract;
                if (Player.breath < 0)
                    Player.breath = 0;
            }
        }

        if (LuneWoL.AdvClientConfig.ClientDepthPressure.DebugText)
            Main.NewText($"stepSize={stepSize}, tier={tier}, breathLoss={breathLoss:F2}, tickRate={tickRate:F2}, lifeloss={pressureDamageToApply}, MaxDepth={maxDepth}, CurrentDepth={(Player.Center.Y - entryY) / 16f}, reducedDiff{reducedDepthDiff}");

        if (Player.statLife <= 0)
            KillPlayer();
    }

    public void KillPlayer()
    {
        IEntitySource source_Death = Player.GetSource_Death();
        Player.lastDeathPostion = Player.Center;
        Player.lastDeathTime = DateTime.Now;
        Player.showLastDeath = true;
        int num = (int)Utils.CoinsCount(out bool overFlowing, Player.inventory);
        if (Main.myPlayer == Player.whoAmI)
        {
            Player.lostCoins = num;
            Player.lostCoinString = Main.ValueToCoins(Player.lostCoins);
            Main.mapFullscreen = false;
            Player.trashItem.SetDefaults(ItemID.None, noMatCheck: false, null);
            if (Player.difficulty == 0 || Player.difficulty == 3)
                for (int i = 0; i < 59; i++)
                    if (Player.inventory[i].stack > 0 && ((Player.inventory[i].type >= ItemID.LargeAmethyst && Player.inventory[i].type <= ItemID.LargeDiamond) || Player.inventory[i].type == ItemID.LargeAmber))
                    {
                        int num2 = Item.NewItem(source_Death, (int)Player.position.X, (int)Player.position.Y, Player.width, Player.height, Player.inventory[i].type);
                        Main.item[num2].netDefaults(Player.inventory[i].netID);
                        Main.item[num2].Prefix(Player.inventory[i].prefix);
                        Main.item[num2].stack = Player.inventory[i].stack;
                        Main.item[num2].velocity.Y = Main.rand.Next(-20, 1) * 0.2f;
                        Main.item[num2].velocity.X = Main.rand.Next(-20, 21) * 0.2f;
                        Main.item[num2].noGrabDelay = 100;
                        Main.item[num2].favorited = false;
                        Main.item[num2].newAndShiny = false;
                        if (Main.netMode == NetmodeID.MultiplayerClient)
                            NetMessage.SendData(MessageID.SyncItem, -1, -1, null, num2);
                        Player.inventory[i].SetDefaults(ItemID.None, noMatCheck: false, null);
                    }
                    else if (Player.difficulty == 1)
                        Player.DropItems();
                    else if (Player.difficulty == 2)
                    {
                        Player.DropItems();
                        Player.KillMeForGood();
                    }
        }
        SoundEngine.PlaySound(in SoundID.PlayerKilled, Player.Center);
        Player.headVelocity.Y = Main.rand.Next(-40, -10) * 0.1f;
        Player.bodyVelocity.Y = Main.rand.Next(-40, -10) * 0.1f;
        Player.legVelocity.Y = Main.rand.Next(-40, -10) * 0.1f;
        Player.headVelocity.X = (Main.rand.Next(-20, 21) * 0.1f) + 0f;
        Player.bodyVelocity.X = (Main.rand.Next(-20, 21) * 0.1f) + 0f;
        Player.legVelocity.X = (Main.rand.Next(-20, 21) * 0.1f) + 0f;
        if (Player.stoned)
        {
            Player.headPosition = Vector2.Zero;
            Player.bodyPosition = Vector2.Zero;
            Player.legPosition = Vector2.Zero;
        }
        for (int j = 0; j < 100; j++)
            Dust.NewDust(Player.position, Player.width, Player.height, DustID.LifeDrain, 0f, -2f);
        Player.mount.Dismount(Player);
        Player.dead = true;
        Player.respawnTimer = 600;
        if (Main.expertMode)
            Player.respawnTimer = (int)(Player.respawnTimer * 1.5);
        Player.immuneAlpha = 0;
        Player.palladiumRegen = false;
        Player.iceBarrier = false;
        Player.crystalLeaf = false;
        PlayerDeathReason playerDeathReason = PlayerDeathReason.ByOther(Player.Male ? 14 : 15);

        if (reducedDepthDiff > maxDepth + 50 && Player.LibPlayer().DepthWaterPressure && Player.Submerged() && Player.whoAmI == Main.myPlayer)
        {
            if (LuneLib.LuneLib.instance.LuneLibAssetsLoaded)
                CopyrightSound();
            else
                Sound();
            playerDeathReason = PlayerDeathReason.ByCustomReason(GetText("Status.Death.PressureDeathTooDeep").ToNetworkText(Player.name));
        }
        else if (tileDiff >= 50 && Player.Submerged() && Player.whoAmI == Main.myPlayer)
        {
            if (LuneLib.LuneLib.instance.LuneLibAssetsLoaded)
                CopyrightSound();
            else
                Sound();
            playerDeathReason = PlayerDeathReason.ByCustomReason(GetText("Status.Death.PressureDeath" + Main.rand.Next(1, 10 + 1)).ToNetworkText(Player.name));
        }
        else if (Player.breath <= 6 && Player.Submerged() && Player.whoAmI == Main.myPlayer)
        {
            Sound();
            playerDeathReason = PlayerDeathReason.ByOther(1);
        }
        NetworkText deathText = playerDeathReason.GetDeathText(Player.name);

        if (Main.netMode == NetmodeID.MultiplayerClient && Player.whoAmI == Main.myPlayer)
            NetMessage.SendPlayerDeath(Player.whoAmI, playerDeathReason, 1000, 0, pvp: false);
        if (Main.netMode == NetmodeID.Server)
            ChatHelper.BroadcastChatMessage(deathText, new Color(225, 25, 25));
        else if (Main.netMode == NetmodeID.SinglePlayer)
            Main.NewText(deathText.ToString(), 225, 25, 25);
        if (Player.whoAmI == Main.myPlayer && (Player.difficulty == 0 || Player.difficulty == 3))
            Player.DropCoins();
        Player.DropTombstone(num, deathText, 0);
        if (Player.whoAmI == Main.myPlayer)
            try
            {
                WorldGen.saveToonWhilePlaying();
            }
            catch
            {
            }
    }

    public int CalcMaxDepth()
    {
        LuneWoL_AdvServerConfig.Adv_EnvironmentPage.ServerDepthPressurePage cfg = AdvServerConfig.Adv_Environment.ServerDepthPressure;
        LuneWoL_AdvServerConfig.Adv_EnvironmentPage.ServerDepthPressurePage.MaxDepthPage Dcfg = AdvServerConfig.Adv_Environment.ServerDepthPressure.MaxDepthValues;
        maxDepth = cfg.BaseMaxDepth;

        LuneLib.Common.Players.LuneLibPlayer.LibPlayer pLib = Player.LibPlayer();

        if (LuneLib.LuneLib.instance.CalamityModLoaded)
        {
            if (pLib.WearingAbyssalDivingSuit) maxDepth = (int)(cfg.BaseMaxDepth * (1f + Dcfg.MaxDepthAbyssalDivingSuit));
            else if (pLib.WearingAbyssalDivingGear) maxDepth = (int)(cfg.BaseMaxDepth * (1f + Dcfg.MaxDepthAbyssalDivingGear));
            else if (pLib.WearingArcticDivingGear) maxDepth = (int)(cfg.BaseMaxDepth * (1f + Dcfg.MaxDepthArcticDivingGear));
            else if (pLib.WearingJellyfishDivingGear) maxDepth = (int)(cfg.BaseMaxDepth * (1f + Dcfg.MaxDepthJellyfishDivingGear));
            else if (pLib.WearingDivingGear) maxDepth = (int)(cfg.BaseMaxDepth * (1f + Dcfg.MaxDepthDivingGear));
            else if (pLib.WearingDivingHelm) maxDepth = (int)(cfg.BaseMaxDepth * (1f + Dcfg.MaxDepthDivingHelm));
            else if (Player.accMerman) maxDepth = (int)(cfg.BaseMaxDepth * (1f + Dcfg.MaxDepthMerman));
            else if (Player.gills) maxDepth = (int)(cfg.BaseMaxDepth * (1f + Dcfg.MaxDepthGills));
        }
        else
        {
            if (pLib.WearingArcticDivingGear) maxDepth = (int)(cfg.BaseMaxDepth * (1f + Dcfg.MaxDepthArcticDivingGear));
            else if (pLib.WearingJellyfishDivingGear) maxDepth = (int)(cfg.BaseMaxDepth * (1f + Dcfg.MaxDepthJellyfishDivingGear));
            else if (pLib.WearingDivingGear) maxDepth = (int)(cfg.BaseMaxDepth * (1f + Dcfg.MaxDepthDivingGear));
            else if (pLib.WearingDivingHelm) maxDepth = (int)(cfg.BaseMaxDepth * (1f + Dcfg.MaxDepthDivingHelm));
            else if (Player.accMerman) maxDepth = (int)(cfg.BaseMaxDepth * (1f + Dcfg.MaxDepthMerman));
            else if (Player.gills) maxDepth = (int)(cfg.BaseMaxDepth * (1f + Dcfg.MaxDepthGills));
        }


        return maxDepth;
    }

    public float CalcReducedDepth()
    {
        LuneWoL_AdvServerConfig.Adv_EnvironmentPage.ServerDepthPressurePage.MaxDepthPage cfg = AdvServerConfig.Adv_Environment.ServerDepthPressure.MaxDepthValues;
        reducedDepth = 1f;

        LuneLib.Common.Players.LuneLibPlayer.LibPlayer pLib = Player.LibPlayer();

        if (LuneLib.LuneLib.instance.CalamityModLoaded)
        {
            if (pLib.WearingAbyssalDivingSuit) reducedDepth -= cfg.MaxDepthAbyssalDivingSuit;
            else if (pLib.WearingAbyssalDivingGear) reducedDepth -= cfg.MaxDepthAbyssalDivingGear;
            else if (pLib.WearingArcticDivingGear) reducedDepth -= cfg.MaxDepthArcticDivingGear;
            else if (pLib.WearingJellyfishDivingGear) reducedDepth -= cfg.MaxDepthJellyfishDivingGear;
            else if (pLib.WearingDivingGear) reducedDepth -= cfg.MaxDepthDivingGear;
            else if (pLib.WearingDivingHelm) reducedDepth -= cfg.MaxDepthDivingHelm;
            else if (Player.accMerman) reducedDepth -= cfg.MaxDepthMerman;
            else if (Player.gills) reducedDepth -= cfg.MaxDepthGills;
        }
        else
        {
            if (pLib.WearingArcticDivingGear) reducedDepth -= cfg.MaxDepthArcticDivingGear;
            else if (pLib.WearingJellyfishDivingGear) reducedDepth -= cfg.MaxDepthJellyfishDivingGear;
            else if (pLib.WearingDivingGear) reducedDepth -= cfg.MaxDepthDivingGear;
            else if (pLib.WearingDivingHelm) reducedDepth -= cfg.MaxDepthDivingHelm;
            else if (Player.accMerman) reducedDepth -= cfg.MaxDepthMerman;
            else if (Player.gills) reducedDepth -= cfg.MaxDepthGills;
        }

        if (reducedDepth <= 0.25f)
            reducedDepth = 0.25f;

        return reducedDepth;
    }

    public float CalcTileDiff()
    {
        tileDiff = Math.Max((Player.Center.Y - entryY) / 16f, 0f);
        return tileDiff;
    }

    public int CalcReducedTileDiff()
    {
        reducedDepthDiff = (int)(tileDiff * reducedDepth);
        if (reducedDepthDiff < 0)
            reducedDepthDiff = 0;
        return reducedDepthDiff;
    }

    public float CalcTileDiffClamped()
    {
        tileDiffCalced = Math.Clamp(reducedDepthDiff, 0, maxDepth);
        return tileDiffCalced;
    }

    public float CalcLightDepthDiff()
    {
        lightDepthDiff = maxDepth == 0 ? 0f : tileDiffCalced / maxDepth;
        return lightDepthDiff;
    }

    private void UpdateWaterState()
    {
        if (UsingModeOne)
        {
            ModeOnePlayer.CheckWaterDepth();
            entryY = ModeOnePlayer.EntryPoint.Y;
        }
        else if (UsingModeTwo)
            entryY = ModeTwoPlayer._topY * 16f;
    }

    public override void PostUpdateMiscEffects()
    {
        if (Disabled || Player.whoAmI != Main.myPlayer)
            return;

        UpdateWaterState();
    }

    public override void PostUpdateEquips()
    {
        if (Disabled || Player.whoAmI != Main.myPlayer || entryY < 0 && UsingModeTwo)
            return;

        if (Player.Submerged())
        {
            CalcMaxDepth();
            CalcReducedDepth();
            CalcTileDiff();
            CalcReducedTileDiff();
            CalcTileDiffClamped();
            CalcLightDepthDiff();

            BreathChecker();
            DamageChecker();

            LuneWoL_AdvServerConfig.Adv_EnvironmentPage.ServerDepthPressurePage cfg = AdvServerConfig.Adv_Environment.ServerDepthPressure;

            bool hasWaterBreathing = Player.gills || Player.merman || Player.accMerman;

            if (hasWaterBreathing)
            {
                bool inAbyss = LuneLib.LuneLib.instance.CalamityModLoaded && Player.ZoneAbyss();

                if (!inAbyss)
                {
                    if (Player.breath > 6)
                        Player.breath -= 3;
                }

                if (Player.breath > 6)
                    _once.Reset(Once.DrownWarning);
                else
                {
                    if (_once.Once(Once.DrownWarning))
                        SoundEngine.PlaySound(SoundID.Drown, Player.Center);

                    Player.breath = 0;
                    Player.lifeRegenTime = 0f;

                    breathCooldown++;
                    if (breathCooldown >= Player.breathCDMax)
                    {
                        breathCooldown = 0;
                        Player.statLife -= 2;

                        if (Player.statLife <= 0)
                        {
                            Player.statLife = 0;
                            KillPlayer();
                        }
                    }
                }
            }
        }
    }

    public override void PostUpdate()
    {
        if (Disabled || Player.whoAmI != Main.myPlayer || entryY < 0 && UsingModeTwo || !Player.Submerged() || !ServerConfig.Environment.MurkyWater)
            return;

        lightDepthDiff *= AdvServerConfig.Adv_Environment.ServerDepthPressure.DepthDarknessIntensity;

        float reversed = 1f - lightDepthDiff;
        float clamped = MathHelper.Clamp(reversed, 0.5f, 1f);
        Lighting.GlobalBrightness *= clamped;
    }
}

#endregion

#region Mode1

public class Depth_I : ModPlayer
{
    public bool WasDrowningLastFrame { get; set; }

    public Vector2 EntryPoint { get; set; }
    public Vector2 ExitPoint { get; set; }

    public bool IsDrowning { get; set; }

    public void CheckWaterDepth()
    {
        bool currentlyDrowning = Collision.DrownCollision(Main.LocalPlayer.position, Main.LocalPlayer.width, Main.LocalPlayer.height, Main.LocalPlayer.gravDir);

        if (currentlyDrowning && !WasDrowningLastFrame && !Player.DepthPlayer().InWaterBody)
        {
            Player.DepthPlayer().InWaterBody = true;
            EntryPoint = Main.LocalPlayer.position;
        }
        else if (currentlyDrowning && Player.DepthPlayer().InWaterBody && Main.LocalPlayer.position.Y < EntryPoint.Y)
        {
            EntryPoint = Main.LocalPlayer.position;
        }
        else if (!currentlyDrowning && WasDrowningLastFrame)
        {
            ExitPoint = Main.LocalPlayer.position;
            Player.DepthPlayer().InWaterBody = true;
        }

        if (!currentlyDrowning && Player.DepthPlayer().InWaterBody)
        {
            if (Vector2.Distance(Player.position, ExitPoint) >= 240f)
                Player.DepthPlayer().InWaterBody = false;
        }

        if (!Player.DepthPlayer().InWaterBody && Vector2.Distance(Main.LocalPlayer.position, ExitPoint) > 240f)
        {
            EntryPoint = Main.LocalPlayer.position;
            ExitPoint = Main.LocalPlayer.position;
        }

        WasDrowningLastFrame = currentlyDrowning;
    }
}

#endregion

#region Mode2

public class Depth_II : ModPlayer
{
    private int _timer;

    public int _topY { get; set; } = -1;

    private static int _worldWidth => Main.maxTilesX;

    private static int _worldHeight => Main.maxTilesY;

    internal HashSet<int> _visited;
    private Queue<int> _queue;
    private int _surfaceY;
    private bool _running;
    private int _scannedY;

    public override void Initialize()
    {
        _timer = 0;
        Player.DepthPlayer().InWaterBody = false;
        _topY = -1;
        _running = false;
        _visited = null;
        _queue = null;
    }

    private void Start(int startX, int startY)
    {
        _visited = new HashSet<int>(capacity: 4096);
        _queue = new Queue<int>();
        _surfaceY = int.MaxValue;
        _scannedY = startY;

        int i = (startX << 16) | (startY & 0xFFFF);
        _visited.Add(i);
        _queue.Enqueue(i);
        _running = true;
    }

    private bool Step(int budget)
    {
        int scanned = 0;

        while (_queue.Count > 0 && (budget == -1 || scanned < budget))
        {
            scanned++;
            int i = _queue.Dequeue();
            int x = i >> 16;
            int y = i & 0xFFFF;

            Tile tile = Main.tile[x, y];
            if (tile == null || tile.LiquidType != LiquidID.Water || tile.LiquidAmount == 0)
                continue;

            if (y < _scannedY)
                _scannedY = y;

            if (y > 0)
            {
                Tile above = Main.tile[x, y - 1];
                if (above == null || above.LiquidAmount == 0)
                    if (y < _surfaceY)
                    {
                        _surfaceY = y;
                    }
            }
            else if (0 < _surfaceY)
            {
                _surfaceY = 0;
            }

            void QueueWater(int x, int y)
            {
                if (IsWater(x, y))
                {
                    int p = (x << 16) | y;
                    if (!_visited.Contains(p))
                    {
                        _visited.Add(p);
                        _queue.Enqueue(p);
                    }
                }
            }

            if (x > 0) QueueWater(x - 1, y);
            if (x + 1 < _worldWidth) QueueWater(x + 1, y);
            if (y > 0) QueueWater(x, y - 1);
            if (y + 1 < _worldHeight) QueueWater(x, y + 1);
        }

        if (_queue.Count == 0)
        {
            _running = false;
            return true;
        }

        return false;
    }

    private bool IsWater(int x, int y)
    {
        if (x < 0 || x >= _worldWidth || y < 0 || y >= _worldHeight)
            return false;

        Tile t = Main.tile[x, y];
        return t.LiquidType == LiquidID.Water && t.LiquidAmount > 0;
    }

    private Point? FindStartingWaterTile()
    {
        int x = (int)(Player.Center.X / 16f);
        int y = (int)(Player.Center.Y / 16f);

        if (IsWater(x, y))
            return new Point(x, y);

        for (int _x = -1; _x <= 1; _x++)
            for (int _y = -1; _y <= 1; _y++)
            {
                if (_x == 0 && _y == 0)
                    continue;
                int newx = x + _x;
                int newy = y + _y;
                if (IsWater(newx, newy))
                    return new Point(newx, newy);
            }

        return null;
    }

    private void RescanSurface()
    {
        if (_visited == null)
            return;

        int newSurfaceY = int.MaxValue;

        foreach (int i in _visited)
        {
            int x = i >> 16;
            int y = i & 0xFFFF;

            if (y > _surfaceY + 1)
                continue;

            if (!IsWater(x, y))
                continue;

            if (y > 0)
            {
                Tile above = Main.tile[x, y - 1];
                if (above == null || above.LiquidAmount == 0)
                    if (y < newSurfaceY)
                        newSurfaceY = y;
            }
            else if (0 < newSurfaceY)
                newSurfaceY = 0;
        }

        if (newSurfaceY == int.MaxValue)
        {
            Player.DepthPlayer().InWaterBody = false;
            _topY = -1;
            _visited = null;
        }
        else
        {
            _surfaceY = newSurfaceY;
            _topY = newSurfaceY;
        }
    }
    private void ValidateSurface()
    {
        if (!Player.DepthPlayer().InWaterBody || _topY < 0 || _visited == null)
            return;

        List<int> removeMe = null;

        foreach (int i in _visited)
        {
            int x = i >> 16;
            int y = i & 0xFFFF;

            if (y != _topY)
                continue;

            if (!IsWater(x, y))
            {
                removeMe ??= new List<int>();
                removeMe.Add(i);
            }
        }

        if (removeMe == null)
            return;

        foreach (int i in removeMe)
            _visited.Remove(i);

        RescanSurface();
    }

    public override void PostUpdate()
    {
        LuneWoL_AdvClientConfig.ClientDepthPressurePage Acfg = LuneWoL.AdvClientConfig.ClientDepthPressure;

        if (!Player.Submerged())
        {
            Player.DepthPlayer().InWaterBody = false;
            _topY = -1;
            _visited = null;
            _running = false;
            _timer = 0;
            return;
        }

        if (_running)
        {
            _topY = _scannedY;
            Player.DepthPlayer().InWaterBody = _scannedY != int.MaxValue;
        }

        if (++_timer < Acfg.UpdateIntervalTicks)
            return;

        _timer = 0;

        if (_running)
        {
            bool done = Step(Acfg.TileScanLimit);
            if (done)
            {
                if (_surfaceY == int.MaxValue)
                {
                    Player.DepthPlayer().InWaterBody = false;
                    _topY = -1;
                }
            }
            return;
        }

        ValidateSurface();

        if (_visited != null && Player.DepthPlayer().InWaterBody)
        {
            RescanSurface();
            return;
        }

        Point? start = FindStartingWaterTile();
        if (!start.HasValue)
        {
            Player.DepthPlayer().InWaterBody = false;
            _topY = -1;
            _visited = null;
            return;
        }

        Start(start.Value.X, start.Value.Y);
    }

    private bool NearVisited(int cx, int cy)
    {
        if (_visited.Contains((cx << 16) | (cy & 0xFFFF)))
            return true;

        for (int dx = -1; dx <= 1; dx++)
            for (int dy = -1; dy <= 1; dy++)
                if (_visited.Contains(((cx + dx) << 16) | ((cy + dy) & 0xFFFF)))
                    return true;
        return false;
    }

    public int GetDepth()
    {
        if (!Player.DepthPlayer().InWaterBody)
            return -1;

        int playerTileY = (int)(Player.Center.Y / 16f);
        int depth = playerTileY - _topY;
        return depth < 0 ? 0 : depth;
    }
}

#endregion

#region Debug

public class LWoL_DepthDebug_old : ModSystem
{
    private Texture2D _pixel;

    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
        LuneWoL_AdvClientConfig.ClientDepthPressurePage Acfg = AdvClientConfig.ClientDepthPressure;
        if (!Acfg.ShowDebugMarker && !Acfg.ShowScannedDebug)
            return;

        int i = layers.FindIndex(l => l.Name == "Vanilla: Mouse Text");
        if (i != -1)
            layers.Insert(i, new LegacyGameInterfaceLayer("SurfaceOverlay", DrawSurfaceOverlay, InterfaceScaleType.Game));
    }

    private void EnsurePixel()
    {
        if (_pixel == null)
        {
            _pixel = new Texture2D(Main.graphics.GraphicsDevice, 1, 1);
            _pixel.SetData([Color.White]);
        }
    }

    private bool DrawSurfaceOverlay()
    {
        LuneWoL_AdvClientConfig.ClientDepthPressurePage Acfg = AdvClientConfig.ClientDepthPressure;
        EnsurePixel();
        SpriteBatch sb = Main.spriteBatch;

        sb.End();
        sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.GameViewMatrix.ZoomMatrix);

        Player player = Main.LocalPlayer;
        Depth_II modPlayer = player.GetModPlayer<Depth_II>();
        Vector2 screenPos = Main.screenPosition;

        if (Acfg.ShowScannedDebug)
            DrawScannedTiles(sb, modPlayer, screenPos);

        if (Acfg.ShowDebugMarker)
            DrawSurfaceMarker(sb, player, modPlayer, screenPos);

        sb.End();
        sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);

        return true;
    }

    private void DrawScannedTiles(SpriteBatch sb, Depth_II modPlayer, Vector2 screenPos)
    {
        if (modPlayer._visited == null)
            return;

        LuneWoL_AdvClientConfig.ClientDepthPressurePage.ShowScannedDebugColourPage cfg = AdvClientConfig.ClientDepthPressure.ShowScannedDebugColour;

        int screenTileX1 = (int)(screenPos.X / 16f) - 1;
        int screenTileY1 = (int)(screenPos.Y / 16f) - 1;
        int screenTileX2 = screenTileX1 + (Main.screenWidth / 16) + 2;
        int screenTileY2 = screenTileY1 + (Main.screenHeight / 16) + 2;

        Color scanColor = new(cfg.ScanR, cfg.ScanG, cfg.ScanB, cfg.ScanA);
        foreach (int i in modPlayer._visited)
        {
            int x = i >> 16;
            int y = i & 0xFFFF;
            if (x < screenTileX1 || x > screenTileX2 || y < screenTileY1 || y > screenTileY2)
                continue;

            int drawX = (x * 16) - (int)screenPos.X;
            int drawY = (y * 16) - (int)screenPos.Y;
            sb.Draw(_pixel, new Rectangle(drawX, drawY, 16, 16), scanColor);
        }
    }

    private void DrawSurfaceMarker(SpriteBatch sb, Player player, Depth_II modPlayer, Vector2 screenPos)
    {
        if (!modPlayer.Player.DepthPlayer().InWaterBody)
            return;

        LuneWoL_AdvClientConfig.ClientDepthPressurePage.ShowDebugMarkerColourPage cfg = AdvClientConfig.ClientDepthPressure.ShowDebugMarkerColour;

        int drawY = (modPlayer._topY * 16) - (int)screenPos.Y;
        Color debugColor = new(cfg.MarkerR, cfg.MarkerG, cfg.MarkerB, cfg.MarkerA);
        sb.Draw(_pixel, new Rectangle(0, drawY, Main.screenWidth, 2), debugColor);
    }

    public override void Unload()
    {
        if (_pixel != null)
        {
            Texture2D tex = _pixel;
            Main.QueueMainThreadAction(tex.Dispose);
            _pixel = null;
        }
    }
}

#endregion
