using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinController : MonoBehaviour
{
    public Cabin MyCabin;



    void OnTriggerEnter(Collider other)
    {
        Debug.Log("我被碰到了");
        if (other.tag == "Player")
        {
            Debug.Log("我被Player碰到了");
            MyCabin.StartMove();
        }
    }
}