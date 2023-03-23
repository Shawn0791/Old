using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Game : UIBase
{
    public override void Init()
    {
        base.Init();
    }

    public override void OnOpen()
    {
        base.OnOpen();
    }

    public override void OnClose()
    {
        GameManager.Instance.SetGameRunningSpeed(GameSpeed.Normal);
        base.OnClose();
    }


}
