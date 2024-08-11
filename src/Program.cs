using static Raylib_cs.Raylib;

namespace mirror_lasers;
/*
    TODO:
    - Important: GET THE BLOCKERS ON (0,0)
    - There need to be sixteen blockers total
    - Grid is 5x5 for now. Add negative coordinates later (though in a perfect world it should just work flawlessly)

    - Add switch between local laser bounces and direct laser bounces. Calculate reflections.

    - Once it works, expand the grid to infinity by frustum culling to the viewport.
        - Shooter is always drawn.
*/

class Program
{
    public static void Main()
    {
        Game game = Game.Instance;

        game.Init(new(1920, 1080));

        while (!WindowShouldClose())
        {
            game.Update();
            game.Draw();
        }

        CloseWindow();
    }
}