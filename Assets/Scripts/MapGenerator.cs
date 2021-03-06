using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using System;

public class MapGenerator : MonoBehaviour
{
    private string mapName;
    private int mapWidth;
    private int mapHeight;
    public TileStats[,] map;
    public Tilemap Ground;
    public TileBase grass;
    public Animator UIController;
    public CameraBehavior cam;
    public Canvas canvas;
    private Selecter selecter;
    private Dropdown tile_options;

    public void Start()
    {
        mapName = "Untitled";
        mapWidth = 0;
        mapHeight = 0;
        map = null;
        selecter = null;
        tile_options = null;
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
            List<TileTemplate> tiles = (List<TileTemplate>)serializer.Deserialize(stream);
            
            if(tiles.Count >= 1)
            {
                map = TileStats.GenerateBaseMap(mapWidth, mapHeight, Ground, tiles[0]);
                UIController.SetBool("Map Generated", true);
                cam.Center(Ground);
                
                //Maybe find somwhere else to handle the transition
                selecter = Selecter.createSelector(map, selectSpace);
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

    public void selectSpace(int x, int y)
    {
        //Create the Tile Display Object
        GameObject template = Resources.Load<GameObject>("Buttons/TileDisplay");
        GameObject tile_display = Instantiate(template, canvas.transform);

        //Get the tile we are selecting
        TileStats tile = map[x, y];

        //Remove Selector
        Destroy(selecter.gameObject);
        selecter = null;

        //add image to tile display
        Image display = tile_display.transform.Find("Sprite Display").GetComponent<Image>();        
        display.sprite = tile.GetSprite();

        //Add Options for Tiles
        tile_options = tile_display.transform.Find("Tile Dropdown").GetComponent<Dropdown>();

        //Add functionality to button
        Button submit = tile_display.transform.Find("Select Tile").GetComponent<Button>();
        submit.onClick.AddListener(delegate { ActivateTile(); });
        
        //Find a different place for this, seems like we should have this loaded already
        FileStream stream = new FileStream("Assets/Resources/TileData/Test.XML", FileMode.Open);
        XmlSerializer serializer = new XmlSerializer(typeof(List<TileTemplate>));
        List<TileTemplate> tile_temps = (List<TileTemplate>)serializer.Deserialize(stream);

        foreach(TileTemplate tile_temp in tile_temps)
        {
            Dropdown.OptionData option = new Dropdown.OptionData(tile_temp.id);
            tile_options.options.Add(option);
        }
    }

    public void ActivateTile()
    {
        if(tile_options == null)
        {
            Debug.Log(0);
        } 
        else
        {
            Debug.Log(tile_options.value);
        }
    }

}
