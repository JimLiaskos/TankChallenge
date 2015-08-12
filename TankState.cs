
using System.Text;
using System.Collections.Generic;

public class TankState
{
    private readonly int _fuel;
    private readonly bool _targetInSight;
    private readonly Dictionary<Direction, int> _distances;

    public static TankState Current
    {
        get { return new TankState(); }
    }

    private TankState()
    {
        _fuel = API.CurrentFuel();
        _targetInSight = API.IdentifyTarget();

        _distances = new Dictionary<Direction, int> {
                {Direction.North, API.LidarFront()},
                {Direction.South, API.LidarBack()},
                {Direction.West, API.LidarLeft()},
                {Direction.East, API.LidarRight()}
            };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    public int this[Direction dir]
    { get { return _distances[dir]; } }

    /// <summary>
    /// 
    /// </summary>
    public int Fuel
    { get { return _fuel; } }

    /// <summary>
    /// 
    /// </summary>
    public bool TargetInSight
    { get { return _targetInSight; } }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        var builder = new StringBuilder();

        builder.AppendFormat("\t\tFR: {0}", this[Direction.North].ToString("##"));
        builder.AppendLine();

        builder.AppendFormat("\tLE: {0}\t\tRI: {1}",
            this[Direction.West].ToString("##"),
            this[Direction.East].ToString("##"));
        builder.AppendLine();

        builder.AppendFormat("\t\tBA: {0}", this[Direction.South].ToString("##"));
        builder.AppendLine();

        //builder.AppendFormat("FR: {0} BA: {1} LE: {2} RI: {3} ",
        //    this[Direction.Front].ToString("##"),
        //    this[Direction.Back].ToString("##"),
        //    this[Direction.Left].ToString("##"),
        //    this[Direction.Right].ToString("##"));

        builder.AppendLine();

        builder.AppendFormat("FUEL: {0}, TIS: {1}", Fuel.ToString("##"), TargetInSight);

        return builder.ToString();
    }
}
