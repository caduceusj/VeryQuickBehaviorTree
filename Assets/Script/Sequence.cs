using System.Collections.Generic;

public class Sequence : Node
{
    private List<Node> children;

    public Sequence(List<Node> nodes)
    {
        children = nodes;
    }

    public override State Evaluate()
    {
        foreach (var child in children)
        {
            var result = child.Evaluate();
            if (result == State.Failure)
            {
                NodeState = State.Failure;
                return NodeState;
            }
            if (result == State.Running)
            {
                NodeState = State.Running;
                return NodeState;
            }
        }
        NodeState = State.Success;
        return NodeState;
    }
}

