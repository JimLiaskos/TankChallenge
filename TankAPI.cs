﻿
using System;

public static class TankAPI
{
    private static bool _isTurnCompleted;
    private static Point _currentPosition;
    private static TankState _currentState;
    private static Compass _currentDirection;


    /// <summary>
    /// 
    /// </summary>
    static TankAPI()
    {
        _currentPosition = new Point(0, 0);
        _currentState = TankState.Current;
        _currentDirection = Compass.North;
    }

    public static bool IsTurnCompleted
    { get { return _isTurnCompleted; } }

    public static Point CurrentPosition
    { get { return _currentPosition; } }

    public static Compass CardinalDirection
    { get { return _currentDirection; } }

    public static TankState CurrentState
    { get { return _currentState; } }

    public static void NewTurn()
    {
        _currentState = TankState.Current;
        _isTurnCompleted = false;
    }

    public static int CompleteTurn()
    {
        if (!_isTurnCompleted) {
            throw new ApplicationException("Turn is NOT completed, error in tank logic!");
        }

        return CurrentState.Fuel - TankState.Current.Fuel;
    }

    public static void PerformAction(TankAction action)
    {
        var apiAction = default(System.Action);

        switch (action) {
            case TankAction.FireCannon:
                apiAction = API.FireCannon;
                break;

            case TankAction.MoveForward:
                apiAction = API.MoveForward;

                _currentPosition = _currentPosition + 
                    CompassMapping.GetDirectionSign(_currentDirection) * 
                    CompassMapping.GetDirectionOffset(_currentDirection, (uint) 1);

                break;

            case TankAction.MoveBackward:
                apiAction = API.MoveBackward;

                var oppsiteDirection = CompassMapping.GetOppositeDirection(_currentDirection);

                _currentPosition = _currentPosition +
                    CompassMapping.GetDirectionSign(oppsiteDirection) *
                    CompassMapping.GetDirectionOffset(oppsiteDirection, (uint) 1);

                break;

            case TankAction.TurnLeft:
                apiAction = API.TurnLeft;

                _currentDirection = CompassMapping.GetNewDirection(
                    _currentDirection, Compass.West);

                break;

            case TankAction.TurnRight:
                apiAction = API.TurnRight;

                _currentDirection = CompassMapping.GetNewDirection(
                    _currentDirection, Compass.East);

                break;

            case TankAction.TryToShoot:
                apiAction = () => {
                    if (CurrentState.TargetInSight)
                        API.FireCannon();
                };
                break;

            default:
                apiAction = null;
                break;
        }

        if (apiAction == null) {
            throw new ApplicationException(string.Format("Unknown TankAction: {0}", (int) action));
        }

        TankAPI.ConsumeTurn(apiAction);
    }

    private static void ConsumeTurn(System.Action apiAction)
    {
        if (_isTurnCompleted)
            throw new ApplicationException("Turn is completed, error in tank logic!");

        apiAction();

        _isTurnCompleted = true;
    }
}