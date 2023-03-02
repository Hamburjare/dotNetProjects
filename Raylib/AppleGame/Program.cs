using System.Numerics;
using Raylib_CsLo;
using System;
using GameEngine6000;

namespace AppleGame;

class Program
{
    static void Main(string[] args)
    {
        DrawGame game = new DrawGame();
        game.Run();
    }
}

class DrawGame
{
    int RandomY(int screen_height)
    {
        Random r = new Random();
        int randomY = r.Next(screen_height + 1);
        return randomY;
    }

    int RandomX(int screen_width)
    {
        Random r = new Random();
        int randomX = r.Next(screen_width + 1);
        return randomX;
    }

    int screen_width = 800;
    int screen_height = 600;

    public DrawGame() { }

    public void Run()
    {
        Player player = new Player(
            new Vector2(screen_height / 2, screen_width / 2),
            5.0f,
            Raylib.BLUE,
            new Vector2(50, 50)
        );
        Apple apple = new Apple(
            new Vector2(RandomX(screen_width), RandomY(screen_height)),
            50,
            50,
            Raylib.RED
        );
        Raylib.InitWindow(screen_width, screen_height, "Apple Game");
        Raylib.SetTargetFPS(60);

        while (Raylib.WindowShouldClose() == false)
        {
            player.transform.Update();
            player.Draw();
            apple.Draw();
            Draw();
            GameEngine6000.Collision collision = new GameEngine6000.Collision();

            if (
                collision.CheckCollision(
                    player.spriteRenderer.GetRectangle(),
                    apple.spriteRenderer.GetRectangle()
                )
            )
            {
                apple.transform.position.X = RandomX(
                    screen_width - (int)apple.spriteRenderer.size.X
                );
                apple.transform.position.Y = RandomY(
                    screen_height - (int)apple.spriteRenderer.size.Y
                );
            }
            // Update(player, apple);
        }

        Raylib.CloseWindow();
    }

    private void Draw()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Raylib.DARKGREEN);

        Raylib.EndDrawing();
    }

}

class Player
{
    public GameEngine6000.Transform transform;
    public GameEngine6000.SpriteRenderer spriteRenderer;

    // Constructor, creates a new player

    public Player(Vector2 position, float velocity, Color color, Vector2 size)
    {
        transform = new GameEngine6000.Transform(position, velocity);

        spriteRenderer = new GameEngine6000.SpriteRenderer(position, color, size);
    }

    // Draw is called after Update()
    public void Draw()
    {
        spriteRenderer.position = transform.position;
        spriteRenderer.Draw();
    }
}

class Apple
{
    public GameEngine6000.SpriteRenderer spriteRenderer;
    public GameEngine6000.Transform transform;

    public Apple(Vector2 position, int width, int height, Color color)
    {
        transform = new GameEngine6000.Transform(position, 0.0f);

        spriteRenderer = new GameEngine6000.SpriteRenderer(
            position,
            color,
            new Vector2(width, height)
        );
    }

    public void Draw()
    {
        spriteRenderer.position = transform.position;
        spriteRenderer.Draw();
    }
}

