
using System;

public abstract class BaseBehavior : ITankBehavior
{
    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public abstract TankAction GetNextAction();

    /// <summary>
    ///  Common behavior, if target in sight...SHOOT!!
    /// </summary>
    /// <returns></returns>
    protected TankAction ChooseAction(TankAction action)
    {
        if (TankAPI.CurrentState.TargetInSight)
            return TankAction.FireCannon;

        return action;
    }
}
