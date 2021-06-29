using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TileStats
{
    //Possible needed for adding script into Tiles
    public Vector3Int Location {get; set;}
    public Tilemap TilemapMember {get; set;} 
    public int m_x {get; set;}
    public int m_y {get; set;}

    //Original data for each tile
    public Creature unit {get; set;}

    //Data for breadth first search
    public bool Selectable {get; set;}

    public TileStats(Tilemap tilemap, Vector3Int pos, TileBase sprite, int x, int y)
    {
        TilemapMember = tilemap;
        TilemapMember.SetTile(pos, sprite);
        Location = pos;
        m_x = x;
        m_y = y;

        unit = null;
        Selectable = false;
    }

}
