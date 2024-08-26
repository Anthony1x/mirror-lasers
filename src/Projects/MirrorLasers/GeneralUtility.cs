using System.Drawing;
using System.Numerics;
using Raylib_cs;

namespace mirror_lasers;

public static class GeneralUtility
{
    public static System.Drawing.Rectangle MainSquare;

    public static (float x, float y) ConvertRelativeToAbsolute(float relativeX, float relativeY, int squareWidth, int squareHeight)
    {
        Game game = Game.Instance;

        // Calculate the conversion factors based on the square's dimensions and the screen's dimensions
        float scaleX = squareWidth / game.WindowWidth;
        float scaleY = squareHeight / game.WindowHeight;

        // Adjust the relative coordinates to account for the square's position on the screen
        float adjustedRelativeX = relativeX - game.MiddleX / game.WindowWidth;
        float adjustedRelativeY = relativeY - game.MiddleY / game.WindowHeight;

        // Apply the conversion to the adjusted coordinates
        float worldX = adjustedRelativeX * scaleX + game.MiddleX;
        float worldY = adjustedRelativeY * scaleY + game.MiddleY;

        return (worldX, worldY);
    }

    public static Tuple<int, int> CalculateTileDistances(System.Drawing.Point targetPoint)
    {
        // Assuming gridBounds is a square rectangle with equal width and height
        int tileSize = Math.Min(MainSquare.Width, MainSquare.Height); // Size of a single tile
        int halfGridSize = MainSquare.Width / 2; // Center of the grid in X direction

        // Initialize tile distances
        int tileXDistance = 0;
        int tileYDistance = 0;

        // Check if the point's X coordinate exceeds the right boundary of the grid
        if (targetPoint.X > halfGridSize)
        {
            tileXDistance += (int)Math.Ceiling((double)(targetPoint.X - halfGridSize) / tileSize) + 1;
        }

        // Check if the point's Y coordinate exceeds the bottom boundary of the grid
        if (targetPoint.Y > MainSquare.Bottom - tileSize)
        {
            tileYDistance += (int)Math.Ceiling((double)(targetPoint.Y - (MainSquare.Bottom - tileSize)) / tileSize) + 1;
        }

        // Return the distances as a Tuple<int, int>
        return new Tuple<int, int>(tileXDistance, tileYDistance);
    }

    public static Vector2 Midpoint(Vector2 a, Vector2 b) => new Vector2((a.X + b.X) / 2, (a.Y + b.Y) / 2);

    public static PointF Midpoint(PointF a, PointF b) => new PointF((a.X + b.X) / 2, (a.Y + b.Y) / 2);
}