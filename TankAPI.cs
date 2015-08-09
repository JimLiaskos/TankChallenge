
using System;

public static class TankAPI
{
    private static bool _isTurnCompleted;
    private static TankState _currentState;

    /// <summary>
    /// 
    /// </summary>
    static TankAPI()
    {
        _isTurnCompleted = false;
        _currentState = TankState.Current;
    }

    public static bool IsTurnCompleted
    { get { return _isTurnCompleted; } }

    public static TankState CurrentState
    { get { return _currentState; } }

    public static int CompleteTurn()
    {
        if (!_isTurnCompleted) {
            throw new ApplicationException("Turn is NOT completed, error in tank logic!");
        }

        _isTurnCompleted = false;
        var newstate = TankState.Current;
        var fuelUsed = CurrentState.Fuel - newstate.Fuel;

        // set state for the begining of the next turn
        _currentState = newstate;

        return fuelUsed;
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
                break;

            case TankAction.MoveBackward:
                apiAction = API.MoveBackward;
                break;

            case TankAction.TurnLeft:
                apiAction = API.TurnLeft;
                break;

            case TankAction.TurnRight:
                apiAction = API.TurnRight;
                break;

            default:
                apiAction = null;
                break;
        }

        if (apiAction == null) {
            throw new ApplicationException(string.Format("Unknown TankAction: {0}", (int)action));
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