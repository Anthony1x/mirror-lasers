
using Raylib_cs;

namespace mirror_lasers;

public class Square
{
    public Rectangle Rectangle;

    public Square(Rectangle rectangle)
    {
        Rectangle = rectangle;

        Rectangle.X -= rectangle.Width / 2;
        Rectangle.Y -= rectangle.Height / 2;
    }

    public void Update()
    {

    }

    public void Draw(float lineThick)
    {
        Raylib.DrawRectangleLinesEx(Rectangle, lineThick, Color.RayWhite);
    }
}