/*
An abstract class in order to define states for our player.
*/

using System.Collections;

/// <summary>
/// Unity has its own StateMachineBehaviour states.
/// In our case, we did not need the extra complexity.
/// </summary>
public interface IState
{
    /// <summary>
    /// A method to enter a state (change graphics for the state, play state sound...)
    /// </summary>
    public void Enter();
    public void Update();
    public IEnumerator Special();

    /// <summary>
    /// Method to Exit State (remove grpahics)
    /// </summary>
    public void Exit();
}
