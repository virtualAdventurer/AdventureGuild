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
        /*player_active = false;

        player.breadth_first_search();

        
        selector.gameObject.SetActive(true);
        selector.transform.position = tiles[selector.getPosition()].Location;
        player_active = true;*/

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
        // Text
        var myText = new GameObject();
        myText.transform.parent = screen.transform;
        myText.name = "Button";

        /*var text = myText.AddComponent<Text>();
        text.font = font;
        text.text = "wobble";
        text.fontSize = 100;*/
        var image = myText.AddComponent<Image>();
        image.sprite = MoveSquare;

        var button = myText.AddComponent<Button>();

        //Debug.Log(Camera.main.ViewportToScreenPoint(player.transform.position));
        //Debug.Log(Camera.main.WorldToViewportPoint(player.transform.position));
        //Debug.Log(Camera.main.WorldToScreenPoint(player.transform.position));
        
        
        //Debug.Log();
        //Debug.Log(Camera.main.);

        //Debug.Log(size);
        

        // Text position
        var rectTransform = button.GetComponent<RectTransform>();
        rectTransform.pivot = new Vector2();
        var size =  screen.renderingDisplaySize.y / 20;
        rectTransform.sizeDelta = new Vector2(size, size);

        //rectTransform.localPosition = new Vector3(player.currentSpace.x, player.currentSpace.y, 0);
        var step1 = Camera.main.WorldToViewportPoint(player.transform.position);
        var step2 = screen.renderingDisplaySize;
        var step3 = step1 * step2;
        var step4 = step2 / 2;
        var step5 = step3 - step4;
        rectTransform.localPosition = step5;
        
        /*var step2 = step1 - new Vector3(18, 0.5f, 0);
        Debug.Log(step2);
        var step3 = rectTransform.sizeDelta;
        Debug.Log(step3);
        var step4 = Vector3.Scale(step2, step3);
        rectTransform.localPosition = step4;
        Debug.Log(step4);*/

        
        
        /*range = player.breadth_first_search(player.Move);
        foreach(var item in range)
        {
            TileStats tile = item.Item1;
            GameObject square = new GameObject();
            var sprite = square.AddComponent<SpriteRenderer>();
            sprite.sprite = MoveSquare;
            square.transform.position = tile.Location;
            sprite.sortingOrder = 1;
            tile.indicator = square;
        }

        selector.gameObject.SetActive(true);
        selector.transform.position = player.currentSpace.Location;
        selector.X = player.currentSpace.x;
        selector.Y = player.currentSpace.y;
        preformAction = PreformMove;
        player_active = true;*/
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

}