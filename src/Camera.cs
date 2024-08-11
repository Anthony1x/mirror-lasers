using Raylib_cs;
using static Raylib_cs.Raylib;

namespace mirror_lasers;

class Camera
{
    Camera2D camera = new Camera2D(new(0, 0), new(0, 0), 0f, 1f);

    public Camera2D GetCamera() => camera;

    public void Update()
    {
        if (IsMouseButtonDown(MouseButton.Right))
        {
            var delta = GetMouseDelta();
            delta = Raymath.Vector2Scale(delta, -1.0f / camera.Zoom);
            camera.Target = Raymath.Vector2Add(camera.Target, delta);
        }

        float wheel = GetMouseWheelMove();
        if (wheel != 0)
        {
            // Get the world point that is under the mouse
            var mouseWorldPos = GetScreenToWorld2D(GetMousePosition(), camera);

            // Set the offset to where the mouse is
            camera.Offset = GetMousePosition();

            // Set the target to match, so that the camera maps the world space point
            // under the cursor to the screen space point under the cursor at any zoom
            camera.Target = mouseWorldPos;

            // Zoom increment
            float scaleFactor = 1.0f + (0.25f * Math.Abs(wheel));
            if (wheel < 0) scaleFactor = 1.0f / scaleFactor;
            camera.Zoom = Math.Clamp(camera.Zoom * scaleFactor, 1f / 6f, 64.0f);
        }
    }
}