
public class TakeAStroll : BaseBehavior, ITankBehavior
{
    public override TankAction GetNextAction()
    {
        return ChooseAction(GetNextActionInternal());
    }

    private TankAction GetNextActionInternal()
    {
        if (TankAPI.CurrentState[Direction.Front] > 1) {
            return TankAction.MoveForward;
        }

        return AvoidDeadEnd();
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
