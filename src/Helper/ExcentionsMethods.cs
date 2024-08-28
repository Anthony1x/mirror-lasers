using System.Drawing;

namespace ExtensionMethods;

public static class ExtensionMethods
{
    private static Random rng = new Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static IList<T> Swap<T>(this IList<T> list, int indexA, int indexB)
    {
        T tmp = list[indexA];
        list[indexA] = list[indexB];
        list[indexB] = tmp;
        return list;
    }


    public static int Map(this int value, int fromSource, int toSource, int fromTarget, int toTarget)
    {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }

    public static decimal Map(this decimal value, decimal fromSource, decimal toSource, decimal fromTarget, decimal toTarget)
    {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }

    public static float Map(this float value, float fromSource, float toSource, float fromTarget, float toTarget)
    {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }


    public static double Map(this double value, double fromSource, double toSource, double fromTarget, double toTarget)
    {
        return (value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget;
    }

    public static Point ToPoint(this PointF pointF) => new Point((int)pointF.X, (int)pointF.Y);

    public static PointF ToPointF(this Point point) => new PointF(point.X, point.Y);

    public static Rectangle ToSysRect(this Raylib_cs.Rectangle rayRect) => new((int)rayRect.X, (int)rayRect.Y, (int)rayRect.Width, (int)rayRect.Height);

    public static Tuple<Point, Point> RightBound(this Rectangle rectangle) => new Tuple<Point, Point>(new(rectangle.X + rectangle.Width, rectangle.Y), new(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height));
}