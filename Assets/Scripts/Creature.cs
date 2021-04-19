using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Creature : MonoBehaviour
{
    public string CreatureName;
    public int Health;
    public int Move;

    private TileData space;
 
    void Start()
    {
        Vector3 point = transform.position;
        var worldPoint = new Vector3Int(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y), 0);
        space = get_data(worldPoint);
        var test = breadth_first_search(worldPoint);

        Debug.Log("Total Number of Spaces: " + test.Count);
        foreach ((TileData, int) i_space in test)
        {
            Debug.Log(i_space.Item1.LocalPlace.x + ", " + i_space.Item1.LocalPlace.y);
            i_space.Item1.Visited = false;
        }

        //var up = new Vector3Int(worldPoint.x, worldPoint.y + 1, 0);
        //var left = new Vector3Int(worldPoint.x - 1, worldPoint.y, 0);
        //var right = new Vector3Int(worldPoint.x + 1, worldPoint.y, 0);
        //var down = new Vector3Int(worldPoint.x, worldPoint.y - 1, 0);
        
    }

    /*void change_to_green(Vector3Int p)
    {
        var tiles = BattleData.instance.tiles;

        TileData _tile;
        if(tiles .TryGetValue(p, out _tile))
        {
            _tile.TilemapMember.SetTileFlags(_tile.LocalPlace, TileFlags.None);
            _tile.TilemapMember.SetColor(_tile.LocalPlace, Color.green);
        }
    }*/

    TileData get_data(Vector3Int p)
    {
        var tiles = BattleData.instance.tiles;
        TileData tile;
        if(!tiles.TryGetValue(p, out tile))
        {
            tile = null;
        }
        return tile;
    }


    List<(TileData, int)> breadth_first_search(Vector3Int point)
    {
        Queue<(TileData, int)> need_to_visit = new Queue<(TileData, int)>();
        List<(TileData, int)> can_reach = new List<(TileData, int)>();
        need_to_visit.Enqueue((space, 0));
        space.Visited = true;

        while(need_to_visit.Count > 0)
        {
            (TileData,  int) temp = need_to_visit.Dequeue();

            TileData current_space = temp.Item1;
            int current_cost = temp.Item2;

            var up = new Vector3Int(current_space.LocalPlace.x , current_space.LocalPlace.y + 1, 0);
            breadth_add_to_queue(need_to_visit, up, current_cost + 1);
        
            var left = new Vector3Int(current_space.LocalPlace.x - 1, current_space.LocalPlace.y, 0);
            breadth_add_to_queue(need_to_visit, left, current_cost + 1);

            var right = new Vector3Int(current_space.LocalPlace.x + 1, current_space.LocalPlace.y, 0);
            breadth_add_to_queue(need_to_visit, right, current_cost + 1);

            var down = new Vector3Int(current_space.LocalPlace.x, current_space.LocalPlace.y - 1, 0);
            breadth_add_to_queue(need_to_visit, down, current_cost + 1);

            if(current_cost <= Move)
            {
                can_reach.Add((current_space, current_cost));
                //Just for testing, add bittons here?
                current_space.TilemapMember.SetTileFlags(current_space.LocalPlace, TileFlags.None);
                current_space.TilemapMember.SetColor(current_space.LocalPlace, Color.green);
            }
        }
        return can_reach;
    }

    void breadth_add_to_queue(Queue<(TileData, int)> q, Vector3Int p, int c)
    {
        TileData tile = get_data(p);
        if(tile != null && !tile.Visited && c <= Move)
        {
            q.Enqueue((tile, c));
            tile.Visited = true;
        }
    }
}
