using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorData : MonoBehaviour
{
    public int X;
    public int Y;

    public void move(int x, int y)
    {
        X += x;
        Y += y;
        transform.position = new Vector3(X + 0.5f, Y + 0.5f, 0);
    }
}
