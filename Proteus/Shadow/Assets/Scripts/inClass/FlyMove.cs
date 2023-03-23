using UnityEngine;

public class FlyMove : MonoBehaviour
{

    public float Speed = 10;

    public float HalfTurnTime = 1.0f;

    public float HalfTurnTimer = 0;

    public AnimationCurve acHorizontal;

    public AnimationCurve acVertical;

    private Vector3 _forwardDirection = Vector3.right;

    void Start()
    {
        HalfTurnTimer = HalfTurnTime;
    }

    // Update is called once per frame
    void Update()
    {
        MoveVertical();
        MoveHorizontal();
    }

    //水平移动
    void MoveHorizontal()
    {
        HalfTurnTimer -= Time.deltaTime;
        if (HalfTurnTimer < 0)
        {
            HalfTurnTimer += HalfTurnTime;
            _forwardDirection = -_forwardDirection;
        }

        float currentSpeed = Speed * acHorizontal.Evaluate(1 - HalfTurnTimer / HalfTurnTime);
        transform.position += _forwardDirection * currentSpeed * Time.deltaTime;
    }

    void MoveVertical()
    {
        //用animation curve 实现上下移动
        // float currentSpeed = Speed * acVertical.Evaluate(1 - HalfTurnTimer / HalfTurnTime);

        //用纯数学的方法实现上下摆动
        transform.position += Vector3.up * Mathf.Sin(Time.time * 4) * Speed * Time.deltaTime;
    }
}