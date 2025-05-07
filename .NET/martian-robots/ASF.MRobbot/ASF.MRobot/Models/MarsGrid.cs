namespace ASF.MRobot.Models
{
    public class MarsGrid
    {
        public int MaxX { get; }
        public int MaxY { get; }

        // Scent set: stores lost positions with orientation (x, y, direction)
        private readonly HashSet<string> _scentMarkers = new();

        public MarsGrid(int maxX, int maxY)
        {
            if (maxX > 50 || maxY > 50 || maxX < 0 || maxY < 0)
                throw new ArgumentOutOfRangeException("Grid size must be within 0 to 50.");

            MaxX = maxX;
            MaxY = maxY;
        }

        public bool IsInBounds(int x, int y)
        {
            return x >= 0 && y >= 0 && x <= MaxX && y <= MaxY;
        }

        public void LeaveScent(int x, int y, char orientation)
        {
            _scentMarkers.Add(GetScentKey(x, y, orientation));
        }

        public bool HasScent(int x, int y, char orientation)
        {
            return _scentMarkers.Contains(GetScentKey(x, y, orientation));
        }

        private static string GetScentKey(int x, int y, char orientation)
        {
            return $"{x}:{y}:{orientation}";
        }
    }

}