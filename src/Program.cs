﻿using Raylib_cs;
using static Raylib_cs.Raylib;

/*
    TODO:
    - Important: GET THE BLOCKERS ON (0,0)

    - To generate the blockers:
        - First calculate the blockers' positions for (n, 0) and (0, n)
        - Some blockers may not be on tile (0,0). Make sure to get the blocker on fucking (0,0) by mirroring it back.
            - how tho
        - Using this information, place blockers wherever a blocker exists at an existing blockers' x, but not y and vice versa.
        - Because the blockers repeat after (4,4), this would mean that only a total of 16 blockers are necessary.

    - There need to be sixteen blockers total
    - Grid is 5x5 for now. Add negative coordinates later (though in a perfect world it should just work flawlessly)

    - Add switch between local laser bounces and direct laser bounces. Calculate reflections.

    - Once it works, expand the grid to infinity by frustum culling to the viewport.
        - Shooter is always drawn.
*/

class Program
{
    public static void Main(string[] args)
    {
        dynamic game = args[0] switch
        {
            "mirror-lasers" => new mirror_lasers.Game(1920, 1080),
            "sorting-algorithms" => new sorting_algorithms.Game(1920, 1080),
            _ => throw new Exception("Invalid project specified, dumbass"),
        };

        game.Init();

        while (!WindowShouldClose())
        {
            game.Update();
            game.Draw();
        }

        CloseWindow();
    }
}