using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{

    public float LeftCameraDist;

    public float RightCameraDist;

    public float LeftTimeToFollow = 0.5f;

    public float RightTimeToFollow = 0.5f;

    public float LeftMaxSpeed = 6;

    public float RightMaxSpeed = 6;

    public bool ModifyMaxSpeed = false;

    public float LeftAcc = 12;

    public float RightAcc = 12;

    public bool ModifyAcc = false;

    public void Switch(Transform trans)
    {
        //switch the light source of the player, when exit this
        bool isRight = trans.position.x - transform.position.x > 0;

        if (isRight)
        {
            GameManager.Instance.Player.CameraController.SetDist(RightCameraDist);
            GameManager.Instance.Player.CameraController.SetTimeToFollow(RightTimeToFollow);
            if (ModifyMaxSpeed)
            {
                GameManager.Instance.Player.CameraController.MaxSpeed = RightMaxSpeed;
            }
            if (ModifyAcc)
            {
                GameManager.Instance.Player.CameraController.Acc = RightAcc;
            }
        }
        else
        {
            GameManager.Instance.Player.CameraController.SetDist(LeftCameraDist);
            GameManager.Instance.Player.CameraController.SetTimeToFollow(LeftTimeToFollow);
            if (ModifyMaxSpeed)
            {
                GameManager.Instance.Player.CameraController.MaxSpeed = LeftMaxSpeed;
            }
            if (ModifyAcc)
            {
                GameManager.Instance.Player.CameraController.Acc = LeftAcc;
            }
        }
    }
}
