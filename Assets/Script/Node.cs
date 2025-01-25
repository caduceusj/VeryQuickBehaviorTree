using UnityEngine;

public abstract class Node
{
    public enum State { Running, Success, Failure }
    public State NodeState { get; protected set; }

    public abstract State Evaluate();
}
