namespace mirror_lasers;

using Raylib_cs;

public class Blocker
{
    public System.Drawing.Point DrawnPoint;

    public float Radius;

    public Color Color;

    public Blocker(System.Drawing.Point point, float radius, Color color)
    {
        DrawnPoint = point;
        Radius = radius;
        Color = color;
    }

    public Blocker()
    {
        DrawnPoint = new(0, 0);
        Radius = 0f;
        Color = Color.DarkGray;
    }

    public virtual void Update()
    {

    }

    public void Draw(float radius = 10f)
    {
        Raylib.DrawCircle(DrawnPoint.X, DrawnPoint.Y, radius, Color);
    }
}