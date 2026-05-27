namespace LuneWoL.Common.LWoLPlayers;

public class DepthModeTwo : ModPlayer
{

    private int _updateTimer;

    public bool IsInWaterPool { get; private set; }
    public int CachedTopY { get; private set; } = -1;

    private static int WorldWidth => Main.maxTilesX;

    private static int WorldHeight => Main.maxTilesY;

    internal HashSet<int> _bfsVisited;
    private Queue<int> _bfsQueue;
    private int _bfsMinSurfaceY;
    private bool _bfsRunning;
    private int _bfsTopScannedY;

    public override void Initialize()
    {
        _updateTimer = 0;
        IsInWaterPool = false;
        CachedTopY = -1;
        _bfsRunning = false;
        _bfsVisited = null;
        _bfsQueue = null;
    }

    private void StartBFS(int startX, int startY)
    {
        _bfsVisited = new HashSet<int>(capacity: 4096);
        _bfsQueue = new Queue<int>();
        _bfsMinSurfaceY = int.MaxValue;
        _bfsTopScannedY = startY;

        int packed = (startX << 16) | (startY & 0xFFFF);
        _bfsVisited.Add(packed);
        _bfsQueue.Enqueue(packed);
        _bfsRunning = true;
    }

    private bool StepBFS(int budget)
    {
        int tilesScanned = 0;

        while (_bfsQueue.Count > 0 && (budget == -1 || tilesScanned < budget))
        {
            tilesScanned++;
            int packed = _bfsQueue.Dequeue();
            int x = packed >> 16;
            int y = packed & 0xFFFF;

            Tile current = Main.tile[x, y];
            if (current == null || current.LiquidType != LiquidID.Water || current.LiquidAmount == 0)
                continue;

            if (y < _bfsTopScannedY)
                _bfsTopScannedY = y;

            if (y > 0)
            {
                Tile above = Main.tile[x, y - 1];
                if (above == null || above.LiquidAmount == 0)
                    if (y < _bfsMinSurfaceY)
                    {
                        _bfsMinSurfaceY = y;
                    }
            }
            else if (0 < _bfsMinSurfaceY)
            {
                _bfsMinSurfaceY = 0;
            }

            void EnqueueIfWater(int nx, int ny)
            {
                if (IsWater(nx, ny))
                {
                    int p = (nx << 16) | ny;
                    if (!_bfsVisited.Contains(p))
                    {
                        _bfsVisited.Add(p);
                        _bfsQueue.Enqueue(p);
                    }
                }
            }

            if (x > 0) EnqueueIfWater(x - 1, y);
            if (x + 1 < WorldWidth) EnqueueIfWater(x + 1, y);
            if (y > 0) EnqueueIfWater(x, y - 1);
            if (y + 1 < WorldHeight) EnqueueIfWater(x, y + 1);
        }

        if (_bfsQueue.Count == 0)
        {
            _bfsRunning = false;
            return true;
        }

        return false;
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

    private void RescanSurface()
    {
        if (_bfsVisited == null)
            return;

        int newMinSurfaceY = int.MaxValue;

        foreach (int packed in _bfsVisited)
        {
            int x = packed >> 16;
            int y = packed & 0xFFFF;

            if (y > _bfsMinSurfaceY + 1)
                continue;

            if (!IsWater(x, y))
                continue;

            if (y > 0)
            {
                Tile above = Main.tile[x, y - 1];
                if (above == null || above.LiquidAmount == 0)
                    if (y < newMinSurfaceY)
                        newMinSurfaceY = y;
            }
            else if (0 < newMinSurfaceY)
                newMinSurfaceY = 0;
        }

        if (newMinSurfaceY == int.MaxValue)
        {
            IsInWaterPool = false;
            CachedTopY = -1;
            _bfsVisited = null;
        }
        else
        {
            _bfsMinSurfaceY = newMinSurfaceY;
            CachedTopY = newMinSurfaceY;
        }
    }
    private void ValidateSurface()
    {
        if (!IsInWaterPool || CachedTopY < 0 || _bfsVisited == null)
            return;

        List<int> toRemove = null;

        foreach (int packed in _bfsVisited)
        {
            int x = packed >> 16;
            int y = packed & 0xFFFF;

            if (y != CachedTopY)
                continue;

            if (!IsWater(x, y))
            {
                toRemove ??= new List<int>();
                toRemove.Add(packed);
            }
        }

        if (toRemove == null)
            return;

        foreach (int packed in toRemove)
            _bfsVisited.Remove(packed);

        RescanSurface();
    }

    public override void PostUpdate()
    {
        LWoLAdvancedClientSettings.ClientDepthPressureDented Acfg = LuneWoL.LWoLAdvancedClientSettings.ClientDepthPressure;

        if (!Player.Submerged())
        {
            IsInWaterPool = false;
            CachedTopY = -1;
            _bfsVisited = null;
            _bfsRunning = false;
            _updateTimer = 0;
            return;
        }

        if (_bfsRunning)
        {
            CachedTopY = _bfsTopScannedY;
            IsInWaterPool = _bfsTopScannedY != int.MaxValue;
        }

        if (++_updateTimer < Acfg.UpdateIntervalTicks)
            return;

        _updateTimer = 0;

        if (_bfsRunning)
        {
            bool done = StepBFS(Acfg.TileScanLimit);
            if (done)
            {
                if (_bfsMinSurfaceY == int.MaxValue)
                {
                    IsInWaterPool = false;
                    CachedTopY = -1;
                }
            }
            return;
        }

        ValidateSurface();

        if (_bfsVisited != null && IsInWaterPool)
        {
            RescanSurface();
            return;
        }

        Point? start = FindStartingWaterTile();
        if (!start.HasValue)
        {
            IsInWaterPool = false;
            CachedTopY = -1;
            _bfsVisited = null;
            return;
        }

        StartBFS(start.Value.X, start.Value.Y);
    }

    public int GetDepth()
    {
        if (!IsInWaterPool)
            return -1;

        int playerTileY = (int)(Player.Center.Y / 16f);
        int depth = playerTileY - CachedTopY;
        return depth < 0 ? 0 : depth;
    }
}