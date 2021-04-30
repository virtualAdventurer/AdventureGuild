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

    TileData get_data(Vector3 p)
    {
        var tiles = BattleData.instance.tiles;
        TileData tile;
        if(!tiles.TryGetValue(p, out tile))
        {
            tile = null;
        }
        return tile;
    }

    TileData getSpace()
    {
        Vector3 point = transform.position;
        var worldPoint = new Vector3Int(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y), 0);
        return get_data(worldPoint);
    }


    public List<(TileData, int)> breadth_first_search()
    {
        TileData space = getSpace();

        Queue<(TileData, int)> need_to_visit = new Queue<(TileData, int)>();
        List<(TileData, int)> can_reach = new List<(TileData, int)>();
        need_to_visit.Enqueue((space, 0));
        space.Selectable = true;

        while(need_to_visit.Count > 0)
        {
            (TileData,  int) temp = need_to_visit.Dequeue();

            TileData current_space = temp.Item1;
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

    void breadth_add_to_queue(Queue<(TileData, int)> q, Vector3 p, int c)
    {
        TileData tile = get_data(p);
        if(tile != null && !tile.Selectable && c <= Move)
        {
            q.Enqueue((tile, c));
            tile.Selectable = true;
        }
    }
}
