using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FerrisWheel : MonoBehaviour
{
    private bool canMove = false;

   

    private float acc = 10;

    private float speed = 0;

    private float maxSpeed = 35;

    public GameObject MyCabinPrefab;

    private float degreePassed = 0;

    public Cabin cabinWithPlayer;

    void Start()
    {
        for (int i = 0; i < 10; i = i + 1)
        {
            CreateCabin(i);
        }
    }

    void CreateCabin(int count)
    { 
        GameObject tempCabin = Instantiate(MyCabinPrefab);
        tempCabin.transform.parent = transform;
        tempCabin.transform.localPosition = new Vector3(0, 0, 0);

        int z = count * 36;
        tempCabin.transform.localEulerAngles = new Vector3(0, 0, z);

        Cabin cabinScript = tempCabin.GetComponentInChildren<Cabin>();
        cabinScript.MyFerrisWheel = this;
    }

    void Update()
    {
        if (degreePassed >= 360)
        {
            canMove = false;
            OpenDoorExit();
        }
        if (canMove)
        {
     
            if (speed < maxSpeed)
            {
                speed += acc * Time.deltaTime;
            }
            float d = speed * Time.deltaTime;
            transform.eulerAngles = transform.eulerAngles + new Vector3(0, 0, speed * Time.deltaTime);
            degreePassed = degreePassed + d;

     
        }
    }

    public void StartMove()
    {
        Debug.Log("我要开始转动了哦");
        canMove = true;
        
    }
    void OpenDoorExit()
    {
        cabinWithPlayer.doorExit.SetActive(false);
    }
}