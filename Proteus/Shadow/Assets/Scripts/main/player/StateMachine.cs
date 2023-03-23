
using System;

public class StateMachine
{
    private EStates _state = EStates.Idle;

    public EStates GetState()
    {
        return _state;
    }

    public enum EStates
    {
        Idle,
        Die,
        Cinematic,
        Walk,
        Interaction,
        Jump,
    }

    public void SetState(EStates es)
    {
        if (es == _state)
        {
            return;
        }

        OnExitState(_state);
        _state = es;
        OnEnterState(_state);
    }

    protected virtual void OnExitState(EStates es)
    {

    }

    protected virtual void OnEnterState(EStates es)
    {

    }

    public bool CanReceiveMControl
    {
        get
        {
            if (_state == EStates.Walk ||
                _state == EStates.Idle)
            {
                return true;
            }

            return false;
        }
    }
}
