namespace LuneWoL.IL
{
    internal class LuneWoL_OreGenIL : ILoadable
    {
        public bool IsLoadingEnabled(Mod mod) => ServerConfig.Environment.OreScarcityPatchToggle;
        public void Unload() { }

        public void Load(Mod mod)
        {
            var method0 = typeof(WorldGen).GetMethod("SmashAltar", BindingFlags.Public | BindingFlags.Static, null, [typeof(int), typeof(int)], null)
            ?? throw new Exception("smelly hooking on SmashAltar. if you are not me, dm me on discord at ´iris_lune' or join my server and ping me. you could also leave a comment on the mod but that might take longer to see and patch");
            
            MonoModHooks.Modify(method0, OreGenIL);
        }

        private static void OreGenIL(ILContext il)
        {
            ILCursor c = new(il);

            double percent = (double)AdvServerConfig.Adv_Environment.OreScarcity.HardmodeOreScarcityPercent / 100;
            int min = Math.Max(1, 5 * AdvServerConfig.Adv_Environment.OreScarcity.HardmodeOreAmountPercent / 100);
            int annivmax = Math.Max(min + 1, 11 * AdvServerConfig.Adv_Environment.OreScarcity.HardmodeOreAmountPercent / 100);
            int max = Math.Max(min + 1, 9 * AdvServerConfig.Adv_Environment.OreScarcity.HardmodeOreAmountPercent / 100);

            if (c.TryGotoNext(MoveType.After, i => i.MatchConvR8(), i => i.MatchDiv(), i => i.MatchStloc2()))
            {
                c.Emit(OpCodes.Ldloc_2);
                c.EmitDelegate<Func<double>>(() => percent);
                c.Emit(OpCodes.Mul);
                c.Emit(OpCodes.Stloc_2);
            }
            if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcI4(5)))
            {
                c.RemoveRange(2);
                c.EmitDelegate<Func<int>>(() => min);
                c.EmitDelegate<Func<int>>(() => annivmax);
            }
            if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcI4(5)))
            {
                c.RemoveRange(2);
                c.EmitDelegate<Func<int>>(() => min);
                c.EmitDelegate<Func<int>>(() => annivmax);
            }
            if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcI4(5)))
            {
                c.RemoveRange(2);
                c.EmitDelegate<Func<int>>(() => min);
                c.EmitDelegate<Func<int>>(() => max);
            }
            if (c.TryGotoNext(MoveType.Before, i => i.MatchLdcI4(5)))
            {
                c.RemoveRange(2);
                c.EmitDelegate<Func<int>>(() => min);
                c.EmitDelegate<Func<int>>(() => max);
            }
        }
    }
}
