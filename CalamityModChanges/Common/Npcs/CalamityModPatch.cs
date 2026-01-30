namespace LuneWoL.CalamityModChanges.Common.Npcs;

public class CalamityModPatch : ILoadable
{
    private Mod CalamityMod => ModLoader.TryGetMod("CalamityMod", out Mod clam) ? clam : null;

    public bool IsLoadingEnabled(Mod mod) => CalamityMod != null && LuneWoL.LWoLServerConfig.CalamityMod.DifficultyRebuff;

    public void Load(Mod mod) => MonoModHooks.Modify(CalamityMod.Code.GetType("CalamityMod.NPCs.CalamityGlobalNPC").GetMethod("AdjustMasterModeStatScaling", BindingFlags.Public | BindingFlags.Static), Callback);

    public void Unload()
    {
    }

    private void Callback(ILContext IL)
    {
        ILCursor c = new(IL);
        _ = c.TryGotoNext(MoveType.Before, (i) => i.MatchLdcR8(0.75));
        _ = c.RemoveRange(1);
        _ = c.EmitLdcR4(1);
        _ = c.TryGotoNext(MoveType.Before, (i) => i.MatchLdcR8(0.9));
        _ = c.RemoveRange(1);
        _ = c.EmitLdcR4(1);
    }
}
