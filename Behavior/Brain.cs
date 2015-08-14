
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Probes the environment and the movement history to take a decision...like a brain!
/// </summary>
public class Brain
{
    private readonly List<TankState> _stateMemory;
    private readonly List<TankAction> _actionMemory;

    private Grid _gameGrid;
    private ITankBehavior _currentBehavior;
    private ITankBehavior _defaultBehavior;

    public Brain(Grid gameGrid)
    {

        _stateMemory = new List<TankState>();
        _actionMemory = new List<TankAction>();
        _currentBehavior = null;
        _defaultBehavior = new TakeAStroll();

        _gameGrid = gameGrid;
    }

    public TankAction TakeDecision(TankState state)
    {
        var closeIn = default(Direction?);
        var nextAction = TankAction.Unknown;
        var previousState = _stateMemory.LastOrDefault();
        var previousAction = _actionMemory.Select(x => (TankAction?)x).LastOrDefault();

        if (previousState != null &&
            previousAction.HasValue) {

            closeIn = DetectCloseIn(previousAction.Value, previousState, state);
        }

        if (_currentBehavior == null) {
            if (closeIn.HasValue) {
                _currentBehavior = new TurnShootTurn(closeIn.Value);
            }
        }

        if (_currentBehavior == null) {
            nextAction = GetDefaultBehavior().GetNextAction(previousAction);
        } else {
            nextAction = _currentBehavior.GetNextAction(previousAction);

            if (nextAction == TankAction.Unknown) { // the previous behaviour ended
                _currentBehavior = null;

                nextAction = GetDefaultBehavior().GetNextAction(previousAction);
            }
        }

        if (nextAction == TankAction.Unknown) {
            throw new ApplicationException("Tank is at a loss! It doesn't know what to do next!!");
        }

        _stateMemory.Add(state);
        _actionMemory.Add(nextAction);
        return nextAction;
    }

    private Direction? DetectCloseIn(TankAction previousAction, TankState previousState, TankState currentState)
    {
        if (previousAction == TankAction.TurnLeft ||
            previousAction == TankAction.TurnRight) {

            return null;
        }

        var closeIn = default(Direction?);

        var right_closeIn = false;
        var left_closeIn = false;

        if (currentState[Direction.Left] < previousState[Direction.Left])
            left_closeIn = true;

        if (currentState[Direction.Right] < previousState[Direction.Right])
            right_closeIn = true;

        if (right_closeIn && left_closeIn) {

            if (currentState[Direction.Left] - previousState[Direction.Left] <=
                currentState[Direction.Right] - previousState[Direction.Right]) {

                closeIn = Direction.Left;
            } else {

                closeIn = Direction.Right;
            }

        } else if (left_closeIn) {
            closeIn = Direction.Left;
        } else if (right_closeIn) {
            closeIn = Direction.Right;
        }

        if (closeIn.HasValue && currentState[closeIn.Value] == 1)
            closeIn = null;

        return closeIn;
    }

    private ITankBehavior GetDefaultBehavior()
    {
        return _defaultBehavior;
    }

}

