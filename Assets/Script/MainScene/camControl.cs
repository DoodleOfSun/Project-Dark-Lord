using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Need To Use Clamp
public class camControl : MonoBehaviour
{
    public float scrollSpeed;
    public float camSpeed;
    private Camera cam;

    // Y Position
    public GameObject mapLimitUp; // Max
    public GameObject mapLimitDown; // Min

    // X Position
    public GameObject mapLimitLeft; // Min
    public GameObject mapLimitRight; // Max

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        cameraMovingByMouse();
        camerZoomingByMouseScroll();
    }

    private void cameraMovingByMouse()
    {
        if (Input.mousePosition.x <= 20 || Input.mousePosition.y <= 20 ||
            Input.mousePosition.x >= 1900 || Input.mousePosition.y >= 1060)
        {
            Vector3 temp = new Vector3();
            temp.x = Mathf.Clamp(cam.ScreenToWorldPoint(Input.mousePosition).x ,mapLimitLeft.transform.position.x, mapLimitRight.transform.position.x);
            temp.y = Mathf.Clamp(cam.ScreenToWorldPoint(Input.mousePosition).y, mapLimitDown.transform.position.y, mapLimitUp.transform.position.y);
            temp.z = -10;
            this.transform.position = Vector3.MoveTowards(this.transform.position, temp, camSpeed);
        }
    }

    private void camerZoomingByMouseScroll()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;

        if (cam.orthographicSize <= 4f && scroll > 0)
        {
            float temp = cam.orthographicSize;
            cam.orthographicSize = temp;
        }
        else if (cam.orthographicSize >= 8f && scroll < 0)
        {
            float temp = cam.orthographicSize;
            cam.orthographicSize = temp;
        }
        else
        {
            cam.orthographicSize -= scroll * 0.5f;

            if (cam.orthographicSize == 4f)
            {

            }
            else if (cam.orthographicSize == 5f)
            {

            }
            else if (cam.orthographicSize == 6f)
            {

            }
            else if (cam.orthographicSize == 7f)
            {

            }
            else if (cam.orthographicSize == 8f)
            {

            }
        }
    }
}
