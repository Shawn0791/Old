using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabin : MonoBehaviour
{
    public FerrisWheel MyFerrisWheel;

    public GameObject door;

    public GameObject doorExit;

    void Update()
    {
        transform.eulerAngles = new Vector3(0, 90, 180);
    }

    public void StartMove()
    {
         door.SetActive(true);
         MyFerrisWheel.StartMove();
         MyFerrisWheel.cabinWithPlayer = this;
    }
}