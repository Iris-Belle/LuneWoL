namespace LuneWoL.IL;

internal class LuneWoL_InvasionIL : ILoadable
{
    public bool IsLoadingEnabled(Mod mod) => !ServerConfig.Npc.InvasionSizePatchToggle;
    public void Unload() { }

    public void Load(Mod mod)
    {
        var method0 = typeof(Main).GetMethod("StartInvasion", BindingFlags.Public | BindingFlags.Static, null, [typeof(int)], null)
        ?? throw new Exception("smelly hooking on StartInvasion. if you are not me, dm me on discord at ´iris_lune' or join my server and ping me. you could also leave a comment on the mod but that might take longer to see and patch");
        
        MonoModHooks.Modify(method0, InvasionIL);
    }

    private static void InvasionIL(ILContext il)
    {
        ILCursor c = new(il);

        c.GotoNext(MoveType.After, i => i.MatchLdarg0(), i => i.MatchLdcI4(4), i => i.MatchBneUn(out _));
        c.Index--;
        Instruction instruction = c.Next;

        c.GotoNext(MoveType.Before, i => i.MatchLdsfld("Terraria.Main", "invasionSize"), i => i.MatchStsfld("Terraria.Main", "invasionSizeStart"));

        int index = c.Index;

        c.EmitDelegate<Func<int>>(() => AdvServerConfig.Adv_Npc.InvasionConfig.InvasionSize);
        c.Emit(OpCodes.Stsfld, typeof(Main).GetField("invasionSize", BindingFlags.Public | BindingFlags.Static));

        ILLabel label = il.DefineLabel(c.Instrs[index]);
        instruction.Operand = label;
    }
}

/*

	//         if (type == 4)

	IL_007E: ldarg.0
	IL_007F: ldc.i4.4
	IL_0080: bne.un.s  IL_0091


	//             Main.invasionSize = 160 + 40 * num;

	IL_0082: ldc.i4    160
	IL_0087: ldc.i4.s  40
	IL_0089: ldloc.0
	IL_008A: mul
	IL_008B: add
	IL_008C: stsfld    int32 Terraria.Main::invasionSize


	//         Main.invasionSizeStart = Main.invasionSize;

	IL_0091: ldsfld    int32 Terraria.Main::invasionSize
	IL_0096: stsfld    int32 Terraria.Main::invasionSizeStart

	//         Main.invasionProgress = 0;

	IL_009B: ldc.i4.0
	IL_009C: stsfld    int32 Terraria.Main::invasionProgress

	//         Main.invasionProgressIcon = type + 3;


	public static void StartInvasion(int type = 1)
	{
		if (invasionType != 0 && invasionSize == 0)
			invasionType = 0;

		if (invasionType != 0)
			return;

		int num = 0;
		for (int i = 0; i < 255; i++) {
			if (player[i].active && player[i].statLifeMax >= 200)
    if (player[i].active && player[i].ConsumedLifeCrystals >= 5)
        num++;
		        }

		        if (num > 0)
    {
        invasionType = type;
        invasionSize = 80 + 40 * num;
        if (type == 3)v
            invasionSize += 40 + 20 * num;

        if (type == 4)
            invasionSize = 160 + 40 * num;

        invasionSize = cfg.value; add this pls me

        invasionSizeStart = invasionSize;
        invasionProgress = 0;
        invasionProgressIcon = type + 3;
        invasionProgressWave = 0;
        invasionProgressMax = invasionSizeStart;
        invasionWarn = 0;
        if (type == 4)
        {
            invasionX = spawnTileX - 1;
            invasionWarn = 2;
        }
        else if (rand.Next(2) == 0)
        {
            invasionX = 0.0;
        }
        else
        {
            invasionX = maxTilesX;
        }
    }
	}

*/