using System;
using System.Linq;
using System.Collections.Generic;

public class Solution
{
    private int _turnCount = 0;

    public Solution()
    {
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
        Console.WriteLine("Turn #{0}", _turnCount++);
        Console.WriteLine();
        
        Console.WriteLine(TankState.Current);
        Console.WriteLine();
        Console.WriteLine();
           
    }
   
}
