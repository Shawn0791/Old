using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;


public class PapermanController : MonoBehaviour
{
    [System.Serializable]
    public class FrameInfo
    {
        public List<Vector3> Frames;

        public Transform Part;
    }

    [System.Serializable]
    public class FramesInfo
    {
        public FrameInfo Frame;

        public string Name;

        public int Count;

        public void Init()
        {
            Count = Frame.Frames.Count;
        }
    }

    [System.Serializable]
    public class AnimInfo
    {
        public List<FramesInfo> Anim;

        public bool Loop;

        public float Time;

        public float Timer;

        public void Init()
        {
            foreach (var anim in Anim)
            {
                anim.Init();
            }
        }
    }

    public AnimInfo StandAnim;

    public AnimInfo WalkAnim;

    public AnimInfo JumpAnim;

    public AnimInfo DieAnim;

    public AnimInfo InteractAnim;

    private float _animBlendTimer;

    public float AnimBlendTime;

    private AnimInfo _currentAnim;

    public bool Pause = false;

    public bool Blend = false;

    private bool _blockInput = false;

    [SerializeField]
    public bool ControlShadows = false;

    private string _animName = "";
    // Use this for initialization
    void Start()
    {
        StandAnim.Init();
        WalkAnim.Init();
        JumpAnim.Init();
        DieAnim.Init();
        InteractAnim.Init();
        Stand();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Pause)
        {
            PlayAnim();
        }
    }

    public void SyncShadow()
    {
        if (_animName == "")
        {
            return;
        }

        if (ControlShadows)
        {
            foreach (var s in ShadowController.List)
            {
                if (s != null && s.emit && s.ppmc != null && !s.ppmc.ControlShadows)
                {
                    s.ppmc.Invoke(_animName, 0);
                }
            }
        }
    }

    private void SetToAnim(AnimInfo anim, string animName)
    {
        if (_currentAnim == anim)
        {
            return;
        }

        _currentAnim = anim;
        _currentAnim.Timer = 0;

        _animName = animName;
        SyncShadow();
    }

    public void Walk()
    {
        if (_blockInput)
        {
            return;
        }
        SetToAnim(WalkAnim,"Walk");
        
    }

    public void OnGroundWalk()
    {
        if (_currentAnim == JumpAnim)
        {
            Walk();
        }
    }

    public void OnGround()
    {
        if (_currentAnim == JumpAnim)
        {
            Stand();
        }
    }

    public void Stand()
    {
        if (_blockInput)
        {
            return;
        }
        SetToAnim(StandAnim,"Stand");
    }

    public void Jump()
    {
        if (_blockInput)
        {
            return;
        }
        SetToAnim(JumpAnim, "Jump");
    }

    public void Interact()
    {
        if (_blockInput)
        {
            return;
        }
        SetToAnim(JumpAnim, "Interact");

    }
    public void Die()
    {
        if (_blockInput)
        {
            return;
        }
        _blockInput = true;
        SetToAnim(DieAnim,"Die");
    }

    private void PlayAnim()
    {
        _currentAnim.Timer += Time.deltaTime;

        float f = _currentAnim.Timer / _currentAnim.Time;
        int t = Mathf.FloorToInt(f);
        if (_currentAnim.Loop)
        {
            f = f - t;
            if (t % 2 == 1)
            {
                f = 1 - f;
            }
        }
        else if (f > 1)
        {
            f = 1;//return?
        }

        foreach (var anim in _currentAnim.Anim)
        {
            float division = (float)(anim.Count - 1);
            int lastIndex = Mathf.FloorToInt(division * f);
            int nextIndex = lastIndex + 1;
            float factor = (f - lastIndex / division) * division;
            lastIndex = Mathf.Max(lastIndex, 0);
            nextIndex = Mathf.Max(nextIndex, 0);
            lastIndex = Mathf.Min(lastIndex, (int)division);
            nextIndex = Mathf.Min(nextIndex, (int)division);
            if (nextIndex>= anim.Frame.Frames.Count)
            {
                nextIndex = lastIndex;
            }
            DisplayFrameByBlend(anim.Frame.Part, anim.Frame.Frames[lastIndex], anim.Frame.Frames[nextIndex], factor);
        }
    }

    private void DisplayFrameByBlend(Transform part, Vector3 f1, Vector3 f2, float factor)
    {
        //这里由于是纸片人的控制，要传坐标和角度
        //为了简化代码，只传一个Vector3
        //其中的xy是坐标，z是角度
        Vector3 v = f1 * (1 - factor) + f2 * factor;
        Vector3 newPos = part.localPosition;
        newPos.x = v.x;
        newPos.y = v.y;
        part.localPosition = newPos;

        Vector3 newEuler = part.localEulerAngles;
        newEuler.z = v.z;
        part.localEulerAngles = newEuler;
    }
}