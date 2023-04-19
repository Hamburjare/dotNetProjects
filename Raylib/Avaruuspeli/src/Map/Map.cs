using System.IO;
using System.Linq;
using System.Globalization;
using System.Numerics;
using Raylib_CsLo;

namespace GameEngine6000;

public class Map
{
    List<Tile> tiles = new List<Tile>();

    public List<Tile> Tiles
    {
        get { return tiles; }
    }

    Vector2 tileSize;

    float y = 0;

    public float Y
    {
        get { return y; }
    }

    // csv file has a unknow number of rows and columns,
    // so we need to read it line by line
    // each index in the line is a tile index in tileset
    // width and height of the map is the number of rows and columns

    public Map(string mapFile, Vector2 size, List<Texture> tileset)
    {
        tileSize = size;
        StartReading(mapFile, tileset);
    }

    async Task StartReading(string mapFile, List<Texture> tileset)
    {
        await Task.Run(() => ReadCSVFile(mapFile, tileset));
    }

    string[][] m_Data;

    public async void ReadCSVFile(string mapFile, List<Texture> tileset)
    {
        // csv file has a unknow number of rows and columns,
        // so we need to read it line by line
        // each index in the line is a tile index in tileset
        // width and height of the map is the number of rows and columns

        // read the file
        string[] lines = await File.ReadAllLinesAsync(mapFile);

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
                tiles.Add(tile);
            }
        }
    }

    public void Draw()
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tiles[i].Draw();
        }

        Tile lastTile = tiles.Last();
        y = lastTile.Position.Y;
    }
}
