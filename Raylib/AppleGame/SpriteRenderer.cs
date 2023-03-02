using System;
using System.Numerics;
using Raylib_CsLo;

namespace GameEngine6000;

class SpriteRenderer
{
    public Vector2 position;

    Color color;

    public Vector2 size;

    public SpriteRenderer(Vector2 position, Color color, Vector2 size)
    {
        this.position = position;
        this.color = color;
        this.size = size;
    }

    public Rectangle GetRectangle()
    {
        return new Rectangle(position.X, position.Y, size.X, size.Y);
    }
    
    public void Draw()
    {
        Raylib.DrawRectangleRec(GetRectangle(), color);
    }
    
}
