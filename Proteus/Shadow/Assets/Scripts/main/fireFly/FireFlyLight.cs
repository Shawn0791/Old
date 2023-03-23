using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlyLight : MonoBehaviour
{
    public AnimationCurve Ac;

    public Light MyLight;

    public float TurnTime = 2;

    private float _turnTimer = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _turnTimer += Time.deltaTime;
        if (_turnTimer > TurnTime)
        {
            _turnTimer -= TurnTime;

        }

        float t = _turnTimer / TurnTime;

        MyLight.intensity = Ac.Evaluate(t) * 2;
    }
}
