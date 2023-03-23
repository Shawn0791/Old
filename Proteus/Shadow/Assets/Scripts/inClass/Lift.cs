using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    private bool moveUp = false;

    public GameObject lift;
	// Use this for initialization
	void Start () {
	    moveUp = false;
    }
	
	// Update is called once per frame
	void Update () {
	    if (moveUp)
	    {
	        lift.transform.position = lift.transform.position + Vector3.up * 3 * Time.deltaTime;
	    }
	}

    void OnTriggerEnter(Collider other)
    {
        StartLift();
    }

    void StartLift()
    {
        moveUp = true;
    }
}
