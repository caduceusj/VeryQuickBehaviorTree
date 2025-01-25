using UnityEngine;

public class ChaseActionNode : Node
{
    private Transform npcTransform;
    private Transform targetTransform;
    private float chaseSpeed;

    public ChaseActionNode(Transform npcTransform, Transform targetTransform, float chaseSpeed)
    {
        this.npcTransform = npcTransform;
        this.targetTransform = targetTransform;
        this.chaseSpeed = chaseSpeed;
    }

    public override State Evaluate()
    {
        if (targetTransform == null)
        {
            NodeState = State.Failure;
            return NodeState;
        }

        // Move em direção ao jogador
        Vector3 direction = (targetTransform.position - npcTransform.position).normalized;
        npcTransform.Translate(new Vector3(direction.x, direction.y * 0, direction.z) * chaseSpeed * Time.deltaTime);

        NodeState = State.Running;
        return NodeState;
    }
}
