using GameEngine6000;
using System;
using System.Numerics;
using Raylib_CsLo;

namespace Avaruuspeli;

class HealthPack : IBehaviour
{
    public bool isActive = false;
    public GameEngine6000.Transform transform;
    public SpriteRenderer sprite;

    public HealthPack(Vector2 size, Texture texture)
    {
        transform = new GameEngine6000.Transform(new Vector2(0, 0), 0);
        sprite = new SpriteRenderer(new Vector2(0, 0), size, Raylib.WHITE, texture);
    }

    public void Update()
    {
        if (!isActive)
        {
            return;
        }
        
        sprite.position = transform.position;
        sprite.Draw();
    }
}
