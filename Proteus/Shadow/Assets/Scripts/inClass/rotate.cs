using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {

    private float speed = 200;
   

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float d = speed * Time.deltaTime;
        transform.eulerAngles = transform.eulerAngles + new Vector3(0, 0, speed * Time.deltaTime);
    }



    void OnTriggerEnter(Collider other)
    {
        Debug.Log("我被碰到了");
        if (other.tag == "Player")
        {
            Debug.Log("我被Player碰到了");
          
        }
    }
}
