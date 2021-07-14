using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileGenerator : MonoBehaviour
{
    public Dropdown spriteMenu;
    // Start is called before the first frame update
    void Start()
    {
        Sprite[] list = Resources.LoadAll<Sprite>("TileSprites");
        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        foreach(Sprite sprite in list)
        {
            options.Add(new Dropdown.OptionData(sprite.name, sprite));
        }
        spriteMenu.AddOptions(options);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
