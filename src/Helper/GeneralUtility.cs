using System.Drawing;
using System.Numerics;
using Raylib_cs;

namespace mirror_lasers;

public static class GeneralUtility
{
    public static System.Drawing.Rectangle mainSquare;

    public static (float x, float y) ConvertRelativeToAbsolute(float relativeX, float relativeY, int squareWidth, int squareHeight)
    {
        Game game = Game.Instance;

        // Calculate the conversion factors based on the square's dimensions and the screen's dimensions
        float scaleX = squareWidth / game.WindowSize.X;
        float scaleY = squareHeight / game.WindowSize.Y;

        // Adjust the relative coordinates to account for the square's position on the screen
        float adjustedRelativeX = relativeX - game.middle.X / game.WindowSize.X;
        float adjustedRelativeY = relativeY - game.middle.Y / game.WindowSize.Y;

        // Apply the conversion to the adjusted coordinates
        float worldX = adjustedRelativeX * scaleX + game.middle.X;
        float worldY = adjustedRelativeY * scaleY + game.middle.Y;

        return (worldX, worldY);
    }

    public static System.Drawing.Point ToMainSquare(System.Drawing.Point point)
    {
        var distance = CalculateTileDistances(point);

        var x = distance.Item1;
        var y = distance.Item2;

        // Point is already at (0,0). Nothing to do.
        // if (x == 0 && y == 0)
        return point;
    }

    public static Tuple<int, int> CalculateTileDistances(System.Drawing.Point targetPoint)
    {
        // Assuming gridBounds is a square rectangle with equal width and height
        int tileSize = Math.Min(mainSquare.Width, mainSquare.Height); // Size of a single tile
        int halfGridSize = mainSquare.Width / 2; // Center of the grid in X direction

        // Initialize tile distances
        int tileXDistance = 0;
        int tileYDistance = 0;

        // Check if the point's X coordinate exceeds the right boundary of the grid
        if (targetPoint.X > halfGridSize)
        {
            tileXDistance += (int)Math.Ceiling((double)(targetPoint.X - halfGridSize) / tileSize) + 1;
        }

        // Check if the point's Y coordinate exceeds the bottom boundary of the grid
        if (targetPoint.Y > mainSquare.Bottom - tileSize)
        {
            tileYDistance += (int)Math.Ceiling((double)(targetPoint.Y - (mainSquare.Bottom - tileSize)) / tileSize) + 1;
        }

        // Return the distances as a Tuple<int, int>
        return new Tuple<int, int>(tileXDistance, tileYDistance);
    }

    public static Vector2 Midpoint(Vector2 a, Vector2 b) => new Vector2((a.X + b.X) / 2, (a.Y + b.Y) / 2);

    public static PointF Midpoint(PointF a, PointF b) => new PointF((a.X + b.X) / 2, (a.Y + b.Y) / 2);
}