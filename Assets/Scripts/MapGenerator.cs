using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapGenerator : MonoBehaviour
{
    
    public void Generate()
    {
        var fields = GetComponents<TextField>();
        foreach(var field in fields)
        {
            Debug.Log(field.name + " has " + field.text);
        }
        //string name = nameField.text;
        //int height = heightField;
        //int width = widthField;


        //Debug.Log("Name is " + name);
        //Debug.Log("Height is " + height);
        //Debug.Log("Width is " + width);
    }
}
