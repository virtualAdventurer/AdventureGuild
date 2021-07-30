using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Camera))]
public class CameraBehavior : MonoBehaviour
{
    public float speed;
    private bool controlsActive;
    private Tilemap map;
    private Camera cam;
    private float x_move;
    private float y_move;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        //var warpedPosition = new Vector2(Screen.width / 2, Screen.height / 2);
        //Appreantly this may not be recomended
        //Maybe find a better solutiong instead of using low level?
        //Mouse.current.WarpCursorPosition(warpedPosition);
        //InputState.Change(Mouse.current.position, warpedPosition);
        x_move = 0;
        y_move = 0;
        controlsActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(controlsActive)
        {
            Vector3 lowerCorner = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
            Vector3 upperCorner = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));

            float bottom = lowerCorner.y;
            float top = upperCorner.y;
            float left = lowerCorner.x;
            float right = upperCorner.x;

            float height = cam.orthographicSize;
            float width = cam.aspect * height;
            
            //TODO: write a comment explainning these calculations
            if(map.size.x > width * 2
                && (left > 0 || x_move > 0)
                && (right < map.size.x || x_move < 0))
            {
                transform.Translate(Vector3.right * x_move * speed);
            }

            if(map.size.y > height * 2
                && (bottom > 0 || y_move > 0)
                && (top < map.size.y || y_move < 0))
            {
                transform.Translate(Vector3.up * y_move * speed);
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

    public void OnCamVertical(InputValue value)
    {
        y_move = value.Get<float>();
    }

    public void OnCamHorizontal(InputValue value)
    {
        x_move = value.Get<float>();
    }

    public void OnCamHorizontalNegative(InputValue value)
    {
        x_move = -value.Get<float>();
    }

    public void OnCamVerticalNegative(InputValue value)
    {
        y_move = -value.Get<float>();
    }
}
