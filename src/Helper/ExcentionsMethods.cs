using System.Drawing;

namespace ExtensionMethods;

public static class ExtensionMethods
{
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
}