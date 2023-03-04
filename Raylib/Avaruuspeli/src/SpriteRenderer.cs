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

    Rectangle sourceRec;

    public SpriteRenderer(Vector2 position, Vector2 size, Color color, string texturePath)
    {
        this.position = position;
        this.color = color;
        this.size = size;
        this.texturePath = texturePath;
        LoadTexture();
        ScaleImage();
        sourceRec = GetRectangle();
    }

    public SpriteRenderer(Vector2 position, Vector2 size, Color color)
    {
        this.position = position;
        this.color = color;
        this.size = size;
    }

    public void ScaleImage()
    {
        // stretch image to fit rectangle

        float imageRatio = (float)texture.width / (float)texture.height;
        float rectangleRatio = size.X / size.Y;

        if (imageRatio > rectangleRatio)
        {
            size.Y = size.X / imageRatio;
        }
        else
        {
            size.X = size.Y * imageRatio;
        }
              
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
            // Raylib.DrawTextureRec(texture, sourceRec, position, color);

            Raylib.DrawTexturePro(
                texture,
                sourceRec,
                GetRectangle(),
                new Vector2(0, 0),
                0,
                color
            );

        }
        else
        {
            Raylib.DrawRectangleRec(GetRectangle(), color);
        }
    }
}
