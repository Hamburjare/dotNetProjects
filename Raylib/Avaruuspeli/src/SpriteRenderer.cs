using System;
using System.Numerics;
using Raylib_CsLo;

namespace GameEngine6000;

/// <summary>
/// Class <c>SpriteRenderer</c> is used to draw a rectangle or a texture.
/// </summary>
class SpriteRenderer
{
    /* A variable that is used to store the position of the sprite. */
    public Vector2 position;

    /* A variable that is used to store the color of the sprite. */
    Color color;

    /* A variable that is used to store the size of the sprite. */
    public Vector2 size;

    /* A variable that is used to store the path of the texture. */
    string texturePath;

    /* A variable that is used to store the texture. */
    Texture texture;

    /* Used to store the rectangle that is used to draw the texture. */
    Rectangle sourceRec;
 
    /// <summary>
    /// The constructor for the SpriteRenderer class.
    /// </summary>
    /// <param name="position">The position of the sprite.</param>
    /// <param name="size">The size of the sprite.</param>
    /// <param name="color">The color of the sprite.</param>
    /// <param name="texturePath">The path of the texture.</param>
    public SpriteRenderer(Vector2 position, Vector2 size, Color color, string texturePath)
    {
        this.position = position;
        this.color = color;
        this.size = size;
        this.texturePath = texturePath;

        /* Loading the texture. */
        LoadTexture();

        /* Scaling the image to fit the rectangle. */
        ScaleImage();

        /* Setting the sourceRec to the rectangle that is used to draw the texture. */
        sourceRec = GetRectangle();
    }

    /// <summary>
    /// The constructor for the SpriteRenderer class.
    /// </summary>
    /// <param name="position">The position of the sprite.</param>
    /// <param name="size">The size of the sprite.</param>
    /// <param name="color">The color of the sprite.</param>
    /// <param name="texture">The texture of the sprite</param>
    public SpriteRenderer(Vector2 position, Vector2 size, Color color, Texture texture)
    {
        this.position = position;
        this.color = color;
        this.size = size;
        this.texture = texture;
        texturePath = "abaduu";

        /* Scaling the image to fit the rectangle. */
        ScaleImage();

        /* Setting the sourceRec to the rectangle that is used to draw the texture. */
        sourceRec = GetRectangle();
    }

    /// <summary>
    /// The constructor for the SpriteRenderer class.
    /// </summary>
    /// <param name="position">The position of the sprite.</param>
    /// <param name="size">The size of the sprite.</param>
    /// <param name="color">The color of the sprite.</param>
    public SpriteRenderer(Vector2 position, Vector2 size, Color color)
    {
        this.position = position;
        this.color = color;
        this.size = size;
    }

    /// <summary>
    /// It scales the image.
    /// </summary>
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

    /// <summary>
    /// Loads the texture.
    /// </summary>
    void LoadTexture()
    {
        if (texturePath != null)
        {
            texture = Raylib.LoadTexture(texturePath);
        }
    }

    /// <summary>
    /// It returns a Rectangle object.
    /// </summary>
    public Rectangle GetRectangle()
    {
        return new Rectangle(position.X, position.Y, size.X, size.Y);
    }

    /// <summary>
    /// Draws the sprite.
    /// </summary>
    /// <remarks>
    /// If the texturePath is not null, it draws the texture.
    /// If the texturePath is null, it draws a rectangle.
    /// </remarks>
    public void Draw()
    {
        if (texturePath != null)
        {
            // Raylib.DrawTextureRec(texture, sourceRec, position, color);

            Raylib.DrawTexturePro(texture, sourceRec, GetRectangle(), new Vector2(0, 0), 0, color);
        }
        else
        {
            Raylib.DrawRectangleRec(GetRectangle(), color);
        }
    }
}
