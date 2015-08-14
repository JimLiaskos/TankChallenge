
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
                throw new ApplicationException("Unknown cardinal direction: " + (int) cardinalDirection);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="currentDirection"></param>
    /// <param name="newDirection"></param>
    /// <returns></returns>
    public static Compass GetNewDirection(Compass currentDirection, Compass newDirection)
    {
        if (newDirection != Compass.West &&
            newDirection != Compass.East) {

            throw new ApplicationException("Cannot turn to a direction other than West or East.");
        }

        if (currentDirection == Compass.North) {
            return newDirection;
        }

        switch (currentDirection) {
            case Compass.South:
                return newDirection == Compass.East ? Compass.West : Compass.East;

            case Compass.East:
                return newDirection == Compass.East ? Compass.South : Compass.North;

            case Compass.West:
                return newDirection == Compass.East ? Compass.North: Compass.South;

            default:
                throw new ApplicationException("New direction unknown");
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
