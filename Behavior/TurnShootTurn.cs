
using System;

using System.Collections.Generic;

public class TurnShootTurn : ITankBehavior
{
    private readonly Queue<TankAction> _queue;

    public TurnShootTurn(Direction direction)
    {
        switch (direction) {
            case Direction.Left:
                _queue = GetLeftComplexAction();
                break;

            case Direction.Right:
                _queue = GetRightComplexAction();
                break;

            default:
                throw new ApplicationException(
                    string.Format("Wrong TurnShootTurn behavior diretion: {0}", direction));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Queue<TankAction> GetLeftComplexAction()
    {
        var queue = new Queue<TankAction>();

        queue.Enqueue(TankAction.TurnLeft);
        queue.Enqueue(TankAction.TryToShoot);
        queue.Enqueue(TankAction.TurnRight);
        queue.Enqueue(TankAction.Unknown);

        return queue;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Queue<TankAction> GetRightComplexAction()
    {
        var queue = new Queue<TankAction>();

        queue.Enqueue(TankAction.TurnRight);
        queue.Enqueue(TankAction.TryToShoot);
        queue.Enqueue(TankAction.TurnLeft);
        queue.Enqueue(TankAction.Unknown);

        return queue;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public TankAction GetNextAction(TankAction? previousAction)
    {
        var nextAction = default(TankAction);

        if (previousAction.HasValue &&
            previousAction.Value == TankAction.TryToShoot &&
            TankAPI.CurrentState.TargetInSight) {

            nextAction = TankAction.TryToShoot;
            Console.WriteLine("--DEBUG: ShootTurnShoot TryShoot AGAIN!");

        } else {
            nextAction = _queue.Dequeue();
        }

        Console.WriteLine("--DEBUG: ShootTurnShoot Action: {0}", nextAction);

        return nextAction;
    }
}
