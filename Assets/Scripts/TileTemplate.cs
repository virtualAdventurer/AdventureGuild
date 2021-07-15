using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTemplate 
{
    public Sprite sprite {get; set;}
    public string id {get; set;}

    public TileTemplate()
    {
        id = "untitled";
        sprite = null;
    }

    public TileTemplate(string m_id, Sprite m_sprite)
    {
        id = m_id;
        sprite = m_sprite;
    }

}
