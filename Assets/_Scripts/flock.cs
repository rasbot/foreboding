using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flock : MonoBehaviour {

	private float speed;
    private float rotationSpeed;
    public GameObject flockingController;
	Vector3 averageHeading;
	Vector3 averagePosition;
    private float neighborDistance;
    private float[] speedRange;
    private float minDistance;
    private Animator orbAnimation;


    bool turning = false;

	void ApplyRules()
	{
		GameObject[] gos;
		gos = FlockingController.allOrbs;

        Vector3 vCenter = Vector3.zero;
        Vector3 vAvoid = Vector3.zero;
        //Vector3 vCenter = flockingController.transform.position;
        //Vector3 vAvoid = flockingController.transform.position;
        float gSpeed = 0.1f;

		Vector3 goalPos = FlockingController.goalPos;

		float dist;

		int groupSize = 0;
		foreach(GameObject go in gos)
		{
			if (go != this.gameObject)
			{
				dist = Vector3.Distance(go.transform.position, this.transform.position);
				if (dist <= neighborDistance)
				{
					vCenter += go.transform.position;
					groupSize++;

					if (dist < minDistance)
					{
						vAvoid = vAvoid + (this.transform.position - go.transform.position);
					}

					flock anotherFlock = go.GetComponent<flock>();
					gSpeed = gSpeed + anotherFlock.speed;
				}
			}

		}

		if (groupSize > 0)
		{
			vCenter = vCenter / groupSize + (goalPos - this.transform.position);
			speed = gSpeed / groupSize;

			Vector3 direction = (vCenter + vAvoid) - transform.position;
			if (direction != Vector3.zero)
			{
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
			}
		}
	}

	// Use this for initialization
	void Start ()
	{
        //speed = FlockingController._speed;
        orbAnimation = GetComponent<Animator>();
        orbAnimation.speed = Random.Range(0.2f, 1.8f);
        rotationSpeed = FlockingController._rotationSpeed;
        minDistance = FlockingController._minDistance;
        neighborDistance = FlockingController._neighborDistance;
        speedRange = FlockingController._speedRange;
		speed = Random.Range(speedRange[0], speedRange[1]);
        flockingController = GameObject.FindGameObjectWithTag("FlockManager");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Vector3.Distance(transform.position, flockingController.transform.position) >= FlockingController._orbContainerSize)
		{
			turning = true;
		}
		else turning = false;

		if (turning)
		{
			Vector3 direction = flockingController.transform.position - transform.position;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
			speed = Random.Range(speedRange[0], speedRange[1]);
		}
		else
		{
			if (Random.Range(0, 5) < 1)
			{
				ApplyRules();
			}
		}

		transform.Translate(0, 0, Time.deltaTime * speed);
	}
}
