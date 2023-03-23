using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlyWayPoints : MonoBehaviour
{
    private List<Vector3> _wayPoints;

    private int _nextWayPointIndex;

    private int _currentWayPointIndex;

    private float _speedX = 0;

    public float SpeedY = 3;

    public float MoveYTime = 2;

    private float _moveYTimer = 0;

    public Transform FireFly;

    public float TimeBetween2Points = 2;
    // Use this for initialization
    void Start()
    {
        _wayPoints = new List<Vector3>();
        for (int i = 0; i < transform.childCount; i++)
        {
            _wayPoints.Add(transform.GetChild(i).position);
        }

        OnArriveNextDestination(0);
    }

    // Update is called once per frame
    void Update()
    {
        float currentX = FireFly.transform.position.x;

        float nextPosX = _wayPoints[_nextWayPointIndex].x;

        float nextX = currentX + _speedX * Time.deltaTime;
        if ((nextX - nextPosX) * (currentX - nextPosX) <= 0)
        {
            OnArriveNextDestination(_nextWayPointIndex);
        }

        _moveYTimer += Time.deltaTime;
        if (_moveYTimer > MoveYTime)
        {
            _moveYTimer -= MoveYTime;
        }

        float nextY = FireFly.transform.position.y + Mathf.Sin(_moveYTimer * Mathf.PI * 2) * SpeedY*Time.deltaTime;
        Vector3 newPos = FireFly.transform.position;
        newPos.x = nextX;
        newPos.y = nextY;
        FireFly.transform.position = newPos;
    }

    void OnArriveNextDestination(int index)
    {
        _currentWayPointIndex = index;
        if (_currentWayPointIndex >= _wayPoints.Count)
        {
            _currentWayPointIndex = 0;
        }
        _nextWayPointIndex = index + 1;
        if (_nextWayPointIndex >= _wayPoints.Count)
        {
            _nextWayPointIndex = 0;
        }

        float currentPosX = _wayPoints[_currentWayPointIndex].x;
        float nextPosX = _wayPoints[_nextWayPointIndex].x;
        float deltaX = nextPosX - currentPosX;

        _speedX = deltaX / TimeBetween2Points;
    }
}
