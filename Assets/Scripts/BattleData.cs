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
        //Temp map generation
        map = TileStats.GenerateBaseMap(mapWidth, mapHeight, Ground, grass);

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
        //Actions that have to happen every time
        deleteButtons();
        var cancel = choiceButton("cancel");
        cancel.onClick.AddListener(cancelAction);

        var spaces = player.breadth_first_search(player.Move);
        foreach(var space in spaces)
        {
            var button = SpaceButton(space.Item1);
            button.onClick.AddListener(() => PreformMove(space.Item1.m_x, space.Item1.m_y));
            space.Item1.Selectable = false;
        }
    }

    public void AttackAction()
    {
        //Actions that have to happen every time
        deleteButtons();
        var cancel = choiceButton("cancel");
        cancel.onClick.AddListener(cancelAction);

        var spaces = player.breadth_first_search(1);
        foreach(var space in spaces)
        {
            if(space.Item1.unit != null)
            {
                var button = SpaceButton(space.Item1);
                button.onClick.AddListener(() => PreformAttack(player, space.Item1.unit));
            }
            space.Item1.Selectable = false;
        }
    }

    public void cancelAction()
    {
        deleteButtons();
        generateActions();
    }

    private void PreformMove(int x, int y)
    {
        deleteButtons();
        player.MoveCharacter(map[x, y]);
        endTurn();
    }

    private void PreformAttack(Creature attacker, Creature target)
    {
        deleteButtons();
        var hit = attacker.GetAccuracy();
        var dodge = target.GetDodge();
        var damage = attacker.GetAttack() - target.GetDefence();
        if(hit >= dodge)
        {
            if(damage > 0)
            {
                Debug.Log(attacker.characterName + " does " +  damage + " damage to " + target.characterName);
                target.AddDamage(damage);
            }
            else
            {
                Debug.Log(attacker.characterName + " does 0 damage to " + target.characterName);
            }
        }
        else
        {
            Debug.Log(target.characterName + " dodges an attack from " + attacker.characterName);
        }

        //If the this attack causes the target to die, remove him from the game
        //Find a better way to do this than just deleting the target, remove him from the list.
        if(target.IsDead())
        {
            Destroy(target.gameObject);

            //Really bad temp design, this is in place until I can get a list of characters, and create a better methond
            //than just a variable for each person.
            if(target.name == "Player")
                player = null;
            if(target.name == "Enemy")
                enemy = null;

        }


        endTurn();
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


    private void endTurn()
    {
        //Win Condition
        if(enemy == null)
        {
            Debug.Log("Player Wins");
        }
        //Lose Condition
        else if(player == null)
        {
            Debug.Log("Player Loses");
        }
        //Continue Condition
        else
        {
            generateActions();
        }
    }
}