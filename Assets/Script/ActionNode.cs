using UnityEngine;

public class ActionNode : Node
{
    private System.Func<State> action;

    public ActionNode(System.Func<State> action)
    {
        this.action = action;
    }

    public override State Evaluate()
    {
        return action();
    }
}
