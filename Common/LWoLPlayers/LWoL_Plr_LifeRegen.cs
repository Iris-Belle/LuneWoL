namespace LuneWoL.Common.WoL_Plrs;

public partial class LWoL_Plr : ModPlayer
{
    public static int spacedout = 50;
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
        LuneLib.Common.Players.LuneLibPlayer.LibPlayer pLib = Player.LibPlayer();

        spacedout = 50 -
            (pLib.WearingAstroHelm ? 10 : 0) -
            (pLib.WearingAstraliteVisor ? 15 : 0) -
            (pLib.IsWearingFishBowl ? 10 : 0);

        ApplyDoTDebuff(Player.LibPlayer().SpaceVacuum, spacedout, pLib.WearingFullAstralite || pLib.WearingFullAstro);

        ApplyDoTDebuff(Player.LibPlayer().depthwaterPressure, Player.LibPlayer().currentDepthPressure, IrisPlayer);

        ApplyDoTDebuff(Player.LibPlayer().BlizzardFrozen, 50, Player.buffImmune[BuffID.Frozen]);

        ApplyDoTDebuff(Player.LibPlayer().Chilly, 2, Player.buffImmune[BuffID.Chilled]);

        ApplyDoTDebuff(Player.LibPlayer().CrimtuptionzoneNight, 100, false);

        Player.lifeRegen -= (int)totalNegativeLifeRegen;
    }
}
