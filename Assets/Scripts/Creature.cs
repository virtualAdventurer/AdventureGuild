using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Creature : MonoBehaviour
{
    public int Move;
 
    void Start()
    {
        
    }

    TileStats get_data(Vector3 p)
    {
        //var tiles = BattleData.instance.tiles;
        TileStats tile;
        /*if(!tiles.TryGetValue(p, out tile))
        {
            tile = null;
        }*/
        tile = null;
        return tile;
    }

    TileStats getSpace()
    {
        Vector3 point = transform.position;
        var worldPoint = new Vector3Int(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y), 0);
        return get_data(worldPoint);
    }


    public List<(TileStats, int)> breadth_first_search()
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

            var up = new Vector3(current_space.Location.x , current_space.Location.y + 1, 0);
            breadth_add_to_queue(need_to_visit, up, current_cost + 1);
        
            var left = new Vector3(current_space.Location.x - 1, current_space.Location.y, 0);
            breadth_add_to_queue(need_to_visit, left, current_cost + 1);

            var right = new Vector3(current_space.Location.x + 1, current_space.Location.y, 0);
            breadth_add_to_queue(need_to_visit, right, current_cost + 1);

            var down = new Vector3(current_space.Location.x, current_space.Location.y - 1, 0);
            breadth_add_to_queue(need_to_visit, down, current_cost + 1);

            if(current_cost <= Move)
            {
                can_reach.Add((current_space, current_cost));
                
                //current_space.TilemapMember.SetTileFlags(current_space.Location, TileFlags.None);
                //current_space.TilemapMember.SetColor(current_space.Location, Color.green);
            }
        }
        return can_reach;
    }

    void breadth_add_to_queue(Queue<(TileStats, int)> q, Vector3 p, int c)
    {
        TileStats tile = get_data(p);
        if(tile != null && !tile.Selectable && c <= Move)
        {
            q.Enqueue((tile, c));
            tile.Selectable = true;
        }
    }
}
