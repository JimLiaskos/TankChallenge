
using System;
using System.Collections.Generic;

public static class CompassMapping
{
    public static Dictionary<Direction, int> GetCompassMap(Compass cardinalDirection)
    {
        switch (cardinalDirection) {
            case Compass.North:
                return GetNorthMap();

            case Compass.South:
                return GetSouthMap();

            case Compass.East:
                return GetEastMap();

            case Compass.West:
                return GetWestMap();

            default:
                throw new ApplicationException("Unknown cardinal direction: " + (int)cardinalDirection);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private static Dictionary<Direction, int> GetNorthMap()
    {
        return new Dictionary<Direction, int>() {
            {Direction.Front, 1},
            {Direction.Back, -1},
            {Direction.Left, -1},
            {Direction.Right, 1}
        };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private static Dictionary<Direction, int> GetSouthMap()
    {
        return new Dictionary<Direction, int>() {
           {Direction.Front, -1},
           {Direction.Back, +1},
           {Direction.Left, +1},
           {Direction.Right, -1}
        };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private static Dictionary<Direction, int> GetEastMap()
    {
        return new Dictionary<Direction, int>() {
           {Direction.Front, +1},
           {Direction.Back, -1},
           {Direction.Left, +1},
           {Direction.Right, -1}
        };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private static Dictionary<Direction, int> GetWestMap()
    {
        return new Dictionary<Direction, int>() {
           {Direction.Front, -1},
           {Direction.Back, +1},
           {Direction.Left, -1},
           {Direction.Right, +1}
        };
    }
}
