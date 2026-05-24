namespace LuneWoL.Common.LWoLPlayers;

internal class LWoL_DepthDamage : ModPlayer
{
    public static bool NotEnabled => LuneWoL.LWoLServerConfig.Water.DepthPressureMode == 0;

    public static bool UsingModeOne => LuneWoL.LWoLServerConfig.Water.DepthPressureMode == 1;

    public static bool UsingModeTwo => LuneWoL.LWoLServerConfig.Water.DepthPressureMode == 2;

    public int breathCooldown, maxDepth, pressureDamageToApply, reducedDepthDiff;
    public float reducedDepth, lightDepthDiff, tileDiffCalced, tileDiff, entryY;

    public DepthModeOne ModeOnePlayer => Player.GetModPlayer<DepthModeOne>();

    public DepthModeTwo ModeTwoPlayer => Player.GetModPlayer<DepthModeTwo>();

    [JITWhenModsEnabled("LuneLibAssets")] //private mod with copyrighted content. you cant has this, sorry :c
    public void CopyrightSound() => SoundEngine.PlaySound(DrownSound, Player.Center);

    public void Sound() => SoundEngine.PlaySound(SoundID.Drown, Player.Center);

    public void DamageChecker()
    {
        if (NotEnabled)
            return;
        LuneLib.Common.Players.LuneLibPlayer.LibPlayer pLib = Player.LibPlayer();

        LWoLAdvancedServerSettings.ServerDepthPressureDented cfg = LuneWoL.LWoLAdvancedServerSettings.ServerDepthPressure;
        LWoLAdvancedServerSettings.ServerDepthPressureDented.LifeResistDented Lcfg = LuneWoL.LWoLAdvancedServerSettings.ServerDepthPressure.LifeResistValues;

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

            if (LuneWoL.LWoLAdvancedClientSettings.ClientDepthPressure.DebugText)
                Main.NewText($"baseLifeLoss={baseLifeLoss:F2}, lifeLossResist={lifeLossResist}, depthPastMax={depthPastMax:F2}, lifeTier={lifeTier:F2}");

            pressureDamageToApply = Math.Max((int)(baseLifeLoss - lifeLossResist), 0);

            Player.LibPlayer().depthwaterPressure = true;
            Player.LibPlayer().currentDepthPressure = pressureDamageToApply;
        }
        else
        {
            Player.LibPlayer().depthwaterPressure = false;
            Player.LibPlayer().currentDepthPressure = 0;
        }
    }

    private void BreathChecker()
    {
        LuneLib.Common.Players.LuneLibPlayer.LibPlayer pLib = Player.LibPlayer();

        LWoLAdvancedServerSettings.ServerDepthPressureDented cfg = LuneWoL.LWoLAdvancedServerSettings.ServerDepthPressure;
        LWoLAdvancedServerSettings.ServerDepthPressureDented.StairValuesDented Scfg = LuneWoL.LWoLAdvancedServerSettings.ServerDepthPressure.StairValues;
        LWoLAdvancedServerSettings.ServerDepthPressureDented.BreathValuesDented Bcfg = LuneWoL.LWoLAdvancedServerSettings.ServerDepthPressure.BreathValues;
        LWoLAdvancedServerSettings.ServerDepthPressureDented.TickValuesDented Tcfg = LuneWoL.LWoLAdvancedServerSettings.ServerDepthPressure.TickValues;

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

        if (LuneWoL.LWoLAdvancedClientSettings.ClientDepthPressure.DebugText)
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

        if (reducedDepthDiff > maxDepth + 50 && Player.LibPlayer().depthwaterPressure && Player.Submerged() && Player.whoAmI == Main.myPlayer)
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
        LWoLAdvancedServerSettings.ServerDepthPressureDented cfg = LuneWoL.LWoLAdvancedServerSettings.ServerDepthPressure;
        LWoLAdvancedServerSettings.ServerDepthPressureDented.MaxDepthDented Dcfg = LuneWoL.LWoLAdvancedServerSettings.ServerDepthPressure.MaxDepthValues;
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
            else if(Player.accMerman) maxDepth = (int)(cfg.BaseMaxDepth * (1f + Dcfg.MaxDepthMerman));
            else if(Player.gills) maxDepth = (int)(cfg.BaseMaxDepth * (1f + Dcfg.MaxDepthGills));
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
        LWoLAdvancedServerSettings.ServerDepthPressureDented.MaxDepthDented cfg = LuneWoL.LWoLAdvancedServerSettings.ServerDepthPressure.MaxDepthValues;
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
            entryY = ModeTwoPlayer.CachedTopY * 16f;
    }

    public override void PostUpdateMiscEffects()
    {
        if (NotEnabled)
            return;

        if (Player.whoAmI != Main.myPlayer)
            return;

        UpdateWaterState();
    }

    public override void PostUpdateEquips()
    {
        if (NotEnabled)
            return;

        if (Player.whoAmI != Main.myPlayer)
            return;
        if (entryY < 0 && UsingModeTwo)
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

            LWoLAdvancedServerSettings.ServerDepthPressureDented cfg = LuneWoL.LWoLAdvancedServerSettings.ServerDepthPressure;

            bool hasWaterBreathing = Player.gills || Player.merman || Player.accMerman;

            if (hasWaterBreathing)
            {
                bool inAbyss = LuneLib.LuneLib.instance.CalamityModLoaded && Player.zoneAbyss();

                if (!inAbyss)
                {
                    if (Player.breath > 6)
                        Player.breath -= 3;
                }

                if (Player.breath > 6)
                    ResetROT(4, Player.whoAmI);
                else
                {
                    if (RunOneTime(4, Player.whoAmI))
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
        if (NotEnabled)
            return;

        if (Player.whoAmI != Main.myPlayer)
            return;
        if (!Player.LibPlayer().WaterEyes)
            return;
        if (entryY < 0 && UsingModeTwo)
            return;

        lightDepthDiff *= LuneWoL.LWoLAdvancedServerSettings.ServerDepthPressure.DepthDarknessIntensity;

        ScreenObstruction.screenObstruction = MathHelper.Lerp(ScreenObstruction.screenObstruction, 1f, lightDepthDiff);
        float reversed = 1f - lightDepthDiff;
        float clamped = MathHelper.Clamp(reversed, 0.5f, 1f);
        Lighting.GlobalBrightness *= clamped;
    }
}
