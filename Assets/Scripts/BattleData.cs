using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
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
    //public Canvas screen;

    //flag used to determine if buttons are ready to be used.
    private bool player_active = false;

    public int mapWidth;
    public int mapHeight;


    //Testing with UI
    //public Font testFont;
    //public Canvas canvas;

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
    }




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
                Vector3Int position = new Vector3Int(i, t, 0);
                map[i, t] = new TileStats();
                map[i, t].TilemapMember = Ground;
                map[i, t].TilemapMember.SetTile(position, grass);
                map[i, t].Location = position;
                map[i, t].x = i;
                map[i, t].y = t;
            }
        }

        player.PlayerBoot();
    }

    public void MoveAction()
    {
        var dummyList = player.breadth_first_search();
        foreach(var item in dummyList)
        {
            item.Item1.TilemapMember.SetTileFlags(item.Item1.Location, TileFlags.None);
            item.Item1.TilemapMember.SetColor(item.Item1.Location, Color.green);
        }

        selector.gameObject.SetActive(true);
        selector.transform.position = player.currentSpace.Location;
        selector.X = player.currentSpace.x;
        selector.Y = player.currentSpace.y;
        player_active = true;
    }

    public void AttackAction()
    {
        Debug.Log("attacl");
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
            player_active = false;

            if(map[selector.X, selector.Y].Selectable)
            {
                player.transform.position = map[selector.X, selector.Y].Location;
                selector.gameObject.SetActive(false);
            }
            else
            {
                player_active = true;
            }
        }
    }

    private void SpaceSelectHelper(int x, int y)
    {
        if(player_active)
        {
            player_active = false;
            selector.X += x;
            selector.Y += y;
            selector.transform.position = map[selector.X, selector.Y].Location;
            


            player_active = true;
        }
    }

}