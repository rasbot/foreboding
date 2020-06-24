using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingController : MonoBehaviour
{

    public GameObject orbPrefab;
    public int numOrbs;
    static int _numOrbs;
    public static GameObject[] allOrbs;
    [Tooltip("Half the width/height/depth of the flocking area.")]
    public int orbContainerSize;
    public static int _orbContainerSize;
    public float speed;
    public float rotationSpeed;
    public static float _speed;
    public static float _rotationSpeed;
    [Tooltip("The distance between any nearest neighbor which is close enough to flock.")]
    public float neighborDistance;
    public float[] speedRange;
    [Tooltip("Any two orbs close than this distance will move to not collide.")]
    public float minDistance;
    public static float _minDistance;
    public static float _neighborDistance;
    public static float[] _speedRange;
    public static Vector3 goalPos;

    // Use this for initialization
    void Start()
    {
        _numOrbs = numOrbs;
        _orbContainerSize = orbContainerSize;
        _speed = speed;
        _rotationSpeed = rotationSpeed;
        _minDistance = minDistance;
        _neighborDistance = neighborDistance;
        _speedRange = speedRange;
        allOrbs = new GameObject[_numOrbs];
        goalPos = transform.localPosition;

        for (int i = 0; i < _numOrbs; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-_orbContainerSize + transform.position.x, _orbContainerSize + transform.position.x),    // allow the orbs to be instnatiated within +/- some amount of the container position
                                      Random.Range(-_orbContainerSize + transform.position.y, _orbContainerSize + transform.position.y),
                                      Random.Range(-_orbContainerSize + transform.position.z, _orbContainerSize + transform.position.z));
            allOrbs[i] = (GameObject)Instantiate(orbPrefab, pos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(0,10000) < 50)
        {
            goalPos = new Vector3(Random.Range(-_orbContainerSize + transform.position.x, _orbContainerSize + transform.position.x),        // move the goal position every so often
                                  Random.Range(-_orbContainerSize + transform.position.y, _orbContainerSize + transform.position.y),
                                  Random.Range(-_orbContainerSize + transform.position.z, _orbContainerSize + transform.position.z)); 
        }
    }
}
