namespace LuneWoL.Common.LWoLPlayers;
public class LWoL_DepthDebug : ModSystem
{
    private Texture2D _pixel;

    public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
    {
        LWoLAdvancedClientSettings.ClientDepthPressureDented Acfg = LuneWoL.LWoLAdvancedClientSettings.ClientDepthPressure;
        if (!Acfg.ShowSurfaceDebug && !Acfg.DrawScannedTiles)
            return;

        int idx = layers.FindIndex(l => l.Name == "Vanilla: Mouse Text");
        if (idx != -1)
            layers.Insert(idx, new LegacyGameInterfaceLayer("SurfaceOverlay", DrawSurfaceOverlay, InterfaceScaleType.Game));
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
        LWoLAdvancedClientSettings.ClientDepthPressureDented Acfg = LuneWoL.LWoLAdvancedClientSettings.ClientDepthPressure;
        EnsurePixel();
        SpriteBatch sb = Main.spriteBatch;

        sb.End();
        sb.Begin(
            SpriteSortMode.Deferred,
            BlendState.AlphaBlend,
            SamplerState.PointClamp,
            DepthStencilState.None,
            RasterizerState.CullCounterClockwise,
            null,
            Main.GameViewMatrix.ZoomMatrix
        );

        Player player = Main.LocalPlayer;
        DepthModeTwo modPlayer = player.GetModPlayer<DepthModeTwo>();
        Vector2 screenPos = Main.screenPosition;

        if (Acfg.DrawScannedTiles)
            DrawScannedTiles(sb, modPlayer, screenPos);

        if (Acfg.ShowSurfaceDebug)
            DrawSurfaceMarker(sb, player, modPlayer, screenPos);

        sb.End();
        sb.Begin(
            SpriteSortMode.Deferred,
            BlendState.AlphaBlend,
            SamplerState.PointClamp,
            DepthStencilState.None,
            RasterizerState.CullCounterClockwise,
            null,
            Main.UIScaleMatrix
        );

        return true;
    }

    private void DrawScannedTiles(SpriteBatch sb, DepthModeTwo modPlayer, Vector2 screenPos)
    {
        if (modPlayer._bfsVisited == null)
            return;

        int screenTileX1 = (int)(screenPos.X / 16f) - 1;
        int screenTileY1 = (int)(screenPos.Y / 16f) - 1;
        int screenTileX2 = screenTileX1 + (Main.screenWidth / 16) + 2;
        int screenTileY2 = screenTileY1 + (Main.screenHeight / 16) + 2;

        Color scanColor = new(255, 100, 0, 60);
        foreach (int packed in modPlayer._bfsVisited)
        {
            int tx = packed >> 16;
            int ty = packed & 0xFFFF;
            if (tx < screenTileX1 || tx > screenTileX2 || ty < screenTileY1 || ty > screenTileY2)
                continue;

            int drawX = (tx * 16) - (int)screenPos.X;
            int drawY = (ty * 16) - (int)screenPos.Y;
            sb.Draw(_pixel, new Rectangle(drawX, drawY, 16, 16), scanColor);
        }
    }

    private void DrawSurfaceMarker(SpriteBatch sb, Player player, DepthModeTwo modPlayer, Vector2 screenPos)
    {
        if (!modPlayer.IsInWaterPool)
            return;

        int px = (int)(player.Center.X / 16f);
        int drawX = (px * 16) - (int)screenPos.X;
        int drawY = (modPlayer.CachedTopY * 16) - (int)screenPos.Y;
        Color debugColor = new(0, 200, 255, 200);
        sb.Draw(_pixel, new Rectangle(drawX, drawY, 16, 2), debugColor);
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