using System;
using System.Numerics;
using Raylib_CsLo;
using GameEngine6000;

namespace Avaruuspeli;

class Bullet
{
    bool isActive = false;
    public GameEngine6000.Transform transform;
    public SpriteRenderer sprite;

    public Bullet()
    {
        transform = new GameEngine6000.Transform(new Vector2(0, 0), 10.0f);
        sprite = new SpriteRenderer(new Vector2(0, 0), Raylib.WHITE, new Vector2(10, 10));
    }

    public void Update()
    {

        // If bullet is not active, return
        if (!isActive)
        {
            return;
        }

        // If bullet goes off screen, set it to inactive
        if (transform.position.Y < 0 || transform.position.Y > Raylib.GetScreenHeight())
        {
            isActive = false;
        }

        // Draw bullet
        sprite.position = transform.position;
        sprite.Draw();
    }

    public void SetActivityFalse()
    {
        isActive = false;
    }

    public bool IsActive()
    {
        return isActive;
    }

    public void SetActive(Vector2 start_position, float start_velocity)
    {
        isActive = true;
        transform.position = start_position;
        transform.velocity = start_velocity;
    }
}
