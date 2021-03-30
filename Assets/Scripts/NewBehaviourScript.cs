using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NewBehaviourScript : MonoBehaviour
{
    private TileData _tile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var worldPoint = new Vector3Int(Mathf.FloorToInt(point.x), Mathf.FloorToInt(point.y), 0);

            var tiles = BattleData.instance.tiles;

            if(tiles.TryGetValue(worldPoint, out _tile))
            {
                if(_tile.Blocked)
                {
                    _tile.TilemapMember.SetTileFlags(_tile.LocalPlace, TileFlags.None);
                    _tile.TilemapMember.SetColor(_tile.LocalPlace, Color.red);
                }
                else
                {
                    _tile.TilemapMember.SetTileFlags(_tile.LocalPlace, TileFlags.None);
                    _tile.TilemapMember.SetColor(_tile.LocalPlace, Color.green);
                    _tile.Blocked = true;
                }
                
            }

        }
    }
}
