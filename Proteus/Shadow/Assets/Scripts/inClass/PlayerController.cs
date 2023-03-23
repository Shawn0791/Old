using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool isRunningLeft = false;

    bool isRunningRight = false;

    float jumpPrepareTimer = 0;

    float speedY = 0;

    public float speed = 5F;
    public float jumpForce = 50.0F;
    public float jumpPrepareTime = 0.1F;
    public float gravity = 20.0F;

    public Rigidbody rb;

    public ShadowController shadow;

    public GameObject avatar;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ControlPlayer();

        Move();
    }

    void ControlPlayer()
    {
        //按键移动
        if (Input.GetKeyDown(KeyCode.A))
        {
            isRunningLeft = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            isRunningRight = true;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            isRunningLeft = false;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            isRunningRight = false;
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Vector3 pos = transform.position;
            pos.z = 0;
            transform.position = pos;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Vector3 pos = transform.position;
            pos.z = 5;
            transform.position = pos;
        }
    }

    void Move()
    {
        if (jumpPrepareTimer > 0)
        {
            jumpPrepareTimer -= Time.deltaTime;
            if (jumpPrepareTimer <= 0)
            {
                StartJump();
            }
            return;
        }

        int dir = 0;
        if (isRunningRight)
        {
            dir += 1;
        }
        if (isRunningLeft)
        {
            dir -= 1;
        }

        if (dir == 1)
        {
            avatar.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (dir == -1)
        {
            avatar.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        Vector3 moveDirection = Vector3.zero;

        moveDirection.x = dir * speed;
        moveDirection.y = 0;

        //cc.Move(moveDirection * Time.deltaTime);
        rb.MovePosition(transform.position+ moveDirection * Time.deltaTime);
    }

    void Jump()
    {
        Debug.Log("Jump");
        /*
        if (!cc.isGrounded)
        {
            Debug.Log("ng");
            return;
        }
        */
        if (jumpPrepareTimer > 0)
        {
            Debug.Log(jumpPrepareTimer);
            return;
        }

        jumpPrepareTimer = jumpPrepareTime;
    }

    void StartJump()
    {
        //speedY = jumpForce;
        Debug.Log("StartJump");
        rb.AddForce(0,160,0);
    }
}
