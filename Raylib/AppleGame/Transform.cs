using System;
using System.Numerics;
using Raylib_CsLo;

namespace GameEngine6000;

class Transform
{
    public Vector2 position;
    public float velocity;

    public Transform(Vector2 position, float velocity)
    {
        this.position = position;
        this.velocity = velocity;
    }

    public Transform()
    {
        this.position = Vector2.Zero;
        this.velocity = 0.0f;
    }

    public void Update()
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

        // position.X = Math.Clamp(position.X, 0, screen_width - width);
        // position.Y = Math.Clamp(position.Y, 0, screen_height - height);
    }
}
