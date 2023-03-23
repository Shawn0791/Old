using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    public Transform CurrentLightSource;

    private float _tickTimer = 0;

    private float _tickTime = 1;

    public StateMachine Sm;

    public Transform Mesh;

    public Move Move;

    private bool _hasRightMove;

    private bool _hasLeftMove;

    private ShadowController _shadow;

    public GameObject ShadowPrefab;

    public CameraController CameraController;

    private float TeleportAnimTimer = 0;

    public float TeleportAnimTime = 0.2f;

    public GameObject TeleportEffectParticle;

    private bool _dead = false;

    private float InteractStateTimer = 0;

    public float InteractStateTime = 0.4f;
    // Use this for initialization
    void Start()
    {
        Sm = new StateMachine();
    }

    // Update is called once per frame
    void Update()
    {
        _tickTimer += Time.deltaTime;
        if (_tickTimer > _tickTime)
        {
            _tickTimer -= _tickTime;
            Tick();
        }

        int moveInt = (_hasRightMove ? 1 : 0) + (_hasLeftMove ? -1 : 0);
        if (_dead)
        {
            moveInt = 0;
        }

        switch (Sm.GetState())
        {
            case StateMachine.EStates.Cinematic:
            case StateMachine.EStates.Die:
                moveInt = 0;
                break;
            case StateMachine.EStates.Interaction:
                moveInt = 0;
                InteractStateTimer -= Time.deltaTime;
                if (InteractStateTimer < 0)
                {
                    Sm.SetState(StateMachine.EStates.Idle);
                }
                break;

            case StateMachine.EStates.Jump:
            case StateMachine.EStates.Idle:
            case StateMachine.EStates.Walk:
                Move.SetMove(moveInt);
                if (moveInt == 0)
                {
                    Sm.SetState(StateMachine.EStates.Idle);
                }
                else
                {
                    Sm.SetState(StateMachine.EStates.Walk);
                }
                break;
        }

        UpdateShadow();

        if (TeleportAnimTimer > 0)
        {
            float f = TeleportAnimTimer / TeleportAnimTime;
            TeleportAnimTimer -= Time.deltaTime;
            Mesh.localScale = Vector3.one * ((TeleportAnimTimer > 0) ? (1 - f * 0.9f) : 1);
        }
    }

    void UpdateShadow()
    {
        if (CurrentLightSource == null)
        {
            if (_shadow)
            {
                _shadow.emit = false;
                _shadow = null;
            }
            return;
        }

        Vector3 lightPosition = CurrentLightSource.position;
        Vector3 myPosition = transform.position;
        Vector3 dir = myPosition - lightPosition;
        dir.Normalize();
        Ray ray = new Ray(myPosition, dir);
        RaycastHit hitInfo = new RaycastHit();
        LayerMask mask = 1 << (LayerMask.NameToLayer("shadowTargets"));
        Physics.Raycast(ray, out hitInfo, 50, mask.value);

        //Debug.Log(hitInfo.transform);//如果hitInfo.transform是空，说明没有影子
        if (hitInfo.transform != null)
        {
            if (_shadow == null)
            {
                GameObject shadow = Instantiate(ShadowPrefab);
                _shadow = shadow.GetComponent<ShadowController>();
                Move.SyncShadow();
                Move.ppmc.SyncShadow();
            }

            _shadow.emit = true;
            _shadow.UpdatePosition(hitInfo.point);
        }
        else
        {
            if (_shadow != null)
            {
                _shadow.emit = false;
                _shadow = null;
            }
        }
    }

    void Interact()
    {
        if (_dead)
        {
            return;
        }

        if (!Move.Grounded)
        {
            return;
        }
        if (!Move.MoveEnabled)
        {
            return;
        }

        switch (Sm.GetState())
        {
            case StateMachine.EStates.Idle:
            case StateMachine.EStates.Walk:
                Sm.SetState(StateMachine.EStates.Interaction);
                Move.ppmc.Interact();
                InteractStateTimer = InteractStateTime;
                break;
        }
    }

    void Teleport()
    {
        if (_dead)
        {
            return;
        }
        if (!_shadow)
        {
            return;
        }

        if (_shadow.emit)
        {
            float targetZ = GameManager.Instance.GetOtherPlane();
            float dist = _shadow.transform.position.z - targetZ;
            //Debug.Log("影子和另一个平面的距离是" + dist);
            if (Mathf.Abs(dist) < 1)
            {
                //Debug.Log("能传送");
                StartTeleport(targetZ);
            }
            else
            {
                //Debug.Log("影子位置不标准 不能传送");
            }
        }
        else
        {
            //Debug.Log("不能在没影子时传送");
        }
    }

    void StartTeleport(float targetZ)
    {
        Plane p = new Plane(Vector3.back, targetZ);
        Vector3 lightPosition = CurrentLightSource.position;
        Vector3 myPosition = transform.position;
        Vector3 dir = myPosition - lightPosition;
        Ray ray = new Ray(myPosition, dir);

        float enter = 100;
        if (p.Raycast(ray, out enter))
        {
            Debug.Log("Plane Raycast hit at distance: " + enter);
            Vector3 hitPoint = ray.GetPoint(enter);
            //Debug.Log(hitPoint);

            PlayTeleportAnim();
            transform.position = hitPoint;
        }
    }

    void Tick()
    {
        Move.Tick();
        if (Sm.GetState() == StateMachine.EStates.Idle)
        {
            //idle anim
        }
    }

    public void ReceiveInput(InputManager.EInput eInput)
    {
        if (eInput == InputManager.EInput.Right)
        {
            _hasRightMove = true;
        }
        if (eInput == InputManager.EInput.Left)
        {
            _hasLeftMove = true;
        }
        if (eInput == InputManager.EInput.Jump)
        {
            Move.Jump();
        }
        if (eInput == InputManager.EInput.Teleport)
        {
            Teleport();
        }
        if (eInput == InputManager.EInput.Interact)
        {
            Interact();
        }
    }

    public void CeaseInput(InputManager.EInput eInput)
    {
        if (eInput == InputManager.EInput.Right)
        {
            _hasRightMove = false;
        }
        if (eInput == InputManager.EInput.Left)
        {
            _hasLeftMove = false;
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.transform.GetComponent<KillZone>() != null)
        {
            Die();
            return;
        }

        if (c.gameObject.tag == "Finish")
        {
            FinishLevel();
            return;
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (c.transform.GetComponent<LightSourceSwitcher>() != null)
        {
            CurrentLightSource = c.transform.GetComponent<LightSourceSwitcher>().Switch(transform);
        }

        if (c.transform.GetComponent<CameraSwitcher>() != null)
        {
            c.transform.GetComponent<CameraSwitcher>().Switch(transform);
        }
    }

    void Die()
    {
        if (_dead)
        {
            return;
        }
        _dead = true;
        Debug.Log("Die!!");
        Move.Die();
        GameManager.Instance.OnPlayDie();
    }

    void FinishLevel()
    {
        Debug.Log("FinishLevel!!");
        GameManager.Instance.OnPlayWin();
    }

    void PlayTeleportAnim()
    {
        Mesh.localScale = Vector3.one * 0.1f;
        TeleportAnimTimer = TeleportAnimTime;
        GameObject ef = Instantiate(TeleportEffectParticle);
        ef.transform.position = transform.position + Vector3.up;
    }
}
