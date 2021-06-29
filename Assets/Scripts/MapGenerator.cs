using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Tilemaps;
using System;

public class MapGenerator : MonoBehaviour
{
    private string mapName;
    private int mapWidth;
    private int mapHeight;
    private TileStats[,] map;
    public Tilemap Ground;
    public TileBase grass;

    public void Start()
    {
        mapName = "Untitled";
        mapWidth = 0;
        mapHeight = 0;
        map = null;        
    }

    public void Generate()
    { 
        if(mapWidth <= 0 || mapHeight <= 0)
        {
            Debug.Log("Please enter values greator than zero for the height and width");
        }
        else
        {
            map = new TileStats[mapWidth, mapHeight];

            for(int i = 0; i < mapWidth; i++)
            {
                for(int t = 0; t < mapHeight; t++)
                {
                    Vector3Int position = new Vector3Int(i, t, 0);
                    map[i, t] = new TileStats(Ground, position, grass, i, t);
                }
            }
        }
    }

    public void editName(string text)
    {
        mapName = text;
    }

    public void editWidth(string text)
    {
        try
        {
            mapWidth = Int32.Parse(text);
        }
        catch (FormatException)
        {
            //Display this on the screen!
            Debug.Log("Please Enter a number!");
        }
    }

    public void editHeight(string text)
    {
        try
        {
            mapHeight = Int32.Parse(text);
        }
        catch (FormatException)
        {
            //Display this on the screen!
            Debug.Log("Please Enter a number!");
        }
    }
}
