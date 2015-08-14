
public class Point
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// 
    /// </summary>
    public int X { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    public int Y { get; private set; }

    public static Point operator +(Point one, Point other)
    {
        return new Point(one.X + other.X, one.Y + other.Y);
    }

    public static Point operator *(Point one, Point other)
    {
        return new Point(one.X * other.X, one.Y * other.Y);
    }

    public static Point operator -(Point one, Point other)
    {
        return new Point(one.X - other.X, one.Y - other.Y);
    }

    public static bool operator ==(Point one, Point other)
    {
        if (System.Object.ReferenceEquals(one, other)) {
            return true;
        }

        if (((object)one == null) || ((object)other == null)) {
            return false;
        }

        return one.Equals(other);
    }

    public static bool operator !=(Point one, Point other)
    {
        return !(one == other);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        int hash = 17;
        hash = ((hash + X) << 5) - (hash + X);
        hash = ((hash + Y) << 5) - (hash + Y);
        return hash;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        if (obj == null) {
            return false;
        }

        if ((obj as Point) == null) {
            return false;
        }

        return obj.GetHashCode() == this.GetHashCode();
    }

    public override string ToString()
    {
        return string.Format("{0}, {1}", X.ToString("##"), Y.ToString("##"));
    }

}

