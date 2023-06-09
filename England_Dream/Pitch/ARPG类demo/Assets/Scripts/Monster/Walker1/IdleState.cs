﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private Walker1 manager;
    private Parameter parameter;

    private float timer;

    public IdleState(Walker1 manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {
        if (parameter.isChanged == false)
        {
            parameter.anim.Play("idle1");
        }
        else
        {
            parameter.anim.Play("idle2");
        }
    }

    public void OnUpdate()
    {
        timer += Time.deltaTime;

        //接近目标切换为追击状态
        if (parameter.target != null &&
            parameter.target.position.x >= parameter.chasePoints[0].x &&
            parameter.target.position.x <= parameter.chasePoints[1].x) 
        {
            manager.TransitionState(StateType.Chase);
        }

        //到达停留时间开始巡逻
        if (timer >= parameter.idleTime)
        {
            manager.TransitionState(StateType.Patrol);
        }
    }

    public void OnExit()
    {
        //结束静止清空计时器
        timer = 0;
    }
}

public class NullState : IState
{
    private Walker1 manager;
    private Parameter parameter;

    public NullState(Walker1 manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }
    public void OnEnter()
    {

    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {

    }
}
