using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileTemplate 
{
    public TileBase t_base {get; set;}
    public string id {get; set;}

    public TileTemplate()
    {
        id = "untitled";
        t_base = null;
    }

    public TileTemplate(string m_id, TileBase m_sprite)
    {
        id = m_id;
        t_base = m_sprite;
    }

}
