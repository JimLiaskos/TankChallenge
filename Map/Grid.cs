
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

public class Grid
{
    private Dictionary<Compass, System.Action<Point, bool?, int>> _compassMap;

    private Dictionary<Point, Tile> _tiles;
    private Dictionary<Tile, string> _tileChars;

    public Grid()
    {
        _tiles = new Dictionary<Point, Tile>();

        _compassMap = new Dictionary<Compass, System.Action<Point, bool?, int>>() {
            {Compass.North, MapNorth},
            {Compass.South, MapSouth},
            {Compass.East, MapEast},
            {Compass.West, MapWest}
        };

        _tileChars = new Dictionary<Tile, string>() {
            {Tile.Unoccupied, "_"},
            {Tile.Ocuppied  , "X"},
            {Tile.Target    , "+"},
            {Tile.Obstacle  , "O"}
        };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public Tile this[Point p]
    {
        get { return _tiles[p]; }

        set { _tiles[p] = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public bool Contains(Point p)
    {
        return _tiles.ContainsKey(p);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="state"></param>
    public void UpdateMap(TankState state)
    {
        var cardinalMapping = CompassMapping.GetCompassMap(state.CardinalDirection);

        var directions = new[] {
                Direction.Front, Direction.Back, 
                Direction.Left, Direction.Right};

        foreach (var tankSide in directions) {
            var isTargetInSight =
                tankSide == Direction.Front ? (bool?) state.TargetInSight : null;
            var cardinalDirection = cardinalMapping[tankSide];

            _compassMap[cardinalDirection](state.CurrentPosition, isTargetInSight, state[tankSide]);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Print(TankState state)
    {
        var points = _tiles.Keys.ToList();

        if (!points.Any()) {
            return;
        }

        var minX = points.Min(point => point.X);
        var maxX = points.Max(point => point.X);

        var minY = points.Min(point => point.Y);
        var maxY = points.Max(point => point.Y);

        var width = Math.Abs(maxX - minX);
        var height = Math.Abs(maxY - minY);

        for (var y = maxY; y >= 0; y--) {
            var builder = new StringBuilder();

            for (var x = minX; x < width; x++) {

                var ch = " ";
                var point = new Point(x, y);

                if (point == state.CurrentPosition) {
                    ch = "T";
                } else if (_tiles.ContainsKey(point)) {
                    ch = _tileChars[_tiles[point]];
                }

                builder.Append(ch);
            }

            Console.WriteLine(builder.ToString());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="points"></param>
    /// <param name="targetInSight"></param>
    private void MapPoints(List<Point> points, bool? targetInSight)
    {
        if (points.Count == 0)
            throw new ApplicationException("No points to map!");

        var barrier = points.ElementAt(points.Count - 1);

        points.RemoveAt(points.Count - 1);

        foreach (var p in points) {

            if ((_tiles.ContainsKey(p) && (_tiles[p] == Tile.Target || _tiles[p] == Tile.Obstacle)) ||
                !_tiles.ContainsKey(p)) {

                _tiles[p] = Tile.Unoccupied;
            }
        }

        if (!_tiles.ContainsKey(barrier)) {

            this[barrier] =
                !targetInSight.HasValue ? Tile.Obstacle :
                    targetInSight.Value ? Tile.Target : Tile.Ocuppied;
        }
    }

    private List<Point> GetPoints(Compass direction, Point zeroLocation, int distance)
    {
        return Enumerable
            .Range(1, distance)
            .Select(offset => new {
                sign = CompassMapping.GetDirectionSign(direction),
                offset = CompassMapping.GetDirectionOffset(direction, (uint) offset)
            })
            .Select(p => zeroLocation + (p.offset * p.sign)).ToList();
    }

    private void MapNorth(Point zeroLocation, bool? targetInSight, int distance)
    {
        MapPoints(GetPoints(Compass.North, zeroLocation, distance), targetInSight);
    }

    private void MapSouth(Point zeroLocation, bool? targetInSight, int distance)
    {
        MapPoints(GetPoints(Compass.South, zeroLocation, distance), targetInSight);
    }

    private void MapEast(Point zeroLocation, bool? targetInSight, int distance)
    {
        MapPoints(GetPoints(Compass.East, zeroLocation, distance), targetInSight);
    }

    private void MapWest(Point zeroLocation, bool? targetInSight, int distance)
    {
        MapPoints(GetPoints(Compass.West, zeroLocation, distance), targetInSight);
    }
}
