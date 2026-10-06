namespace LuneWoL;

internal static class LuneWoL_Utilities
{
    public static LuneWoL_Plr WoLPlayer(this Player player) => player.GetModPlayer<LuneWoL_Plr>();
}
