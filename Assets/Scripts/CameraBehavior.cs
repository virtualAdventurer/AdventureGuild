using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class CameraBehavior : MonoBehaviour
{
    public float speed;
    public bool controlsActive;
    // Start is called before the first frame update
    void Start()
    {
        var warpedPosition = new Vector2(Screen.width / 2, Screen.height / 2);
        //Appreantly this may not be recomended
        //Maybe find a better solutiong instead of using low level?
        Mouse.current.WarpCursorPosition(warpedPosition);
        InputState.Change(Mouse.current.position, warpedPosition);

        //transform.position = new Vector3(Screen.width / 2, 0, -10);
        var cam = GetComponent<Camera>();
        transform.position = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
    }

    // Update is called once per frame
    void Update()
    {
        if(controlsActive)
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

        }
    }
}
