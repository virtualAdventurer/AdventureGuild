using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileTemplate 
{
    public string sprite_name {get; set;}
    public string id {get; set;}

    public TileTemplate()
    {
        id = "untitled";
        sprite_name = null;
    }

    public TileTemplate(string m_id, string m_sprite)
    {
        id = m_id;
        sprite_name = m_sprite;
    }

    public Sprite GetSprite()
    {
        return Resources.Load<Sprite>("TileSprites/" + sprite_name);
    }

}
