using UnityEngine;
using System.Collections;

public class Metronome : MonoBehaviour
{
    public string nameTest = "abc";

    public Vector3 vectorTest;

    public float timer = 0;

    public int k = 7;

    public int drawCount = 0;

    public AnimationCurve ac;

    public Color col;

    PlayerController testPlayer;

    // Update is called once per frame, this time in one frame is Time.deltaTime
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 0.02f)
        {
            timer -= 0.02f;
            drawCount += 1;
            Tick(drawCount);
        }
    }

    void Tick(int drawCount)
    {
      
    }
}
