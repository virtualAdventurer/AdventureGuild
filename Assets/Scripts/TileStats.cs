using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TileStats
{
    //Possible needed for adding script into Tiles
    public Vector3Int Location {get; set;}
    public Tilemap TilemapMember {get; set;}
    public Tile tile_render {get; set;} 
    public int m_x {get; set;}
    public int m_y {get; set;}

    //Original data for each tile
    public Creature unit {get; set;}

    //Data for breadth first search
    public bool selectable {get; set;}

    /*public TileStats(Tilemap tilemap, Vector3Int pos, TileBase sprite, int x, int y)
    {
        TilemapMember = tilemap;
        TilemapMember.SetTile(pos, sprite);
        Location = pos;
        m_x = x;
        m_y = y;

        unit = null;
        selectable = false;
    }*/

    public TileStats(Tilemap tilemap, Vector3Int pos, TileTemplate template, int x, int y)
    {
        TilemapMember = tilemap;
        Tile tile = Tile.CreateInstance<Tile>();
        tile.sprite = template.GetSprite();
        tile_render = tile;
        TilemapMember.SetTile(pos, tile);
        Location = pos;
        m_x = x;
        m_y = y;

        unit = null;
        selectable = false;
    }

    public Sprite GetSprite()
    {
        return tile_render.sprite;
    }

    /*static public TileStats[,] GenerateBaseMap(int width, int height, Tilemap ground, TileBase sprite)
    {
        var map = new TileStats[width, height];

            for(int i = 0; i < width; i++)
            {
                for(int t = 0; t < height; t++)
                {
                    Vector3Int position = new Vector3Int(i, t, 0);
                    map[i, t] = new TileStats(ground, position, sprite, i, t);
                }
            }
        return map;
    }*/

    static public TileStats[,] GenerateBaseMap(int width, int height, Tilemap ground, TileTemplate template)
    {
        var map = new TileStats[width, height];

            for(int i = 0; i < width; i++)
            {
                for(int t = 0; t < height; t++)
                {
                    Vector3Int position = new Vector3Int(i, t, 0);
                    map[i, t] = new TileStats(ground, position, template, i, t);
                }
            }
        return map;
    }

}
