using System.Numerics;
using Raylib_CsLo;
using System;

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
            50,
            50,
            5.0f,
            Raylib.BLUE
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
            player.Update(screen_width, screen_height);
            player.Draw();
            apple.Draw();
            Draw();
            Update(player, apple);
        }

        Raylib.CloseWindow();
    }

    private void Draw()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Raylib.DARKGREEN);

        Raylib.EndDrawing();
    }

    private void Update(Player player, Apple apple)
    {
        
        // check if player is colliding with apple
        if (player.position.X < apple.position.X + apple.width &&
            player.position.X + player.width > apple.position.X &&
            player.position.Y < apple.position.Y + apple.height &&
            player.position.Y + player.height > apple.position.Y)
        {
            // collision detected!
            apple.position.X = RandomX(screen_width - apple.width);
            apple.position.Y = RandomY(screen_height - apple.height);
        }
    }
}

class Player
{
    public Vector2 position;
    public int width;
    public int height;
    public float velocity;

    public Color color; // One of Raylib colors

    // Constructor, creates a new player

    public Player(Vector2 position, int width, int height, float velocity, Color color)
    {
        this.position = position;
        this.width = width;
        this.height = height;
        this.color = color;
        this.velocity = velocity;
    }

    // Update is called every frame
    public void Update(int screen_width, int screen_height)
    {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT) || Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            position.X += velocity;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) || Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            position.X -= velocity;
        }

        if (Raylib.IsKeyDown(KeyboardKey.KEY_UP) || Raylib.IsKeyDown(KeyboardKey.KEY_W))
        {
            position.Y -= velocity;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN) || Raylib.IsKeyDown(KeyboardKey.KEY_S))
        {
            position.Y += velocity;
        }

        // Prevent player from going outside the window

        position.X = Math.Clamp(position.X, 0, screen_width - width);
        position.Y = Math.Clamp(position.Y, 0, screen_height - height);
    }

    // Draw is called after Update()
    public void Draw()
    {
        Raylib.DrawRectangle((int)this.position.X, (int)this.position.Y, width, height, color);
    }
}

class Apple
{
    public Vector2 position;
    public int width;
    public int height;
    public Color color;

    public Apple(Vector2 position, int width, int height, Color color)
    {
        this.position = position;
        this.width = width;
        this.height = height;
        this.color = color;
    }

    public void Draw()
    {
        Raylib.DrawRectangle((int)this.position.X, (int)this.position.Y, width, height, color);
    }

    
}
