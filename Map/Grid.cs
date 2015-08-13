
using System.Collections.Generic;

public class Grid<T>
{
    private Dictionary<Point, T> _tiles;

    public Grid()
    {
        _tiles = new Dictionary<Point, T>();

        TankPosition = new Point(0, 0);
    }

    /// <summary>
    /// 
    /// </summary>
    public Point TankPosition { get; private set; }

    public void UpdateMap(TankState state)
    {

        

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public T this[Point p]
    {
        get { return _tiles[p]; }

        set { _tiles[p] = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="p"></param>
    public void SetTankPosition(Point p)
    {
        TankPosition = new Point(p.X, p.Y);
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
}
