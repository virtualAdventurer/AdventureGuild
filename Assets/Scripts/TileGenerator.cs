using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class TileGenerator : MonoBehaviour
{
    //UI buttons assigned for easy access
    public InputField idTextBox;
    public Dropdown spriteDropdown;
    public GameObject sv_Content;

    //Data to keep track of
    private Sprite[] spriteList;
    private List<TileTemplate> tiles;
    private Dictionary<int, Sprite> option_to_sprite;

    //TODO: Maybe add some variables to deal with the magic numbers present in the UI
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
        TileTemplate tile = new TileTemplate(idTextBox.text, option_to_sprite[spriteDropdown.value].name);
        tiles.Add(tile);

        GameObject TilePreviewTemp = Resources.Load<GameObject>("Buttons/Tile-Preview");
        GameObject TilePreview = Instantiate(TilePreviewTemp, sv_Content.transform);
        
        //Add sprite
        Image display = TilePreview.GetComponentInChildren<Image>();
        display.sprite = option_to_sprite[spriteDropdown.value];

        //Add text
        Text display_name = TilePreview.GetComponentInChildren<Text>();
        display_name.text = idTextBox.text;

        //Increase size of Tile Preview
        var content_transform = sv_Content.GetComponent<RectTransform>();
        content_transform.sizeDelta += new Vector2(0, 64 + 8);        

        //Set position inside the preview so it fits
        var preview_transform = TilePreview.GetComponent<RectTransform>();
        preview_transform.localPosition = new Vector3(8, -((tiles.Count - 1) * 64) - ((tiles.Count - 1) * 8) - 8, 0);        
    }

    public void SaveTiles()
    {
        FileStream stream = new FileStream("Assets/Resources/TileData/Test.XML", FileMode.Create);
        XmlSerializer serializer = new XmlSerializer(typeof(List<TileTemplate>));
        serializer.Serialize(stream, tiles);
    }
}
