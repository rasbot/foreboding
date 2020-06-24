using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScale : MonoBehaviour
{

    //public float scale_min;         // set the minimum scale used during the scale change
    //public float scale_max;         // set the maximum scale used during the scale change
    public static float scale_f;    // final, or current scale of player

    private float playerPos;        // current player position
    private float playerPos_i;      // previous player position
    private Vector3 scale_i;        // initial scale of player
    private float dz;               // length of the room
    private float ds;               // player change in scale

    // Use this for initialization
    void Start()
    {
        //dz = 6.61f;
        dz = 7.85f;
        scale_i = transform.localScale;         // get scale of player
        ds = scale_i.z - 0.1f * scale_i.z;

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ScaleCol"))
        {
            playerPos_i = transform.position.z;     // grab the player position right when they enter the collision area
            //print("playerPos_i = " + playerPos_i);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ScaleCol"))
        {
            playerPos = transform.position.z - playerPos_i;                              // get the distance from the initial position
            //print("playerPos = " + playerPos);
            //scale_f = scale_min + ds / dz * playerPos;
            scale_f = scale_i.z + ds / dz * playerPos;
            transform.localScale = new Vector3(scale_f, scale_f, scale_f);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ScaleCol"))
        {
            transform.localScale = new Vector3(scale_i.x, scale_i.y, scale_i.z);    // set the scale of the player to whatever it was before entering the room
        }
    }
}
