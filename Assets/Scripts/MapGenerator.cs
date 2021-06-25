using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;

public class MapGenerator : MonoBehaviour
{
    private string mapName;
    private int mapWidth;
    private int mapHeight;

    public void Start()
    {
        mapName = "Untitled";
        mapWidth = 0;
        mapHeight = 0;
    }

    public void Generate()
    { 
        Debug.Log("Name is " + mapName);
        Debug.Log("Height is " + mapHeight);
        Debug.Log("Width is " + mapWidth);
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
