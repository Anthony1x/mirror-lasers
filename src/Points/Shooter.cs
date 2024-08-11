using Raylib_cs;

namespace mirror_lasers;

class Shooter : Point
{
    public Shooter(System.Drawing.PointF point, float radius, Color color, System.Drawing.Rectangle constraints) : base(point, radius, color, constraints)
    {
    }

    public Shooter(){}
}