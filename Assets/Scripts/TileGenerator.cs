using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class TileGenerator : MonoBehaviour
{
    //temp classes for now
    public InputField idTextBox;
    public Dropdown spriteDropdown;

    private Sprite[] spriteList;
    private List<TileTemplate> tiles;
    private Dictionary<int, Sprite> option_to_sprite;
    void Start()
    {
        spriteList = Resources.LoadAll<Sprite>("TileSprites");
        option_to_sprite = new Dictionary<int, Sprite>();

        //If the player selected new Tile file;
        tiles = new List<TileTemplate>();

        spriteDropdown.ClearOptions();
        //temp
        for(int i = 0; i < spriteList.Length; i++)
        {
            Dropdown.OptionData option = new Dropdown.OptionData(spriteList[i].name, spriteList[i]);
            spriteDropdown.options.Add(option);
            option_to_sprite.Add(i, spriteList[i]);
        }
    }

    public void AddTile()
    {
        TileTemplate tile = new TileTemplate(idTextBox.text, option_to_sprite[spriteDropdown.value]);
        tiles.Add(tile);
    }

    public void SaveTiles()
    {
        FileStream stream = new FileStream("Assets/Resources/TileData/Test.XML", FileMode.Create);
        XmlSerializer serializer = new XmlSerializer(typeof(List<TileTemplate>));
        serializer.Serialize(stream, tiles);
    }
}
