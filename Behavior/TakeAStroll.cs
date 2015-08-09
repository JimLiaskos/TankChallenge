
public class TakeAStroll : BaseBehavior, ITankBehavior
{
    public override TankAction GetNextAction()
    {
       return ChooseAction(GetNextActionInternal());
    }

    private TankAction GetNextActionInternal() {
        if (TankAPI.CurrentState[Direction.Front] > 1) {
            return TankAction.MoveForward;
        }

        return AvoidDeadEnd();
    }

    private TankAction AvoidDeadEnd()
    {
        var direction = Direction.Back;

        var left_available = TankAPI.CurrentState[Direction.Left] > 1;
        var right_available = TankAPI.CurrentState[Direction.Right] > 1;

        if (left_available && right_available) {

            direction = TankAPI.CurrentState[Direction.Left] >= TankAPI.CurrentState[Direction.Right] ?
                Direction.Left :
                Direction.Right;

        } else if (left_available) {

            direction = Direction.Left;

        } else if (right_available) {

            direction = Direction.Right;
        }

        return TankAction.MoveForward;
    }
}
