using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateChange
{
    void StateChange(StateMachine.State newState, StateMachine.State oldState);
}
