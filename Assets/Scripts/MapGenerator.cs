using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
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
    public Animator UIController;
    public CameraBehavior cam;

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
            FileStream stream = new FileStream("Assets/Resources/TileData/Test.XML", FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(List<TileTemplate>));
            var tiles = serializer.Deserialize(stream);
            
            if(/*tiles.Count >= 1*/true)
            {
                map = TileStats.GenerateBaseMap(mapWidth, mapHeight, Ground, grass);
                UIController.SetBool("Map Generated", true);
                cam.Center(Ground);
            }
            else
            {
                Debug.Log("No Tile Data set found!");
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
