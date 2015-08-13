
public class TakeAStroll : ITankBehavior
{
    public TankAction GetNextAction(TankAction? previousAction)
    {
        if (TankAPI.CurrentState.TargetInSight)
            return TankAction.FireCannon;

        if (TankAPI.CurrentState[Direction.Front] > 1) {
            return TankAction.MoveForward;
        }

        return AvoidDeadEnd();
    }

    public bool IsComplete()
    {
        return false;
    }

    private TankAction AvoidDeadEnd()
    {
        var action = TankAction.MoveBackward;

        var left_available = TankAPI.CurrentState[Direction.Left] > 1;
        var right_available = TankAPI.CurrentState[Direction.Right] > 1;

        if (left_available && right_available) {

            action = TankAPI.CurrentState[Direction.Left] >= TankAPI.CurrentState[Direction.Right] ?
                TankAction.TurnLeft :
                TankAction.TurnRight;

        } else if (left_available) {

            action = TankAction.TurnLeft;

        } else if (right_available) {

            action = TankAction.TurnRight;
        }

        return action;
    }
}
