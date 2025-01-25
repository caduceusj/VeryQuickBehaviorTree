using System.Collections.Generic;

public class Selector : Node
{
    private List<Node> children;

    public Selector(List<Node> nodes)
    {
        children = nodes;
    }

    public override State Evaluate()
    {
        foreach (var child in children)
        {
            var result = child.Evaluate();
            if (result == State.Success || result == State.Running)
            {
                NodeState = result;
                return NodeState;
            }
        }
        NodeState = State.Failure;
        return NodeState;
    }
}
