using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform followTarget;
    private Camera camera;
    Vector3 newPosition;
    public float xOffset, yOffset, zOffSet = 1f;
    public float xbound, ybound, xSpeed, ySpeed;
    public bool turnCursorOn = false;

    /*
     * w = width
     * 
     * h = height
     * 
     * x = x position
     * 
     * y = y position
     */
    private float w, h, x, y;
    private void Awake()
    {
        camera = GetComponent<Camera>();
        newPosition = transform.position;
        //x position of camera
        x = transform.position.x;
        //y position of camera
        y = transform.position.y;
        Cursor.visible = turnCursorOn;
        transform.position = followTarget.position;
    }
    private void Update()
    {
        if (turnCursorOn)
        {
            Cursor.visible = turnCursorOn;
        }
        else
        {
            Cursor.visible = turnCursorOn;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //camera width
        w = transform.position.x;
        //camera height
        h = transform.position.y;


        if (followTarget.position.x > w + xbound)
        {
            x += xSpeed * Time.fixedDeltaTime;
        }
        if (followTarget.position.x < w - xbound)
        {
            x -= xSpeed * Time.fixedDeltaTime;
        }
        if (followTarget.position.y > h + ybound)
        {
            y += ySpeed * Time.fixedDeltaTime;
        }
        if (followTarget.position.y < h - ybound)
        {
            y -= ySpeed * Time.fixedDeltaTime;
        }

        //set camera position to new position

        newPosition = new Vector3(x + xOffset, y + yOffset, zOffSet);
        transform.position = newPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(xbound + w, ybound + h));
    }
}
