using System;
using System.Linq;
using System.Collections.Generic;

public class Solution
{
    private int _turnCount = 0;

    private ITankBehavior _behavior;


    public Solution()
    {
        _behavior = new TakeAStroll();

        // If you need initialization code, you can write it here!
    }

    /**
     * Executes a single step of the tank's programming. The tank can only move,
     * turn, or fire its cannon once per turn. Between each update, the tank's
     * engine remains running and consumes 1 fuel. This function will be called
     * repeatedly until there are no more targets left on the grid, or the tank
     * runs out of fuel.
     */
    public void Update()
    {
        // Todo: Write your code here!
        Console.WriteLine("Turn #{0}", ++_turnCount);
        Console.WriteLine();

        Loop();

    }

    public void Loop()
    {

        TankAPI.NewTurn();

        var action = _behavior.GetNextAction();
        TankAPI.PerformAction(action);

        Console.WriteLine(TankState.Current);
        Console.WriteLine("Action: {0}", action);
        Console.WriteLine();

        TankAPI.CompleteTurn();    
    }

}
