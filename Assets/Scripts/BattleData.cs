using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
//using System.Xml.Serialization;
//using System.IO;

public class BattleData : MonoBehaviour
{
    //Bad Singleton Pattern?
    public static BattleData instance;
    public Creature player;
    public SelectorData selector;

    public Tilemap Ground;
    //public Dictionary<Vector3, TileStats> tiles;

    public TileStats[,] map;
    public TileBase grass;

    public List<TileStats> tile_data;

    //flag used to determine if buttons are ready to be used.
    private bool player_active = false;

    public int mapWidth;
    public int mapHeight;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        //GetTiles();
    }

    /*private void GetTiles()
    {
        tiles = new Dictionary<Vector3, TileData>();
        tile_data = new List<TileData>();
        foreach(Vector3Int pos in Ground.cellBounds.allPositionsWithin)
        {
            var localPlace = new Vector3Int(pos.x, pos.y, pos.z);

            if(!Ground.HasTile(localPlace)) continue;
            var tile = new TileData
            {
                Location = Ground.CellToWorld(localPlace),
                TilemapMember = Ground,
                Selectable = false
            };

            tiles.Add(tile.Location, tile);
            tile_data.Add(tile);
        }
    }*/




    void Start()
    {
        /*player_active = false;

        player.breadth_first_search();

        
        selector.gameObject.SetActive(true);
        selector.transform.position = tiles[selector.getPosition()].Location;
        player_active = true;*/

        map = new TileStats[mapWidth, mapHeight];

        for(int i = 0; i < mapWidth; i++)
        {
            for(int t = 0; t < mapHeight; t++)
            {
                map[i, t] = new TileStats();
                map[i, t].TilemapMember = Ground;
                map[i, t].TilemapMember.SetTile(new Vector3Int(i, t, 0), grass);
            }
        }

    }

    public void OnUp()
    {
        SpaceSelectHelper(0, 1);
    }

    public void OnDown()
    {
        SpaceSelectHelper(0, -1);
    }

    public void OnRight()
    {
        SpaceSelectHelper(1, 0);
    }

    public void OnLeft()
    {
        SpaceSelectHelper(-1, 0);
    }

    public void OnAccept()
    {
        if(player_active)
        {
            /*player_active = false;

            if(tiles[selector.getPosition()].Selectable)
            {
                player.transform.position = tiles[selector.getPosition()].Location;
                selector.gameObject.SetActive(false);
            }
            else
            {
                player_active = true;
            }*/
        }
    }

    private void SpaceSelectHelper(int x, int y)
    {
        if(player_active)
        {
            /*player_active = false;
            TileStats tile;
            Vector3 position = selector.getPosition();
            position.x += x;
            position.y += y;

            if(tiles.TryGetValue(position, out tile))
            {
                selector.X += x;
                selector.Y += y;
                selector.transform.position = tile.Location;
            }
            player_active = true;*/
        }
    }

}