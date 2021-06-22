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
    public Tilemap Ground;
    public TileStats[,] map;
    public TileBase grass;
    public Canvas screen;
    public int mapWidth;
    public int mapHeight;
    public Sprite MoveSquare;
    public Sprite AttackSquare;
    public GameObject ActionButtonTemplate;

    private List<Action> actionList;

    private List<GameObject> buttons;

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
                map[i, t].unit = null;
                map[i, t].x = i;
                map[i, t].y = t;
            }
        }

        //Place Creatures on the map
        player.PlayerBoot();
        enemy.PlayerBoot();

        //Temp Action Generation
        actionList = new List<Action>();
        actionList.Add(new Action("Attack", AttackAction));
        actionList.Add(new Action("Move", MoveAction));

        buttons = new List<GameObject>();

        //Debug.Log(actionList);
        generateActions();
    }

    public void MoveAction()
    {
        deleteButtons();
        var spaces = player.breadth_first_search(player.Move);
        foreach(var space in spaces)
        {
            var button = SpaceButton(space.Item1);
            button.onClick.AddListener(() => PreformMove(space.Item1.x, space.Item1.y));
            space.Item1.Selectable = false;
        }
    }

    public void AttackAction()
    {
        deleteButtons();
        //Get attack spaces
        var spaces = player.breadth_first_search(1);
        //Generate Button for every enemy in range. (simplify to creature)
        foreach(var space in spaces)
        {
            //Only put a button on the space if the space has an enemy
            if(space.Item1.unit != null)
            {
                var button = SpaceButton(space.Item1);
                button.onClick.AddListener(() => PreformAttack(player, space.Item1.unit));
            }
            space.Item1.Selectable = false;
        }
        //Give each button a function to preform the attack, with its target
    }

    private void PreformMove(int x, int y)
    {
        deleteButtons();
        player.MoveCharacter(map[x, y]);
        generateActions();
    }

    private void PreformAttack(Creature attacker, Creature target)
    {
        deleteButtons();
        Debug.Log(attacker.name + " attacks " + target.name);
        generateActions();
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

        buttons.Add(buttonObject);

        return button;
    }

    private Button choiceButton(string f_text, int place = 0)
    {
        GameObject buttonObject = Instantiate(ActionButtonTemplate, screen.transform);
        var button = buttonObject.GetComponent<Button>();
        var textComponent = button.transform.GetChild(0).GetComponent<Text>();
        textComponent.text = f_text;
        var rectTransform = buttonObject.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector2(0, place * rectTransform.sizeDelta.y);

        buttons.Add(buttonObject);
        return button;
    }

    private void generateActions()
    {
        for (int i = 0; i < actionList.Count; i++)
        {
            var action = actionList[i];

            var button = choiceButton(action.getName(), i);
            button.onClick.AddListener(action.preformAction);
        }
    }

    private void deleteButtons()
    {
        for(int i = 0; i < buttons.Count; i++)
        {
            Destroy(buttons[i]);
        }
        buttons.Clear();
    }

}