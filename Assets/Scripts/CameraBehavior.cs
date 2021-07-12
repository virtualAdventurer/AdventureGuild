using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Tilemaps;

public class CameraBehavior : MonoBehaviour
{
    public float speed;
    private bool controlsActive;
    private Tilemap map;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        var warpedPosition = new Vector2(Screen.width / 2, Screen.height / 2);
        //Appreantly this may not be recomended
        //Maybe find a better solutiong instead of using low level?
        Mouse.current.WarpCursorPosition(warpedPosition);
        InputState.Change(Mouse.current.position, warpedPosition);
        controlsActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(controlsActive)
        {
            var mousePos = Mouse.current.position.ReadValue();
            Vector3 lowerCorner = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
            Vector3 upperCorner = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));

            float bottom = lowerCorner.y;
            float top = upperCorner.y;
            float left = lowerCorner.x;
            float right = upperCorner.x;

            float height = cam.orthographicSize;
            float width = cam.aspect * height;
            
            if(map.size.x > width * 2)
            {
                if(mousePos.x <= 0 && left > 0)
                {
                    transform.Translate(Vector3.left * speed);
                }
                else if(mousePos.x >= Screen.width && right <= map.size.x)
                {
                    transform.Translate(Vector3.right * speed);
                }
            }

            if(map.size.y > height * 2)
            {
                if(mousePos.y <= 0 && bottom > 0)
                {
                    transform.Translate(Vector3.down * speed);
                }
                else if(mousePos.y >= Screen.height && top < map.size.y)
                {
                    transform.Translate(Vector3.up * speed);
                }
            }
        }
    }

    public void Center(Tilemap m)
    {
        map = m;

        float height = cam.orthographicSize;
        float width = cam.aspect * height;

        Vector3 move = cam.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));

        if(map.size.x < width * 2)
        {
            move.x = map.size.x / 2f;
        }

        if(map.size.y < height * 2)
        {
            move.y = map.size.y / 2f;
        }

        transform.position = move;
        controlsActive = true;
    }
}
