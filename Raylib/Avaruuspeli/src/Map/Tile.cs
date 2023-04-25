using System.Numerics;
using Raylib_CsLo;
namespace GameEngine6000;

public class Tile
{
    Vector2 position;
    int imageIndex;

    SpriteRenderer sprite;

    // csv file has a unknow number of rows and columns,
    // so we need to read it line by line
    // each index in the line is a tile index in tileset
    // width and height of the map is the number of rows and columns

    public Tile(int imageIndex, Vector2 position, Texture texture, Vector2 size)
    {
        this.imageIndex = imageIndex;
        this.position = position;
        // imageIndex is 4 numbers long, so we need to add 0's to the front
        string path = "./Tiled/tiles/tile_" + imageIndex.ToString("0000") + ".png";
        sprite = new SpriteRenderer(
            new Vector2(0, 0),
            size,
            Raylib.WHITE,
            texture
        );
    }

    public int ImageIndex
    {
        get { return imageIndex; }
    }

    public Vector2 Position
    {
        get { return position; }
        set { position = value; }
    }

    /// <summary>
    /// This function draws the tile to the screen.
    /// </summary>
    public void Draw()
    {
        sprite.position = position;
        sprite.Draw();
    }
}
