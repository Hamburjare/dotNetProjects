using Raylib_CsLo;
using System;
using System.Numerics;

namespace raylib_test
{
    class Program
    {
        public static void Main()
        {
            WindowTest test = new WindowTest();
            test.Run();
        }
    }

    class WindowTest
    {
        const int screen_width = 800;
        const int screen_height = 600;

        public WindowTest() { }

        public void Run()
        {
            Raylib.InitWindow(screen_width, screen_height, "Raylib");
            Raylib.SetTargetFPS(60);

            while (Raylib.WindowShouldClose() == false)
            {
                Update();
                Draw();
            }

            Raylib.CloseWindow();
        }

        private void Draw()
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.DARKGREEN);


            Raylib.EndDrawing();
        }

        void DrawChessBoard()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        Raylib.DrawRectangle(i * 100, j * 100, 100, 100, Raylib.WHITE);
                    }
                    else
                    {
                        Raylib.DrawRectangle(i * 100, j * 100, 100, 100, Raylib.BLACK);
                    }
                }
            }
        }

        void DrawShapes()
        {
            Raylib.DrawCircle(screen_width / 2 + 150, screen_height / 2, 50, Raylib.RED);
            Raylib.DrawRectangle(
                screen_width / 2 - 200,
                screen_height / 2 - 50,
                100,
                100,
                Raylib.BLUE
            );
            Raylib.DrawTriangle(
                new Vector2(screen_width / 2, screen_height / 2 - 50),
                new Vector2(screen_width / 2 - 50, screen_height / 2 + 50),
                new Vector2(screen_width / 2 + 50, screen_height / 2 + 50),
                Raylib.GREEN
            );
        }

        void DrawChristmasTree()
        {
            Raylib.DrawRectangle(
                screen_width / 2 - 10,
                screen_height / 2 + 100,
                20,
                100,
                Raylib.BROWN
            );
            Raylib.DrawTriangle(
                new Vector2(screen_width / 2 - 50, screen_height / 2 + 100),
                new Vector2(screen_width / 2 + 50, screen_height / 2 + 100),
                new Vector2(screen_width / 2, screen_height / 2 - 50),
                Raylib.GREEN
            );
            Raylib.DrawTriangle(
                new Vector2(screen_width / 2 - 50, screen_height / 2 + 50),
                new Vector2(screen_width / 2 + 50, screen_height / 2 + 50),
                new Vector2(screen_width / 2, screen_height / 2 - 100),
                Raylib.GREEN
            );
            Raylib.DrawTriangle(
                new Vector2(screen_width / 2 - 50, screen_height / 2),
                new Vector2(screen_width / 2 + 50, screen_height / 2),
                new Vector2(screen_width / 2, screen_height / 2 - 150),
                Raylib.GREEN
            );
        }

        private void Update()
        {
            // Update game here
        }
    }
}
