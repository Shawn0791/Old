using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinZone4 : MonoBehaviour
{
    public GameObject MovePart;

    public Transform Destination;

    public float Acc = 10;

    public float TimeDropPlayer = 3;

    public float TimeBreak = 4;

    public float TimeDestroy = 10;

    public TrapPlank BreakablePart;

    public TrapPlank Floor;

    private bool _isMoving = false;

    private float _moveTimer = 0;

    private Vector3 _direction;

    private float _currentSpeed = 0;

    public Transform FloorTransfrom;

    private Character _playerInCabin;
    // Use this for initialization
    void Start()
    {
        _direction = Destination.position - MovePart.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMoving)
        {
            _currentSpeed += Acc * Time.deltaTime;
            MovePart.transform.position = MovePart.transform.position + _direction.normalized * _currentSpeed * Time.deltaTime;

            float beforeTime = _moveTimer;
            _moveTimer += Time.deltaTime;
            if (beforeTime < TimeDropPlayer && _moveTimer >= TimeDropPlayer)
            {
                DropPlayer();
            }

            if (beforeTime < TimeBreak && _moveTimer >= TimeBreak)
            {
                BreakSelf();
            }

            if (_moveTimer >= TimeDestroy)
            {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider c)
    {
        _playerInCabin = c.transform.GetComponent<Character>();
        if (!_isMoving && _playerInCabin != null)
        {
            TriggerCabin();
            _playerInCabin.transform.parent = FloorTransfrom;
            _playerInCabin.Move.MoveEnabled = false;
            _playerInCabin.Move.Stop();
            return;
        }
    }

    void TriggerCabin()
    {
        _isMoving = true;
    }

    void DropPlayer()
    {
        Floor.Triggering = true;
        _playerInCabin.transform.parent = null;
        _playerInCabin.Move.Stop();
        _playerInCabin.Move.MoveEnabled = true;
       // _playerInCabin.Move.Jump();
    }

    void BreakSelf()
    {
        BreakablePart.Triggering = true;
    }
}
