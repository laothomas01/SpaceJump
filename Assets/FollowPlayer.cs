using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    //follow the player
    public Transform followTarg;
    //new cam position
    Vector3 newPos;

    public float xoffSet = 1f;
    public float xbound;
    private float w, h, x, y;

    private void Awake()
    {
        newPos = transform.position;
        //camera x position
        x = transform.position.x;
        //camera y position
        y = transform.position.y;
        transform.position = followTarg.position;
    }
    private void Update()
    {


    }
    // Update is called once per frame
    void FixedUpdate()
    {
        w = transform.position.x;

        if (followTarg.position.x > w + xbound)
        {
            x += 11;
        }
        else if (followTarg.position.x < w - xbound - 1)
        {
            x -= 11;
        }
        //set cam position to new postiion
        newPos = new Vector2(x , y);
        transform.position = newPos;
    }


}
