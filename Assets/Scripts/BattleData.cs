using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
//using System.Xml.Serialization;
//using System.IO;

public class BattleData : MonoBehaviour
{
    //Bad Singleton Pattern?
    public static BattleData instance;
    public Creature player;
    public Creature enemy;
    //public SelectorData selector;

    public Tilemap Ground;
    //public Dictionary<Vector3, TileStats> tiles;

    public TileStats[,] map;
    public TileBase grass;
    public Canvas screen;

    public int mapWidth;
    public int mapHeight;
    public Sprite MoveSquare;
    public Sprite AttackSquare;

    private List<(TileStats, int)> range;

    private delegate void action();
    private action preformAction;

    //Testing with UI
    //public Font testFont;
    //public Canvas canvas;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }




    void Start()
    {
        //Temp map generator
        map = new TileStats[mapWidth, mapHeight];

        for(int i = 0; i < mapWidth; i++)
        {
            for(int t = 0; t < mapHeight; t++)
            {
                Vector3Int position = new Vector3Int(i, t, 0);
                map[i, t] = new TileStats();
                map[i, t].TilemapMember = Ground;
                map[i, t].TilemapMember.SetTile(position, grass);
                map[i, t].Location = position;
                map[i, t].x = i;
                map[i, t].y = t;
            }
        }

        player.PlayerBoot();
        enemy.PlayerBoot();
    }

    public void MoveAction()
    {
        var spaces = player.breadth_first_search(player.Move);
        foreach(var space in spaces)
        {
            var button = SpaceButton(space.Item1);
            button.onClick.AddListener(() => PreformMove(space.Item1.x, space.Item1.y));
        }
    }

    public void AttackAction()
    {
        Debug.Log("Attack");
    }

    private void PreformMove(int x, int y)
    {
        player.MoveCharacter(map[x, y]);
    }

    private void PrefomAttack()
    {
        Debug.Log("Attack2");
    }

    private Button SpaceButton(TileStats tile)
    {
        // Create Button Object
        var buttonObject = new GameObject();
        buttonObject.transform.parent = screen.transform;
        buttonObject.name = "Button";

        //Adding the image to the button.
        var image = buttonObject.AddComponent<Image>();
        image.sprite = MoveSquare;

        //creating the button functionality
        var button = buttonObject.AddComponent<Button>();

        //Create position and size component
        var rectTransform = button.GetComponent<RectTransform>();
        rectTransform.pivot = new Vector2();

        //Calculate size
        var size =  screen.renderingDisplaySize.y / 20;
        rectTransform.sizeDelta = new Vector2(size, size);

        //calculate position
        var postionOnScreen = Camera.main.WorldToViewportPoint(tile.Location);
        var positionInPixels = postionOnScreen * screen.renderingDisplaySize;
        var Offset = screen.renderingDisplaySize / 2;
        rectTransform.localPosition = positionInPixels - Offset;

        return button;
    }

}