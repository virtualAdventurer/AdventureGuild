using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selecter : MonoBehaviour
{
    //coordinates to select from the grid
    public int x;
    public int y;
    public TileStats[,] map {set; get;}
    public delegate void cursorDelegate(int m_x, int m_y);
    private cursorDelegate cursorAction;
    static public Selecter createSelector(TileStats[,] m_map, cursorDelegate act)
    {
        var g_ob = Resources.Load<GameObject>("Buttons/Pointer");
        GameObject n_ob = Instantiate(g_ob);

        Selecter temp = n_ob.GetComponent<Selecter>();
        temp.map = m_map;
        temp.transform.position = temp.map[temp.x, temp.y].Location;
        temp.cursorAction = act;


        return temp;
    }

    public void OnSelectorLeft()
    {
        AdjustPointer(-1, 0);
    }

    public void OnSelectorRight()
    {
        AdjustPointer(1, 0);
    }

    public void OnSelectorUp()
    {
        AdjustPointer(0, 1);
    }

    public void OnSelectorDown()
    {
        AdjustPointer(0, -1);
    }

    public void OnSelect()
    {
        cursorAction(x, y);
    }

    private void AdjustPointer(int m_x, int m_y)
    {
        if(x + m_x >= 0 && x + m_x < map.GetLength(0))
            x += m_x;
        if(y + m_y >= 0 && y + m_y < map.GetLength(1))
            y += m_y;
        transform.position = map[x,y].Location;
    }



}
