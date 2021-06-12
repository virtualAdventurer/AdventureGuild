using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Creature : MonoBehaviour
{
    public int Move;

    public int characterX;
    public int characterY;

    public TileStats currentSpace;
    //Battle Stats
    public int accuracy;
    public int dodge;
    public int attack;
    public int defence;
 
    void Start()
    {
        
    }

    public void PlayerBoot()
    {
        //place character into correct positon?
        currentSpace = BattleData.instance.map[characterX, characterY];
        transform.position = currentSpace.Location;        
    }

    public void MoveCharacter(TileStats space)
    {
        transform.position =  space.Location;
        currentSpace = space;
    }

    TileStats get_data(int x, int y)
    {
        TileStats tile = null;
        if(0 <= y && y < BattleData.instance.mapHeight && 0 <= x && x < BattleData.instance.mapWidth)
        {
            tile = BattleData.instance.map[x, y];
        }
        return tile;
    }

    TileStats getSpace()
    {
        //Vector3 point = transform.position;
        //var worldPoint = new Vector3Int(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y), 0);
        return currentSpace;
    }


    public List<(TileStats, int)> breadth_first_search(int range)
    {
        TileStats space = getSpace();

        Queue<(TileStats, int)> need_to_visit = new Queue<(TileStats, int)>();
        List<(TileStats, int)> can_reach = new List<(TileStats, int)>();
        need_to_visit.Enqueue((space, 0));
        space.Selectable = true;

        while(need_to_visit.Count > 0)
        {
            (TileStats,  int) temp = need_to_visit.Dequeue();

            TileStats current_space = temp.Item1;
            int current_cost = temp.Item2;

            //var up = new Vector3(current_space.Location.x , current_space.Location.y + 1, 0);
            breadth_add_to_queue(need_to_visit, (current_space.x, current_space.y + 1), current_cost + 1, range);
        
            //var left = new Vector3(current_space.Location.x - 1, current_space.Location.y, 0);
            breadth_add_to_queue(need_to_visit, (current_space.x - 1, current_space.y), current_cost + 1, range);

            //var right = new Vector3(current_space.Location.x + 1, current_space.Location.y, 0);
            breadth_add_to_queue(need_to_visit, (current_space.x + 1, current_space.y), current_cost + 1, range);

            //var down = new Vector3(current_space.Location.x, current_space.Location.y - 1, 0);
            breadth_add_to_queue(need_to_visit, (current_space.x, current_space.y - 1), current_cost + 1, range);

            if(current_cost <= range)
            {
                can_reach.Add((current_space, current_cost));
                
                //current_space.TilemapMember.SetTileFlags(current_space.Location, TileFlags.None);
                //current_space.TilemapMember.SetColor(current_space.Location, Color.green);
            }
        }
        return can_reach;
    }

    private void breadth_add_to_queue(Queue<(TileStats, int)> q, (int, int) pos, int c, int r)
    {
        TileStats tile = get_data(pos.Item1, pos.Item2);
        if(tile != null && !tile.Selectable && c <= r)
        {
            q.Enqueue((tile, c));
            tile.Selectable = true;
        }
    }

    //Battle Stats
    public int GetAttack()
    {
        return attack;
    }
    public int GetDefence()
    {
        return defence;
    }
    public int GetAccuracy()
    {
        return accuracy;
    }
    public int GetDodge()
    {
        return dodge;
    }
}
