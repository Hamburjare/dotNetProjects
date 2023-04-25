using System.IO;
using System.Linq;
using System.Globalization;
using System.Numerics;
using Raylib_CsLo;

namespace GameEngine6000;

public class Map
{
    List<Tile> tiles = new();

    Vector2 tileSize;

    Vector2 mapSize;

    public Vector2 MapSize
    {
        get { return mapSize; }
    }

    public Map(string mapFile, Vector2 size, List<Texture> tileset)
    {
        StartReading(mapFile, size, tileset);
    }

    /// <summary>
    /// This function starts reading a map file and takes in the size and tileset as parameters.
    /// </summary>
    /// <param name="mapFile">The file path or name of the map file that contains the data for the game
    /// map.</param>
    /// <param name="Vector2">Vector2 is a data type in Unity that represents a 2D vector with x and y
    /// components. It is often used to represent positions, directions, and sizes in 2D space. In this
    /// context, the Vector2 size parameter is likely used to specify the size of the map being
    /// read</param>
    /// <param name="tileset">The `tileset` parameter is a list of textures that contains all the images
    /// of the tiles used in the map. These textures are used to render the map on the screen.</param>
    public void StartReading(string mapFile, Vector2 size, List<Texture> tileset)
    {
        tileSize = size;
        tiles.Clear();
        tiles = ReadCSVFile(mapFile, tileset);
        mapSize.Y = tiles.LastOrDefault()!.Position.Y + tileSize.Y;
        mapSize.X = tiles.LastOrDefault()!.Position.X + tileSize.X;
    }

    /// <summary>
    /// This function reads a CSV file and returns a list of Tile objects using a provided list of
    /// textures.
    /// </summary>
    /// <param name="mapFile">The file path or name of the CSV file that contains the map data.</param>
    /// <param name="tileset">The `tileset` parameter is a list of `Texture` objects that represent the
    /// images used for each tile in the map. These textures are used to render the map on the
    /// screen.</param>
    List<Tile> ReadCSVFile(string mapFile, List<Texture> tileset)
    {
        List<Tile> tilesList = new();

        string[][] m_Data;

        // csv file has a unknow number of rows and columns,
        // so we need to read it line by line
        // each index in the line is a tile index in tileset
        // width and height of the map is the number of rows and columns

        // read the file
        string[] lines = File.ReadAllLines(mapFile);

        // create a jagged array to store the data
        m_Data = new string[lines.Length][];
        for (int i = 0; i < lines.Length; i++)
        {
            // split the line into an array of strings
            m_Data[i] = lines[i].Split(',');
        }

        // create the tiles
        for (int y = 0; y < m_Data.Length; y++)
        {
            for (int x = 0; x < m_Data[y].Length; x++)
            {
                // create a tile
                Tile tile = new Tile(
                    int.Parse(m_Data[y][x]),
                    new Vector2(x * tileSize.X, y * tileSize.Y),
                    tileset[int.Parse(m_Data[y][x])],
                    tileSize
                );
                tilesList.Add(tile);
            }
        }
        return tilesList;
    }

    /// <summary>
    /// This function draws the map on the screen.
    /// </summary>
    public void Draw()
    {
        lock (tiles)
        {
            for (int i = 0; i < tiles.Count; i++)
            {
                tiles[i].Draw();
            }
        }
    }
}
