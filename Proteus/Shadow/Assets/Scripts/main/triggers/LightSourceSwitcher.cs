using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSourceSwitcher : MonoBehaviour
{

    public Transform LeftLight;

    public Transform RightLight;

    public Transform Switch(Transform trans)
    {
        //switch the light source of the player, when exit this
        bool isRight = false;
        if (trans.position.x-transform.position.x>0)
        {
            isRight = true;
        }

        if (isRight)
        {
            return RightLight;
        }
        else
        {
            return LeftLight;
        }
    }
}
