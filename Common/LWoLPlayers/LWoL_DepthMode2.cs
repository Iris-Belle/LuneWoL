
namespace LuneWoL.Common.LWoLPlayers;

public class SurfacePressurePlayer : ModPlayer
{

    private int _updateTimer;
    private Texture2D _pixel;

    public bool IsInWaterPool { get; private set; }
    public int CachedTopY { get; private set; } = -1;

    private static int WorldWidth => Main.maxTilesX;

    private static int WorldHeight => Main.maxTilesY;

    public override void Initialize()
    {
        if (NotEnabled)
            return;

        _updateTimer = 0;
        IsInWaterPool = false;
        CachedTopY = -1;
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

    private bool IsWater(int x, int y)
    {
        if (x < 0 || x >= WorldWidth || y < 0 || y >= WorldHeight)
            return false;

        Tile t = Main.tile[x, y];
        return t.LiquidType == LiquidID.Water && t.LiquidAmount > 0;
    }

    private Point? FindStartingWaterTile()
    {
        int px = (int)(Player.Center.X / 16f);
        int py = (int)(Player.Center.Y / 16f);

        if (IsWater(px, py))
            return new Point(px, py);

        for (int dx = -1; dx <= 1; dx++)
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0)
                    continue;
                int nx = px + dx;
                int ny = py + dy;
                if (IsWater(nx, ny))
                    return new Point(nx, ny);
            }

        return null;
    }

    private int FindConnectedWaterSurface(int startX, int startY)
    {
        if (!IsWater(startX, startY))
            return -1;

        HashSet<int> visited = new(capacity: 4096);
        Queue<int> queue = new();

        int packedStart = (startX << 16) | (startY & 0xFFFF);
        _ = visited.Add(packedStart);
        queue.Enqueue(packedStart);

        int minSurfaceY = int.MaxValue;

        while (queue.Count > 0)
        {
            int packed = queue.Dequeue();
            int x = packed >> 16;
            int y = packed & 0xFFFF;

            Tile current = Main.tile[x, y];
            if (current == null || current.LiquidType != LiquidID.Water || current.LiquidAmount == 0)
                continue;

            if (y > 0)
            {
                Tile above = Main.tile[x, y - 1];
                if (above == null || above.LiquidAmount == 0)
                    if (y < minSurfaceY)
                        minSurfaceY = y;
            }
            else if (0 < minSurfaceY)
                minSurfaceY = 0;
            void EnqueueIfWater(int nx, int ny)
            {
                if (IsWater(nx, ny))
                {
                    int packed = (nx << 16) | ny;
                    if (!visited.Contains(packed))
                    {
                        _ = visited.Add(packed);
                        queue.Enqueue(packed);
                    }
                }
            }

            if (x > 0)
                EnqueueIfWater(x - 1, y);
            if (x + 1 < WorldWidth)
                EnqueueIfWater(x + 1, y);
            if (y > 0)
                EnqueueIfWater(x, y - 1);
            if (y + 1 < WorldHeight)
                EnqueueIfWater(x, y + 1);
        }

        return minSurfaceY == int.MaxValue ? -1 : minSurfaceY;
    }

    public override void PostUpdate()
    {
        if (NotEnabled)
            return;

        LWoLAdvancedClientSettings.ClientDepthPressureDented Acfg = LuneWoL.LWoLAdvancedClientSettings.ClientDepthPressure;

        if (++_updateTimer < Acfg.UpdateIntervalTicks)
            return;

        _updateTimer = 0;

        Point? start = FindStartingWaterTile();

        if (!start.HasValue)
        {
            IsInWaterPool = false;
            CachedTopY = -1;
            return;
        }

        int surfaceY = FindConnectedWaterSurface(start.Value.X, start.Value.Y);
        if (surfaceY < 0)
        {
            IsInWaterPool = false;
            CachedTopY = -1;
        }
        else
        {
            IsInWaterPool = true;
            CachedTopY = surfaceY;
        }

        PrintCurrentPlayerWaterDepth();
    }

    public int GetDepth()
    {
        if (!IsInWaterPool)
            return -1;

        int playerTileY = (int)(Player.Center.Y / 16f);
        int depth = playerTileY - CachedTopY;
        return depth < 0 ? 0 : depth;
    }

    public void PrintCurrentPlayerWaterDepth()
    {
        LWoLAdvancedClientSettings.ClientDepthPressureDented Acfg = LuneWoL.LWoLAdvancedClientSettings.ClientDepthPressure;
        if (Player.whoAmI != Main.myPlayer && Acfg.ShowSurfaceDebug)
            return;
        int playerY = (int)(Player.Center.Y / 16f);
        int depth = playerY - CachedTopY;
        if (depth < 0)
            _ = 0;
    }

    public class SurfacePressureSystemClient : ModSystem
    {
        private Texture2D _pixel;

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            if (NotEnabled)
                return;

            LWoLAdvancedClientSettings.ClientDepthPressureDented Acfg = LuneWoL.LWoLAdvancedClientSettings.ClientDepthPressure;
            if (!Acfg.ShowSurfaceDebug)
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
            _ = LuneWoL.LWoLAdvancedClientSettings.ClientDepthPressure;
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
            SurfacePressurePlayer modPlayer = player.GetModPlayer<SurfacePressurePlayer>();

            if (modPlayer.IsInWaterPool)
            {
                int px = (int)(player.Center.X / 16f);
                Vector2 screenPos = Main.screenPosition;
                int drawX = (px * 16) - (int)screenPos.X;
                int drawY = (modPlayer.CachedTopY * 16) - (int)screenPos.Y;
                Color debugColor = new(0, 200, 255, 200);
                sb.Draw(_pixel, new Rectangle(drawX, drawY, 16, 2), debugColor);
            }

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
}