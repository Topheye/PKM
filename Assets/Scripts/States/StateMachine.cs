/*
A state machine to handle state transition.
*/

using System;
using UnityEngine;

[Serializable]
public class PlayerStateMachine : MonoBehaviour
{
    #region State change
    public enum States
    {
        State1,
    }

    public IState CurrentState { get; private set; }
    #endregion

    /// <summary>
    /// Special powers
    /// </summary>
    #region Monobehaviour methods
    public void Awake() { }

    public void Update() { }
    #endregion

    #region Subscribe to events
    void OnEnable() { }

    void OnDisable() { }
    #endregion

    #region State transition
    /// <summary>
    /// Initializes with our first state.
    /// </summary>
    /// <param name="startingState"></param>
    public void TransitionTo(IState nextState)
    {
        CurrentState?.Exit();
        CurrentState = nextState;
        nextState?.Enter();
    }
    #endregion
}
