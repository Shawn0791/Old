using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public static float DistFar = 7;

    public static float DistMid = 5f;

    public static float DistNear = 3f;

    private float _targetDist;

    public Transform TargetTrans;

    public float MaxSpeed = 3;

    public float Acc = 10;

    public float Speed = 0;

    Vector3 _dirNormalized;

    private float _timeToFollow = 0.4f;

    private Vector3 _idealPos;
    // Use this for initialization
    void Start()
    {
        _dirNormalized = transform.position - TargetTrans.position;
        _dirNormalized.Normalize();
        SetMid();
        transform.position = TargetTrans.position + _dirNormalized * _targetDist;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (TargetTrans != null)
        {
            _idealPos = TargetTrans.position + _dirNormalized * _targetDist;
        }

        MoveToIdealPos();
    }

    public void LooseTarget()
    {
        TargetTrans = null;
    }

    private void MoveToIdealPos()
    {
        Vector3 dir = _idealPos - transform.position;

        float distance = dir.magnitude;

        if (distance > 0.5 * Acc * _timeToFollow * _timeToFollow)
        {
            //摄像机的实际位置距离理想位置太远，要加速
            Speed = Speed + Acc * Time.deltaTime;
            if (Speed > MaxSpeed)
            {
                Speed = MaxSpeed;
            }
        }
        else
        {
            //摄像机的实际位置距离理想位置太近，要减速
            Speed = Speed - Acc * Time.deltaTime;
            if (Speed < 0)
            {
                Speed = 0;
            }
        }

        if (distance <= Speed * Time.deltaTime)
        {
            transform.position = _idealPos;
            Speed = 0;
        }
        else
        {
            transform.position = transform.position + dir.normalized * Speed * Time.deltaTime;
        }
    }

    public void SetFar()
    {
        SetDist(DistFar);
    }

    public void SetMid()
    {
        SetDist(DistMid);
    }

    public void SetNear()
    {
        SetDist(DistNear);
    }

    public void SetDist(float dist)
    {
        _targetDist = dist * 1.05f;
    }

    public void SetTimeToFollow(float t)
    {
        _timeToFollow = t;
    }
}