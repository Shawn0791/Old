using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    public enum EInput
    {
        Left,
        Right,
        Jump,
        Teleport,
        Interact,
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ReceiveInput(EInput.Left);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            CeaseInput(EInput.Left);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            ReceiveInput(EInput.Right);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            CeaseInput(EInput.Right);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReceiveInput(EInput.Jump);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            ReceiveInput(EInput.Interact);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            ReceiveInput(EInput.Teleport);
        }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            GameManager.Instance.Player.CameraController.SetDist(CameraController.DistFar);
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            GameManager.Instance.Player.CameraController.SetDist(CameraController.DistMid);
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            GameManager.Instance.Player.CameraController.SetDist(CameraController.DistNear);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Mgr_UI.Instance.ToUI<UI_GameMenu>(UIName.UI_GameMenu);
        }
    }

    public void ReceiveInput(EInput eInput)
    {
        GameManager.Instance.Player.ReceiveInput(eInput);
    }

    public void CeaseInput(EInput eInput)
    {
        GameManager.Instance.Player.CeaseInput(eInput);
    }
}
