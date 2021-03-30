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

    public Tilemap Ground;
    public Dictionary<Vector3, TileData> tiles;

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
                Visited = false
            };

            tiles.Add(tile.WorldLocation, tile);
        }
    }


    void Start()
    {
        //Get Characters List and Battle Map
        //Create Characters and map.

        //Generate Turn_order
        //sortCharacters();
        /*XmlSerializer serailizer = new XmlSerializer(typeof(List<TileData>));
        FileStream stream = new FileStream(Application.dataPath + "/BattleMaps/TestBattle.xml", FileMode.Create);
        serailizer.Serialize(stream, tile_data);
        stream.Close();*/



        Turn = 0;
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


}