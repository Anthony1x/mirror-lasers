using System.Drawing;
using System.Numerics;
using ExtensionMethods;

namespace mirror_lasers;

public static class GeneralUtility
{
    public static System.Drawing.Rectangle MainSquare;

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

    public static System.Drawing.Point? GetIntersection(System.Drawing.Point p1Start, System.Drawing.Point p1End, System.Drawing.Point p2Start, System.Drawing.Point p2End)
    {
        // Check if the lines are parallel
        float denominator = (p1Start.X - p1End.X) * (p2Start.Y - p2End.Y) - (p1Start.Y - p1End.Y) * (p2Start.X - p2End.X);

        if (denominator == 0)
            return null; // Lines are parallel, no intersection

        // Calculate t parameter
        float t = ((p1Start.X - p2Start.X) * (p2Start.Y - p2End.Y) - (p1Start.Y - p2Start.Y) * (p2Start.X - p2End.X)) / denominator;

        // Calculate u parameter
        float u = -((p1Start.X - p1End.X) * (p1Start.Y - p2Start.Y) - (p1Start.Y - p1End.Y) * (p1Start.X - p2Start.X)) / denominator;

        // Check if the intersection point lies within the line segments
        if (t >= 0 && t <= 1 && u >= 0 && u <= 1)
        {
            return new((int)(p1Start.X + t * (p1End.X - p1Start.X)), (int)(p1Start.Y + t * (p1End.Y - p1Start.Y)));
        }

        return null; // Intersection point is outside the line segments
    }

    /// <summary>
    /// This function returns the Lines ("lasers") that need to be drawn by the mirrors
    /// </summary>
    /// <param name="startPoint"></param>
    /// <param name="endPoint"></param>
    /// <returns></returns>
    public static List<Tuple<System.Drawing.Point, System.Drawing.Point>> GetLinesToDraw(System.Drawing.Point startPoint, System.Drawing.Point endPoint)
    {
        List<Tuple<System.Drawing.Point, System.Drawing.Point>> lines = new();

        // We can ignore lines that are already within the bounds of the mainSquare.
        if (MainSquare.Contains(endPoint))
        {
            lines.Add(new(startPoint, endPoint));

            return lines;
        }

        // This means we know for a fact that everything that follows must be outside.
        Tuple<int, int> distance = CalculateTileDistances(endPoint);

        for (int i = 0; i < distance.Item1; i++)
        {
            System.Drawing.Point? right = GetIntersection(startPoint, endPoint, MainSquare.RightBound().Item1, MainSquare.RightBound().Item2);


            // Get the inflection point for all sides of the square.
            // We need this to figure out where the next line needs to start drawing.

            if (right != null)
                lines.Add(new(startPoint, endPoint));
        }

        // Logic here

        return lines;
    }

    public static Vector2 Midpoint(Vector2 a, Vector2 b) => new Vector2((a.X + b.X) / 2, (a.Y + b.Y) / 2);

    public static PointF Midpoint(PointF a, PointF b) => new PointF((a.X + b.X) / 2, (a.Y + b.Y) / 2);
}