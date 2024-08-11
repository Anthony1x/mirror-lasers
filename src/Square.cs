
using Raylib_cs;

namespace mirror_lasers;

public class Square
{
    public Rectangle rectangle;

    public Square(Rectangle rectangle)
    {
        this.rectangle = rectangle;

        this.rectangle.X -= rectangle.Width / 2;
        this.rectangle.Y -= rectangle.Height / 2;
    }

    public void Update()
    {

    }

    public void Draw(float lineThick)
    {
        // Raylib.DrawRectangleLines((int)rectangle.X, (int)rectangle.Y, (int)rectangle.Width, (int)rectangle.Height, Color.Red);

        Raylib.DrawRectangleLinesEx(rectangle,lineThick, Color.RayWhite);
    }
}