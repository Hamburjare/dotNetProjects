using System;
using System.Numerics;
using Raylib_CsLo;
using GameEngine6000;

namespace Avaruuspeli;

class Player
{
    public GameEngine6000.Transform transform;
    public SpriteRenderer sprite;

    bool canMove = true;

    Texture texture;

    public Player(Vector2 position, float velocity)
    {
        transform = new GameEngine6000.Transform(position, velocity);
        sprite = new SpriteRenderer(position, Raylib.WHITE, new Vector2(50, 50), "./resources/textures/widepasi.png");
    }
    

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }

    public void Update()
    {
        // Read input
        KeyPressed();

        // Draw player
        sprite.position = transform.position;
        sprite.Draw();

        // Prevert player from going off screen
        transform.position.X = Math.Clamp(transform.position.X, 0, Raylib.GetScreenWidth() - sprite.size.X);
    }

    public void KeyPressed()
    {
        if (!canMove)
        {
            return;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT) || Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            transform.position.X += transform.velocity;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) || Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            transform.position.X -= transform.velocity;
        }
    }
}
