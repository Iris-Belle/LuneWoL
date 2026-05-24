namespace LuneWoL.Common.LWoLPlayers;

public partial class DepthModeOne : ModPlayer
{
    public bool WasDrowningLastFrame { get; set; }

    public Vector2 EntryPoint { get; set; }
    public Vector2 ExitPoint { get; set; }

    public bool InWaterBody { get; set; }
    public bool IsDrowning { get; set; }

    public void CheckWaterDepth()
    {
        bool currentlyDrowning = Collision.DrownCollision(Main.LocalPlayer.position, Main.LocalPlayer.width, Main.LocalPlayer.height, Main.LocalPlayer.gravDir);

        if (currentlyDrowning && !WasDrowningLastFrame && !InWaterBody)
        {
            InWaterBody = true;
            EntryPoint = Main.LocalPlayer.position;
        }
        else if (currentlyDrowning && InWaterBody && Main.LocalPlayer.position.Y < EntryPoint.Y)
        {
            EntryPoint = Main.LocalPlayer.position;
        }
        else if (!currentlyDrowning && WasDrowningLastFrame)
        {
            ExitPoint = Main.LocalPlayer.position;
            InWaterBody = true;
        }

        if (!currentlyDrowning && InWaterBody)
        {
            if (Vector2.Distance(Player.position, ExitPoint) >= 240f)
                InWaterBody = false;
        }

        if (!InWaterBody && Vector2.Distance(Main.LocalPlayer.position, ExitPoint) > 240f)
        {
            EntryPoint = Main.LocalPlayer.position;
            ExitPoint = Main.LocalPlayer.position;
        }

        WasDrowningLastFrame = currentlyDrowning;
    }
}
