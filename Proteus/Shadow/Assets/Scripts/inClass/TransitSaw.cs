using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitSaw : MonoBehaviour
{

    public Transform fromPoint;

    public Transform toPoint;

    public float speed = 10;
    
    public float OffsetTime = 0;

    private Vector3 fromPosition;

    private Vector3 toPosition;

    private float timer = 0;

    private float turnTime;

    public float RotateSpeed = 100;
    // Use this for initialization
    void Start()
    {

        fromPosition = fromPoint.position;

        toPosition = toPoint.position;

        float distance = Vector3.Distance(fromPosition, toPosition);

        turnTime = (distance * 2) / speed;

        timer = OffsetTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer + Time.deltaTime;

        float factor = timer / turnTime;
        if (factor > 1)
        {
            timer -= turnTime;
        }
        float factor2 = 1 - Mathf.Abs(factor - 0.5f) * 2f;

        transform.position = Vector3.Lerp(fromPosition, toPosition, factor2);

        Vector3 newEulerAngles = transform.eulerAngles;
        newEulerAngles.z += Time.deltaTime * RotateSpeed * (factor < 0.5 ? 1 : -1);
        transform.eulerAngles = newEulerAngles;
    }
}
