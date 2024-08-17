using ExtensionMethods;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace mirror_lasers;

sealed class Game
{
    private static readonly Lazy<Game> lazy = new(() => new Game());
    public static Game Instance { get { return lazy.Value; } }

    public Square[,] squares = new Square[5, 5];

    public int rows;
    public int columns;

    public Vector2<int> WindowSize;
    public Vector2<int> middle;

    Camera camera = new();

    /// <summary>
    /// We know that there will never be more than 16 blockers, so we may as well initialize it as such.
    /// </summary>
    public Blocker[,] blockers = new Blocker[4, 4];

    // Shooter is initialized in Init()
    public Shooter shooter = new Shooter();

    public Target[,] targets = new Target[5, 5];

    private Game()
    {
        rows = squares.GetLength(0);
        columns = squares.GetLength(1);
    }

    public void Init(Vector2<int> windowSize)
    {
        WindowSize = windowSize;
        middle = new(WindowSize.X / 2, WindowSize.Y / 2);

        InitWindow(WindowSize.X, WindowSize.Y, "Mirror lasers");

        SetTargetFPS(360);

        var sides = windowSize.Y / 100 * 98;

        Parallel.For(0, rows, row =>
        {
            for (int col = 0; col < columns; col++)
            {
                // Adjust the offset calculation to accommodate both row and column
                var offsetX = col * sides;
                var offsetY = row * sides;

                squares[row, col] = new Square(new Rectangle(middle.X + offsetX, middle.Y - offsetY, sides, sides));

                var rectangle = squares[row, col].rectangle.ToSysRect();

                bool flippedVertically = col % 2 == 0;
                bool flippedHorizontally = row % 2 == 0;

                Target.FlippedState flippedState;

                if (flippedHorizontally && flippedVertically)
                {
                    flippedState = Target.FlippedState.MirroredBoth;
                }
                else if (flippedHorizontally && !flippedVertically)
                {
                    flippedState = Target.FlippedState.MirroredHorizontally;
                }
                else if (!flippedHorizontally && flippedVertically)
                {
                    flippedState = Target.FlippedState.MirroredVertically;
                }
                else
                {
                    flippedState = Target.FlippedState.Unmirrored;
                }

                float targetPosX = .8f;
                float targetPosY = .5f;

                // Absolutely INSANE code copied from ChatGPT that mirrors the points if their tile is uneven.
                targets[row, col] = new Target(
                    new(
                        flippedVertically ? targetPosX : 1 - targetPosX,
                        flippedHorizontally ? targetPosY : 1 - targetPosY),
                    10f,
                    Color.Blue,
                    rectangle,
                    flippedState
                );
            }
        });

        // Blockers. Yee haw!
        Parallel.For(0, 4, row =>
        {
            blockers[row, 0] = new Blocker(new(0, 0), 10f, Color.Orange);
        });

        Parallel.For(0, 4, col =>
        {
            blockers[0, col] = new Blocker(new(0, 0), 10f, Color.Orange);
        });

        var rect = squares[0, 0].rectangle.ToSysRect();
        GeneralUtility.mainSquare = rect;

        shooter = new Shooter(new(0.1f, 0.7f), 10f, Color.Green, rect);
    }

    public void Update()
    {
        var sysrect = squares[0, 0].rectangle.ToSysRect();


        Parallel.For(0, rows, row =>
        {
            for (int col = 0; col < columns; col++)
            {
                squares[row, col].Update();
                targets[row, col].Update();
            }
        });

        // Blockers. Yee haw!
        Parallel.For(0, 4, row =>
        {
            blockers[row, 0].DrawnPoint = GeneralUtility.Midpoint(shooter.DrawnPoint, targets[row, 0].DrawnPoint).ToPoint();

            blockers[row, 0].Update();

            blockers[row, 0].TranslateToOrigin(sysrect);
        });

        Parallel.For(0, 4, col =>
        {
            blockers[0, col].DrawnPoint = GeneralUtility.Midpoint(shooter.DrawnPoint, targets[0, col].DrawnPoint).ToPoint();

            blockers[0, col].Update();

            blockers[0, col].TranslateToOrigin(sysrect);
        });

        shooter.Update();
        camera.Update();
    }

    public void Draw()
    {
        BeginDrawing();
        ClearBackground(Color.Black);
        BeginMode2D(camera.GetCamera());

        var radius = Math.Min(10f, targets[0, 0].Radius / camera.GetCamera().Zoom);

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                // Make sure all lines are always exactly one pixel wide, no matter the zoom.
                // Otherwise, the lines disappear (less than 1 pixel wide can't be displayed).
                // Shame Raylib doesn't handle this by itself.
                squares[row, col].Draw(1 / camera.GetCamera().Zoom);
                targets[row, col].Draw(radius);

                // var midPoint = GeneralUtility.Midpoint(shooter.DrawnPoint, targets[row, col].DrawnPoint).ToPoint();

                // DrawLine(shooter.DrawnPoint.X, shooter.DrawnPoint.Y, midPoint.X, midPoint.Y, Color.Red);
                // DrawLine(midPoint.X, midPoint.Y, targets[row, col].DrawnPoint.X, targets[row, col].DrawnPoint.Y, Color.DarkGray);
            }
        }

        for (int row = 0; row < 4; row++)
        {
            blockers[row, 0].Draw(radius);
        }

        for (int col = 0; col < 4; col++)
        {
            blockers[0, col].Draw(radius);
        }

        shooter.Draw(radius);

        EndMode2D();
        EndDrawing();
    }
}