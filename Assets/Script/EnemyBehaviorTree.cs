using System.Collections.Generic;
using UnityEngine;

public class BehaviorTreeExample : MonoBehaviour
{
    private Node behaviorTree;

    public Transform player; // Refer�ncia ao jogador
    public Transform item;   // Refer�ncia ao item

    public float patrolDistance = 5f;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3f;
    public float returnSpeed = 2f;

    private Vector3 startPosition; // Posi��o inicial do NPC
    private bool isPatrolling = true; // Come�a em patrulha

    private void Start()
    {
        // Salva a posi��o inicial do NPC
        startPosition = transform.position;

        // N� de patrulha
        var patrol = new PatrolActionNode(transform, patrolDistance, patrolSpeed);

        // N� de persegui��o
        var chase = new ChaseActionNode(transform, player, chaseSpeed);

        // N� de retorno � posi��o inicial
        var returnToStart = new ActionNode(() =>
        {
            if (!isPatrolling)
            {
                if (Vector3.Distance(transform.position, startPosition) < 0.5f)
                {
                    Debug.Log("Returned to start position: Success");
                    isPatrolling = true; // Marca como pronto para patrulhar
                    return Node.State.Success;
                }

                // Move em dire��o � posi��o inicial
                Vector3 direction = (startPosition - transform.position).normalized;
                transform.Translate(direction * returnSpeed * Time.deltaTime);
                return Node.State.Running;
            }

            return Node.State.Failure;
        });

        // Condi��es
        var isPlayerNearItem = new ConditionNode(() =>
        {
            bool result = Vector3.Distance(player.position, item.position) < 10f;
            if (result)
            {
                isPatrolling = false; // Interrompe a patrulha para perseguir
            }
            return result;
        });

        // Sequ�ncias
        var chaseSequence = new Sequence(new List<Node> { isPlayerNearItem, chase });

        var patrolSequence = new Sequence(new List<Node> {
            new ConditionNode(() => isPatrolling), // S� patrulha se estiver no estado de patrulha
            patrol
        });

        var returnAndPatrolSequence = new Sequence(new List<Node> { returnToStart });

        // Configurar a �rvore de comportamento
        behaviorTree = new Selector(new List<Node> { chaseSequence, returnAndPatrolSequence, patrolSequence });
        Debug.Log("Behavior tree initialized");
    }

    private void Update()
    {
        if (behaviorTree != null)
        {
            behaviorTree.Evaluate();
        }
    }
}
