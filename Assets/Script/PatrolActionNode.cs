using UnityEngine;

public class PatrolActionNode : Node
{
    private Transform npcTransform;
    private float patrolDistance;
    private float patrolSpeed;
    private Vector3 startPosition;
    private bool movingForward = true;

    public PatrolActionNode(Transform npcTransform, float patrolDistance, float patrolSpeed)
    {
        this.npcTransform = npcTransform;
        this.patrolDistance = patrolDistance;
        this.patrolSpeed = patrolSpeed;
        this.startPosition = npcTransform.position;
    }

    public override State Evaluate()
    {
        Debug.Log("EVALUATING PATROL");
        float distanceFromStart = Vector3.Distance(npcTransform.position, startPosition);

        if (distanceFromStart >= patrolDistance)
        {
            movingForward = !movingForward;
        }

        Vector3 direction = movingForward ? Vector3.forward : Vector3.back;
        npcTransform.Translate(direction * patrolSpeed * Time.deltaTime);

        NodeState = State.Running;
        return NodeState;
    }
}
