using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Transform Foot1;

    public Transform Foot2;

    public CharacterController CC;

    public float MoveSpeed = 3;

    public float MoveAcc = 22;

    public float JumpForce = 10;

    public float Gravity = 20;

    public float GroundTestDistance = 0.3f;

    public bool MoveEnabled = false;

    private Vector3 _movement;

    private int _moveInt = 0;

    private Vector3 _fwd = Vector3.right;

    public PapermanController ppmc;

    public Transform Mesh;

    public bool Grounded
    {
        get { return Foot1Grounded || Foot2Grounded; }
    }

    public bool Foot1Grounded
    {
        get
        {
            return Physics.Raycast(Foot1.position + Vector3.up, Vector3.down, GroundTestDistance);
        }
    }

    public bool Foot2Grounded
    {
        get
        {
            return Physics.Raycast(Foot2.position + Vector3.up, Vector3.down, GroundTestDistance);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckGravity();

        CheckWalk();
        CC.Move(_movement * Time.deltaTime);
    }

    void CheckWalk()
    {
        if (!MoveEnabled)
        {
            return;
        }

        float targetSpeed = _moveInt * MoveSpeed;

        float deltaSpeed = Time.deltaTime * MoveAcc;
        if (_moveInt < 0)
        {
            deltaSpeed *= -1;
        }

        float currentHorizontalSpeed = _movement.x;

        float expectedHorizontalSpeed = currentHorizontalSpeed + deltaSpeed;

        expectedHorizontalSpeed = Mathf.Min(expectedHorizontalSpeed, Mathf.Abs(targetSpeed));
        expectedHorizontalSpeed = Mathf.Max(expectedHorizontalSpeed, -Mathf.Abs(targetSpeed));

        _movement.x = expectedHorizontalSpeed;
    }

    void CheckGravity()
    {
        float veloY = _movement.y;

        if (!Grounded)
        {
            veloY -= Time.deltaTime * Gravity;
        }
        else if (veloY < 0)
        {
            veloY = 0;
            if (_movement.x==0)
            {
                ppmc.OnGround();
            }
            else
            {
                ppmc.OnGroundWalk();
            }
        }

        _movement.y = veloY;
    }

    public void Die()
    {
        ppmc.Die();
        MoveEnabled = false;
        Stop();
    }

    public void SetMoveAxis(Vector3 fwd)
    {
        _fwd = fwd.normalized;
    }

    public void SetMove(int v)
    {
        if (_moveInt == v)
        {
            return;
        }

        if (v == 0)
        {
            ppmc.Stand();
        }
        else
        {
            ppmc.Walk();
            Mesh.transform.localScale = new Vector3((v > 0) ? 1 : -1, 1, 1);
            SyncShadow();
        }

        _moveInt = v;
    }

    public void SyncShadow()
    {
        bool notInversed = Mesh.transform.localScale.x>0;
        foreach (var s in ShadowController.List)
        {
            if (s != null && s.emit)
            {
                s.transform.localScale = new Vector3(notInversed ? 1 : -1, 1, 1);
                foreach (var particleSystem in s.ps)
                {
                    var shape = particleSystem.shape;
                    shape.rotation = new Vector3(0, 0, notInversed ? 0 : 180);
                }
            }
        }
    }

    public void Tick()
    {
        //Debug.Log("tick "+ Foot1Grounded+"  "+ Foot2Grounded);
    }

    public void Jump()
    {
        if (!MoveEnabled)
        {
            return;
        }

        if (Grounded)
        {
            _movement.y = JumpForce;
            ppmc.Jump();
        }
    }

    public void Stop()
    {
        _movement.x = 0;
        ppmc.Stand();
    }
}
