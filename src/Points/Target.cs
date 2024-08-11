using Raylib_cs;

namespace mirror_lasers;

class Target : Point
{
    public enum FlippedState
    {
        Unmirrored,
        MirroredHorizontally,
        MirroredVertically,
        MirroredBoth
    }

    FlippedState MirroredState;

    public Target(System.Drawing.PointF point, float radius, Color color, System.Drawing.Rectangle constraints, FlippedState mirroredState) : base(point, radius, color, constraints)
    {
        MirroredState = mirroredState;
    }

    public override void Update()
    {
        bool up = Raylib.IsKeyDown(KeyboardKey.Up);
        bool left = Raylib.IsKeyDown(KeyboardKey.Left);
        bool down = Raylib.IsKeyDown(KeyboardKey.Down);
        bool right = Raylib.IsKeyDown(KeyboardKey.Right);

        // Determine the flip factor based on the current MirroredState
        float xFlipFactor = -1f; // Default to no horizontal flip
        float yFlipFactor = -1f; // Default to no vertical flip

        switch (MirroredState)
        {
            case FlippedState.MirroredHorizontally:
                yFlipFactor = 1f; // Flip horizontal movement
                break;
            case FlippedState.MirroredVertically:
                xFlipFactor = 1f; // Flip vertical movement
                break;
            case FlippedState.MirroredBoth:
                xFlipFactor = 1f; // Flip horizontal movement
                yFlipFactor = 1f; // Flip vertical movement
                break;
        }

        // Apply the flip factors to the movement
        if (left)
        {
            RealPoint.X -= 1f * Raylib.GetFrameTime() * xFlipFactor;
        }
        if (up)
        {
            RealPoint.Y -= 1f * Raylib.GetFrameTime() * yFlipFactor;
        }
        if (right)
        {
            RealPoint.X += 1f * Raylib.GetFrameTime() * xFlipFactor;
        }
        if (down)
        {
            RealPoint.Y += 1f * Raylib.GetFrameTime() * yFlipFactor;
        }

        SetDrawnPoint();
    }

}