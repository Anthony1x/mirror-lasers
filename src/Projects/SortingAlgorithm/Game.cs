using ExtensionMethods;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace sorting_algorithms;

sealed class Game
{
    public int WindowWidth, WindowHeight;
    public int MiddleX, MiddleY;

    List<int> lines;

    public Game(int windowWidth, int windowHeight, int? numLines = null)
    {
        WindowWidth = windowWidth;
        WindowHeight = windowHeight;
        MiddleX = windowWidth / 2;
        MiddleY = windowHeight / 2;

        numLines ??= windowWidth / 2;

        int lineHeight = (int)(WindowHeight / numLines);

        lines = new List<int>();

        for (int i = 0; i < numLines; i++)
        {
            lines.Add(i * lineHeight);
        }

        lines.Shuffle();
    }

    public void Init()
    {
        InitWindow(WindowWidth, WindowHeight, "Mirror lasers");

        SetTargetFPS(360);
    }

    public void Update()
    {
        lines = Algorithms.BubbleSort(lines);
    }

    public void Draw()
    {
        BeginDrawing();
        ClearBackground(Color.Black);

        var lineWidth = (WindowWidth / lines.Count);

        for (int i = 0; i < lines.Count; i++)
        {
            DrawLineEx(
                new((i * lineWidth) + (lineWidth / 2), WindowHeight),
                new((i * lineWidth) + (lineWidth / 2), WindowHeight - lines[i]),
                lineWidth,
                Color.RayWhite
            );
        }

        EndDrawing();
    }
}