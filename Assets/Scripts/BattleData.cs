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
    public List<Creature> CharacterList;
    public Creature player;
    public SelectorData selector;

    public Tilemap Ground;
    public Dictionary<Vector3, TileData> tiles;

    public List<TileData> tile_data;

    //flag used to determine if buttons are ready to be used.
    private bool player_active = false;

    //List of possible moves the player can make
    //TODO:Find permenant home for this and organise.
    private List<(TileData, int)> player_spaces;
    private int space_index;

    //private Circular_linkedList turn_order?
    //public TileDataArray[] BattleMap;
    private int Turn;

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

        GetTiles();
    }

    private void GetTiles()
    {
        tiles = new Dictionary<Vector3, TileData>();
        tile_data = new List<TileData>();
        foreach(Vector3Int pos in Ground.cellBounds.allPositionsWithin)
        {
            var localPlace = new Vector3Int(pos.x, pos.y, pos.z);

            if(!Ground.HasTile(localPlace)) continue;
            var tile = new TileData
            {
                LocalPlace = localPlace,
                WorldLocation = Ground.CellToWorld(localPlace),
                TilemapMember = Ground,
                Blocked = false,
                MoveCost = 1,
                AccuracyCost = 1,
                Selectable = false
            };

            tiles.Add(tile.WorldLocation, tile);
            tile_data.Add(tile);
        }
    }


    void Start()
    {
        player_active = false;
        Turn = 0;

        //Current used for testing the movement action
        //This will eventually be moved to a sperate function
        var test = player.breadth_first_search();

        Debug.Log("Total Number of Spaces: " + test.Count);
        foreach ((TileData, int) i_space in test)
        {
            //Debug.Log(i_space.Item1.LocalPlace.x + ", " + i_space.Item1.LocalPlace.y);
            i_space.Item1.Selectable = false;
        }
        player_spaces = test;
        space_index = 0;



        
        selector.gameObject.SetActive(true);
        //var selector_data = selector.getComponent<>
        selector.move(0, 0);
        player_active = true;
    }

    public Creature getCurrentCreature()
    {
        return CharacterList[Turn];
    }

    //private void sortCharacters(){}
    
    public void nextTurn()
    {
        Turn++;
        if(Turn >= CharacterList.Count)
        {
            Turn = 0;
        }
    }

    private void move()
    {
        Creature unit = CharacterList[Turn];
        //How to get tile of where character is standing???
    }

    public void OnUp()
    {
        if(player_active)
        {
            player_active = false;
            selector.move(0, 1);
            player_active = true;
        }
    }

    public void OnDown()
    {
        if(player_active)
        {
            player_active = false;
            selector.move(0, -1);
            player_active = true;
        }
    }

    public void OnRight()
    {
        if(player_active)
        {
            player_active = false;
            selector.move(1, 0);
            player_active = true;
        }
    }

    public void OnLeft()
    {
        if(player_active)
        {
            player_active = false;
            selector.move(-1, 0);
            player_active = true;
        }
    }

    public void OnAccept()
    {
        if(player_active)
        {
            player_active = false;

            player.transform.position = new Vector3(selector.X + 0.5f, selector.Y + 0.05f, 0);
            selector.gameObject.SetActive(false);
        }
    }

}