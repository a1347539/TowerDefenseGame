using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class CameraMovement : Singleton<CameraMovement>
{
    private float cameraSpeed = 2;

    private float xMax;
    private float yMin;
    private float tileWidth, tileHeight;
    public float TileWidth { set => tileWidth = value; }
    public float TileHeight { set => tileHeight = value; }

    private Vector3 dragOrigin;

    float scroll;

    float zoomSpeed = 1;

    private float zoomTarget;

    private float zoomMin = 2.5f;

    private float zoomMax = 5f;

    private Vector3 origin;

    private void Start()
    {
        zoomTarget = Camera.main.orthographicSize;

        origin = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
    }

    // Update is called once per frame
    private void Update()
    {
        zoom_In_Out();

        dragByMouse();
        
        setLimit();

        //testMousePosition();
    }
    private void GetInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * cameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * cameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * cameraSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime);
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, tileWidth, xMax),
                                         Mathf.Clamp(transform.position.y, yMin, tileHeight*-1),
                                         -1);
    }

    public void SetLimits(Vector3 maxTile)
    {
        Vector3 limit = Camera.main.ViewportToWorldPoint(new Vector3(1, 0));

        xMax = maxTile.x - limit.x;
        yMin = maxTile.y - limit.y;
    }

    private void dragByMouse()
    {
        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(1)) return;

        Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);

        transform.Translate(new Vector3(-pos.x * cameraSpeed, -pos.y * cameraSpeed, 0), Space.World);
    }

    private void zoom_In_Out()
    {
        scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {
            zoomTarget -= scroll * zoomSpeed;
            zoomTarget = Mathf.Clamp(zoomTarget, zoomMin, zoomMax);
        }

        Camera.main.orthographicSize = Mathf.MoveTowards(Camera.main.orthographicSize, zoomTarget, Time.deltaTime);
    }

    private void setLimit()
    {
        //print(Camera.main.orthographicSize);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, tileWidth - (widthDifference(Camera.main.orthographicSize) * tileWidth), xMax + (widthDifference(Camera.main.orthographicSize) * tileWidth)),
                             Mathf.Clamp(transform.position.y, yMin - (heightDifference(Camera.main.orthographicSize) * tileHeight), tileHeight * -1 + (heightDifference(Camera.main.orthographicSize) * tileHeight)),
                             -1);
    }

    private void testMousePosition()
    {
        Vector3 mouse = Input.mousePosition;
        Vector2 point = Camera.main.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y));
    }

    private float widthDifference(float cameraSize)
    {
        return (-1f * cameraSize + 5f) / 0.36f;
    }

    private float heightDifference(float cameraSize)
    {
        return (cameraSize - 5f) / -0.64f;
    }
}
