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
    public SelectorData selector;

    public Tilemap Ground;
    //public Dictionary<Vector3, TileStats> tiles;

    public TileStats[,] map;
    public TileBase grass;
    public Canvas screen;

    //flag used to determine if buttons are ready to be used. 
    private bool player_active = false;

    public int mapWidth;
    public int mapHeight;
    public Sprite MoveSquare;
    public Sprite AttackSquare;
    public Font font;

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
            SpaceButton(space.Item1);
        }
    }

    public void AttackAction()
    {
        /*range = player.breadth_first_search(1);
        foreach(var item in range)
        {
            TileStats tile = item.Item1;
            GameObject square = new GameObject();
            var sprite = square.AddComponent<SpriteRenderer>();
            sprite.sprite = AttackSquare;
            square.transform.position = tile.Location;
            sprite.sortingOrder = 1;
            tile.indicator = square;
        }

        selector.gameObject.SetActive(true);
        selector.transform.position = player.currentSpace.Location;
        selector.X = player.currentSpace.x;
        selector.Y = player.currentSpace.y;
        preformAction = PrefomAttack;
        player_active = true; */
    }

    public void OnUp()
    {
        SpaceSelectHelper(0, 1);
    }

    public void OnDown()
    {
        SpaceSelectHelper(0, -1);
    }

    public void OnRight()
    {
        SpaceSelectHelper(1, 0);
    }

    public void OnLeft()
    {
        SpaceSelectHelper(-1, 0);
    }

    public void OnAccept()
    {
        if(player_active)
        {
            player_active = false;
            preformAction();
            if(map[selector.X, selector.Y].Selectable)
            {
                selector.gameObject.SetActive(false);
                foreach(var item in range)
                {
                    item.Item1.Selectable = false;
                    Destroy(item.Item1.indicator);
                    item.Item1.indicator = null;
                }
            }
            else
            {
                player_active = true;
            }
        }
    }

    private void SpaceSelectHelper(int x, int y)
    {
        if(player_active)
        {
            player_active = false;
            if(selector.X + x >= 0 && selector.X + x < mapWidth)
                selector.X += x;
            if(selector.Y + y >= 0 && selector.Y + y < mapHeight)
                selector.Y += y;
            selector.transform.position = map[selector.X, selector.Y].Location;
            player_active = true;
        }
    }

    private void PreformMove()
    {
        player.MoveCharacter(map[selector.X, selector.Y]);
    }

    private void PrefomAttack()
    {
        Debug.Log("Attack");
    }

    private void SpaceButton(TileStats tile)
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
        //Find out how to clearly indicate button being selected without mouse
        

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
    }

}