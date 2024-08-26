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

    /// <summary>
    /// Translate the given DrawnPoint to be inside of the given Rectangle's bounds.
    /// The rules are:
    /// if x is uneven, mirror the DrawnPoint's X axis.
    /// if y is uneven, mirror the DrawnPoint's Y axis.
    /// if both x and y are uneven, mirror both axes.
    /// </summary>
    /// <param name="bounds"></param>
    public void TranslateToOrigin(System.Drawing.Rectangle bounds)
    {
        int x = -(bounds.Left - DrawnPoint.X) / bounds.Width;
        int y = (bounds.Bottom - DrawnPoint.Y) / bounds.Height;

        if (x != 0)
        {
            // Calculate the translation required to move the point back to the origin
            int translateX = x * bounds.Width;

            DrawnPoint = new(DrawnPoint.X - translateX, DrawnPoint.Y);

            int a = DrawnPoint.X;

            if (x % 2 == 1)
            {
                // Calculate the midpoint of the rectangle
                int midX = bounds.Left + bounds.Width / 2;
                // Mirror the point across the midpoint
                a = 2 * midX - a;
            }

            DrawnPoint = new(a, DrawnPoint.Y);
        }

        if (y != 0)
        {
            // Calculate the translation required to move the point back to the origin
            int translateY = y * bounds.Height;

            DrawnPoint = new(DrawnPoint.X, DrawnPoint.Y + translateY);

            int b = DrawnPoint.Y;

            if (y % 2 == 1)
            {
                b = bounds.Bottom - b + b % 2;
            }

            DrawnPoint = new(DrawnPoint.X, b);
        }
    }
}