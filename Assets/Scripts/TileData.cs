using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TileData
{
    //Possible needed for adding script into Tiles
    public Vector3Int LocalPlace {get; set;}
    public Vector3 WorldLocation {get; set;}
    //public TileBase TileBase {get; set;}
    public Tilemap TilemapMember {get; set;}

    //Original data for each tile
    public bool Blocked {get; set; }
    public int MoveCost {get; set; }
    public int AccuracyCost {get; set;}
    public Creature character {get; set;}

    //Data for breadth first search
    public bool Visited {get; set;}
    

}
