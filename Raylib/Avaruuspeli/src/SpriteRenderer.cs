using System;
using System.Numerics;
using Raylib_CsLo;

namespace GameEngine6000;

class SpriteRenderer
{
    public Vector2 position;

    Color color;

    public Vector2 size;

    string texturePath;

    Texture texture;

    public SpriteRenderer(Vector2 position, Color color, Vector2 size, string texturePath)
    {
        this.position = position;
        this.color = color;
        this.size = size;
        this.texturePath = texturePath;
        LoadTexture();
    }

    public SpriteRenderer(Vector2 position, Color color, Vector2 size)
    {
        this.position = position;
        this.color = color;
        this.size = size;
    }

    void LoadTexture()
    {
        if (texturePath != null)
        {
            texture = Raylib.LoadTexture(texturePath);
        }
    }

    public Rectangle GetRectangle()
    {
        return new Rectangle(position.X, position.Y, size.X, size.Y);
    }

    public void Draw()
    {
        if (texturePath != null)
        {
            Raylib.DrawTextureRec(texture, GetRectangle(), position, color);
        }
        else
        {
            Raylib.DrawRectangleRec(GetRectangle(), color);
        }
    }
}
