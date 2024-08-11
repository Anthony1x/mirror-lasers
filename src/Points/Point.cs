using System.Numerics;
namespace mirror_lasers;

using ExtensionMethods;
using Raylib_cs;

public abstract class Point
{
    public System.Drawing.PointF RealPoint;
    public System.Drawing.Point DrawnPoint;

    public float Radius;

    public Color Color;

    protected System.Drawing.Rectangle Constraints;


    public Point(System.Drawing.PointF point, float radius, Color color, System.Drawing.Rectangle constraints)
    {
        RealPoint = point;
        Radius = radius;
        Color = color;
        Constraints = constraints;

        SetDrawnPoint();
    }

    public Point()
    {
        RealPoint = new(0, 0);
        Radius = 10f;
        Color = Color.DarkGray;
        Constraints = new(0, 0, 0, 0);

        SetDrawnPoint();
    }

    public virtual void Update()
    {
        bool up;
        bool left;
        bool down;
        bool right;

        if (this is Shooter)
        {
            up = Raylib.IsKeyDown(KeyboardKey.W);
            left = Raylib.IsKeyDown(KeyboardKey.A);
            down = Raylib.IsKeyDown(KeyboardKey.S);
            right = Raylib.IsKeyDown(KeyboardKey.D);
        }
        else
        {
            up = false;
            left = false;
            down = false;
            right = false;
        }

        if (left)
        {
            RealPoint.X -= 1f * Raylib.GetFrameTime();
        }
        if (up)
        {
            RealPoint.Y -= 1f * Raylib.GetFrameTime();
        }
        if (right)
        {
            RealPoint.X += 1f * Raylib.GetFrameTime();
        }
        if (down)
        {
            RealPoint.Y += 1f * Raylib.GetFrameTime();
        }

        SetDrawnPoint();
    }

    public void Draw(float radius = 10f)
    {
        Raylib.DrawCircle(DrawnPoint.X, DrawnPoint.Y, radius, Color);
    }

    protected void SetDrawnPoint()
    {
        RealPoint.X = Math.Clamp(RealPoint.X, 0, 1);
        RealPoint.Y = Math.Clamp(RealPoint.Y, 0, 1);

        DrawnPoint.X = (int)RealPoint.X.Map(0, 1, Constraints.X, Constraints.Right);
        DrawnPoint.Y = (int)RealPoint.Y.Map(0, 1, Constraints.Y, Constraints.Bottom);
    }
}