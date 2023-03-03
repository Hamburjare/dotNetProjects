using System;
using System.Numerics;
using Raylib_CsLo;
using GameEngine6000;

namespace Avaruuspeli;

class Enemy
{
    List<Bullet> bullets = new List<Bullet>();
    public bool isActive = false;

    bool canMove = true;

    public GameEngine6000.Transform transform;
    public SpriteRenderer sprite;
    

    public Enemy()
    {
        transform = new GameEngine6000.Transform(new Vector2(0, 0), 5.0f);
        sprite = new SpriteRenderer(new Vector2(0, 0), Raylib.WHITE, new Vector2(50, 50), "./resources/textures/hjallis.png");
    }

    public void Update()
    {
        // If enemy is not active, return
        if (!isActive)
        {
            return;
        }

        // Draw enemy
        sprite.position = transform.position;
        sprite.Draw();

        // Move enemy
        MoveEnemy();

    }

    public void SetActivityFalse()
    {
        isActive = false;
    }

    public bool IsActive()
    {
        return isActive;
    }

    public void SetCanMove(bool canMove)
    {
        this.canMove = canMove;
    }

    public void SetActive(Vector2 start_position, float start_velocity)
    {
        isActive = true;
        transform.position = start_position;
        transform.velocity = start_velocity;
    }

    void MoveEnemy()
    {

        if(!canMove)
        {
            return;
        }

        // Move enemy randomly left or right
        if (Raylib.GetRandomValue(0, 100) < 50)
        {
            transform.position.X += transform.velocity;
        }
        else
        {
            transform.position.X -= transform.velocity;
        }

        transform.position.X = Math.Clamp(transform.position.X, 0, Raylib.GetScreenWidth() - sprite.size.X);
    }
}
