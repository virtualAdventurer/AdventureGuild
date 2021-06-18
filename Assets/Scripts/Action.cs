using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    private string name;
    public delegate void actionTemplate();
    private actionTemplate act;

    public Action(string n, actionTemplate func)
    {
        name = n;
        act = func;
    }

    public void preformAction()
    {
        act();
    }

    public string getName()
    {
        return name;
    }
}
