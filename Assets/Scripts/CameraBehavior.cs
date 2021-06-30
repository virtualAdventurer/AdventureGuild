using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class CameraBehavior : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Screen.width);
        Debug.Log(Screen.height);
        var warpedPosition = new Vector2(Screen.width / 2, Screen.height / 2);
        //Appreantly this may not be recomended
        //Maybe find a better solutiong instead of using low level?
        Mouse.current.WarpCursorPosition(warpedPosition);
        InputState.Change(Mouse.current.position, warpedPosition);
    }

    // Update is called once per frame
    void Update()
    {
        var mousePos = Mouse.current.position.ReadValue();
        if(mousePos.x <= 0)
        {
            transform.Translate(Vector3.left * speed);
        }
        else if(mousePos.x >= Screen.width)
        {
            transform.Translate(Vector3.right * speed);
        }

        if(mousePos.y <= 0)
        {
            transform.Translate(Vector3.down * speed);
        }
        else if(mousePos.y >= Screen.height)
        {
            transform.Translate(Vector3.up * speed);
        }

        Debug.Log(mousePos);

    }
}
