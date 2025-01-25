using UnityEngine;

public class ReturnToStartNode : Node
{
    private Transform npcTransform;
    private Vector3 startPosition;
    private float returnSpeed;

    public ReturnToStartNode(Transform npcTransform, Vector3 startPosition, float returnSpeed)
    {
        this.npcTransform = npcTransform;
        this.startPosition = startPosition;
        this.returnSpeed = returnSpeed;
    }

    public override State Evaluate()
    {
        // Verifica se o NPC está na posição inicial
        if (Vector3.Distance(npcTransform.position, startPosition) < 0.1f)
        {
            NodeState = State.Success;
            return NodeState;
        }

        Debug.Log("EVALUATING RETURN");

        // Move o NPC de volta à posição inicial
        Vector3 direction = (startPosition - npcTransform.position).normalized;
        npcTransform.Translate(direction * returnSpeed * Time.deltaTime);

        NodeState = State.Running;
        return NodeState;
    }
}
