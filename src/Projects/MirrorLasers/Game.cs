using ExtensionMethods;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace mirror_lasers;

sealed class Game
{
    private static readonly Lazy<Game> Lazy = new(() => new Game());
    public static Game Instance { get { return Lazy.Value; } }

    public Square[,] Squares = new Square[5, 5];

    public int Rows;
    public int Columns;

    public int WindowWidth, WindowHeight;
    public int MiddleX, MiddleY;

    readonly Camera Camera = new();

    /// <summary>
    /// We know that there will never be more than 16 blockers, so we may as well initialize it as such.
    /// </summary>
    public Blocker[,] Blockers = new Blocker[4, 4];

    // Shooter is initialized in Init()
    public Shooter Shooter = new Shooter();

    public Target[,] Targets = new Target[5, 5];

    private Game()
    {
        Rows = Squares.GetLength(0);
        Columns = Squares.GetLength(1);
    }

    public void Init(int windowWidth, int windowHeight)
    {
        WindowWidth = windowWidth;
        WindowHeight = windowHeight;
        MiddleX = windowWidth / 2;
        MiddleY = windowHeight / 2;

        InitWindow(WindowWidth, WindowHeight, "Mirror lasers");

        SetTargetFPS(360);

        var sides = WindowHeight / 100 * 98;

        Parallel.For(0, Rows, row =>
        {
            for (int col = 0; col < Columns; col++)
            {
                // Adjust the offset calculation to accommodate both row and column
                int offsetX = col * sides;
                int offsetY = row * sides;

                Squares[row, col] = new Square(new Rectangle(MiddleX + offsetX, MiddleY - offsetY, sides, sides));

                var rectangle = Squares[row, col].Rectangle.ToSysRect();

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
                Targets[row, col] = new Target(
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

        var rect = Squares[0, 0].Rectangle.ToSysRect();
        GeneralUtility.MainSquare = rect;

        Shooter = new Shooter(new(0.1f, 0.7f), 10f, Color.Green, rect);

        Parallel.For(0, 4, row =>
        {
            for (int col = 0; col < 4; col++)
            {
                Blockers[row, col] = new Blocker(new(0, 0), 10f, Color.Orange);
            }
        });

    }

    public void Update()
    {
        Parallel.For(0, Rows, row =>
        {
            for (int col = 0; col < Columns; col++)
            {
                Squares[row, col].Update();
                Targets[row, col].Update();
            }
        });

        // Blockers. Yee haw!
        Parallel.For(0, 4, row =>
        {
            for (int col = 0; col < 4; col++)
            {
                var b = Blockers[row, col];

                b.DrawnPoint = GeneralUtility.Midpoint(Shooter.DrawnPoint, Targets[row, col].DrawnPoint).ToPoint();

                b.Update();

                b.TranslateToOrigin(GeneralUtility.MainSquare);
            }
        });

        Shooter.Update();
        Camera.Update();
    }

    public void Draw()
    {
        BeginDrawing();
        ClearBackground(Color.Black);
        BeginMode2D(Camera.GetCamera());

        var radius = Math.Min(10f, Targets[0, 0].Radius / Camera.GetCamera().Zoom);

        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                // Make sure all lines are always exactly one pixel wide, no matter the zoom.
                // Otherwise, the lines disappear (less than 1 pixel wide can't be displayed).
                // Shame Raylib doesn't handle this by itself.
                Squares[row, col].Draw(1 / Camera.GetCamera().Zoom);
                Targets[row, col].Draw(radius);

                // var midPoint = GeneralUtility.Midpoint(shooter.DrawnPoint, targets[row, col].DrawnPoint).ToPoint();

                // DrawLine(shooter.DrawnPoint.X, shooter.DrawnPoint.Y, midPoint.X, midPoint.Y, Color.Red);
                // DrawLine(midPoint.X, midPoint.Y, targets[row, col].DrawnPoint.X, targets[row, col].DrawnPoint.Y, Color.DarkGray);
            }
        }

        for (int row = 0; row < 4; row++)
        {
            for (int col = 0; col < 4; col++)
            {
                Blockers[row, col].Draw(radius);
            }
        }

        Shooter.Draw(radius);

        EndMode2D();
        EndDrawing();
    }
}