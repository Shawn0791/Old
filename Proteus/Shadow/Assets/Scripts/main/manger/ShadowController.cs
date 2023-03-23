using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShadowController : MonoBehaviour
{
    public List<ParticleSystem> ps;

    public bool emit;

    public PapermanController ppmc;

    public static List<ShadowController> List = new List<ShadowController>();

    void Awake()
    {
        ShadowController.List.Add(this);
    }

    void OnDestroy()
    {
        ShadowController.List.Remove(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (emit)
        {
            foreach (var p in ps)
            {
                if (!p.isPlaying)
                {
                    p.Play();
                }
            }
        }
        else
        {
            foreach (var p in ps)
            {
                if (p.isPlaying)
                {
                    p.Stop();
                }
            }
            Destroy(gameObject,5);
        }
    }

    public float BackDistance = 0.5f;

    public void UpdatePosition(Vector3 pos)
    {
        transform.position = pos - Vector3.forward * BackDistance;
    }
}