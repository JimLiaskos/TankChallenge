
using System;
using System.Collections.Generic;

public static class CompassMapping
{

    public static Dictionary<Direction, Compass> GetCompassMap(Compass cardinalDirection)
    {
        var north = new Dictionary<Direction, Compass>() {
            {Direction.Front, Compass.North}, {Direction.Back, Compass.South},
            {Direction.Left, Compass.West}, {Direction.Right, Compass.East}
        };

        var south = new Dictionary<Direction, Compass>() {
            {Direction.Front, Compass.South}, {Direction.Back, Compass.South},
            {Direction.Left, Compass.East}, {Direction.Right, Compass.West}
        };

        var east = new Dictionary<Direction, Compass>() {
            {Direction.Front, Compass.East}, {Direction.Back, Compass.West},
            {Direction.Left, Compass.North}, {Direction.Right, Compass.South}
        };

        var west = new Dictionary<Direction, Compass>() {
            {Direction.Front, Compass.West}, {Direction.Back, Compass.East},
            {Direction.Left, Compass.South}, {Direction.Right, Compass.North}
        };


        switch (cardinalDirection) {
            case Compass.North:
                return north;

            case Compass.South:
                return south;

            case Compass.East:
                return east;

            case Compass.West:
                return west;

            default:
                throw new ApplicationException(
                    "Unknown cardinal direction: " + (int) cardinalDirection);
        }
    }

    public static Point GetDirectionSign(Compass cardinalDirection)
    {
        switch (cardinalDirection) {
            case Compass.North:
            case Compass.East:
                return new Point(1, 1);

            case Compass.South:
                return new Point(1, -1);

            case Compass.West:
                return new Point(-1, 1);

            default:
                throw new ApplicationException(
                    "Unknown cardinal direction: " + (int) cardinalDirection);
        }
    }

    public static Point GetDirectionOffset(Compass cardinalDirection, uint offset)
    {
        switch (cardinalDirection) {
            case Compass.North:
                return new Point(0, (int) offset);

            case Compass.South:
                return new Point(0, -1 * (int) offset);

            case Compass.East:
                return new Point((int) offset, 0);

            case Compass.West:
                return new Point(-1 * (int) offset, 0);

            default:
                throw new ApplicationException(
                    "Unknown cardinal direction: " + (int) cardinalDirection);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cardinalDirection"></param>
    /// <returns></returns>
    public static Compass GetOppositeDirection(Compass cardinalDirection) {
        switch (cardinalDirection) {
            case Compass.North:
                return Compass.South;

            case Compass.South:
                return Compass.North;

            case Compass.East:
                return Compass.West;

            case Compass.West:
                return Compass.East;

            default:
                throw new ApplicationException(
                    "Unknown cardinal direction: " + (int) cardinalDirection);
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
                return newDirection == Compass.East ? Compass.North : Compass.South;

            default:
                throw new ApplicationException("New direction unknown");
        }

    }
}
