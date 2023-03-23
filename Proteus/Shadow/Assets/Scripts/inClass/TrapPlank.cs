using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPlank : MonoBehaviour
{

    public List<Rigidbody> RbList;

    public bool Triggering = false;

    public float ForceValue = 600;

    public Transform HitTransformFrom;//力量产生的目标位置

    private Vector3 _forceDir;//力量的方向

    void Update()
    {
        if (Triggering)
        {
            Triggering = false;
            ReleasePlank();
        }
    }

    void ReleasePlank()
    {
        foreach (Rigidbody rb in RbList)
        {
            _forceDir = rb.transform.position - HitTransformFrom.position;//计算力的方向
            _forceDir = _forceDir.normalized;//把向量的大小变成1

            rb.constraints = RigidbodyConstraints.None;

            Vector3 currentForce = ForceValue * _forceDir ;
            rb.AddForceAtPosition(currentForce, HitTransformFrom.position);
            rb.transform.parent = null;
            //rb.GetComponent<Collider>().enabled = false;
        }
    }
}
