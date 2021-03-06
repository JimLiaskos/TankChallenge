﻿
using System.Text;
using System.Collections.Generic;

public class TankState
{
    private readonly int _fuel;
    private Point _currentPosition;
    private readonly bool _targetInSight;
    private readonly Compass _cardinalDirection;
    private readonly Dictionary<Direction, int> _distances;

    public static TankState Current
    {
        get { return new TankState(); }
    }

    private TankState()
    {
        _fuel = API.CurrentFuel();
        _targetInSight = API.IdentifyTarget();
        _currentPosition = TankAPI.CurrentPosition;
        _cardinalDirection = TankAPI.CardinalDirection;

        _distances = new Dictionary<Direction, int> {
                {Direction.Front, API.LidarFront()},
                {Direction.Back, API.LidarBack()},
                {Direction.Left, API.LidarLeft()},
                {Direction.Right, API.LidarRight()}
            };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    public int this[Direction dir]
    { get { return _distances[dir]; } }

    /// <summary>
    /// 
    /// </summary>
    public int Fuel
    { get { return _fuel; } }

    /// <summary>
    /// 
    /// </summary>
    public bool TargetInSight
    { get { return _targetInSight; } }

    /// <summary>
    /// 
    /// </summary>
    public Point CurrentPosition
    { get { return _currentPosition; } }

    /// <summary>
    /// 
    /// </summary>
    public Compass CardinalDirection
    { get { return _cardinalDirection; } }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        var builder = new StringBuilder();

        builder.AppendFormat("\t\tFR: {0}", this[Direction.Front].ToString("##"));
        builder.AppendLine();

        builder.AppendFormat("\tLE: {0}\t\tRI: {1}",
            this[Direction.Left].ToString("##"),
            this[Direction.Right].ToString("##"));
        builder.AppendLine();

        builder.AppendFormat("\t\tBA: {0}", this[Direction.Back].ToString("##"));
        builder.AppendLine();

        builder.AppendFormat("FUEL: {0}, TIS: {1}", Fuel.ToString("##"), TargetInSight);
        builder.AppendFormat("[X, Y]: {0} CMP: {1}", CurrentPosition.ToString(), CardinalDirection);
        return builder.ToString();
    }
}
