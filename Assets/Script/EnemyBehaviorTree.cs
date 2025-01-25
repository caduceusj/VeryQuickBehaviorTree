using System.Collections.Generic;
using UnityEngine;

public class BehaviorTreeExample : MonoBehaviour
{
    private Node behaviorTree;

    public Transform player; // Referência ao jogador
    public Transform item;   // Referência ao item

    public float patrolDistance = 5f;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 3f;
    public float returnSpeed = 2f;

    private Vector3 startPosition; // Posição inicial do NPC
    private bool isPatrolling = true; // Começa em patrulha

    private void Start()
    {
        // Salva a posição inicial do NPC
        startPosition = transform.position;

        // Nó de patrulha
        var patrol = new PatrolActionNode(transform, patrolDistance, patrolSpeed);

        // Nó de perseguição
        var chase = new ChaseActionNode(transform, player, chaseSpeed);

        // Nó de retorno à posição inicial
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

                // Move em direção à posição inicial
                Vector3 direction = (startPosition - transform.position).normalized;
                transform.Translate(direction * returnSpeed * Time.deltaTime);
                return Node.State.Running;
            }

            return Node.State.Failure;
        });

        // Condições
        var isPlayerNearItem = new ConditionNode(() =>
        {
            bool result = Vector3.Distance(player.position, item.position) < 10f;
            if (result)
            {
                isPatrolling = false; // Interrompe a patrulha para perseguir
            }
            return result;
        });

        // Sequências
        var chaseSequence = new Sequence(new List<Node> { isPlayerNearItem, chase });

        var patrolSequence = new Sequence(new List<Node> {
            new ConditionNode(() => isPatrolling), // Só patrulha se estiver no estado de patrulha
            patrol
        });

        var returnAndPatrolSequence = new Sequence(new List<Node> { returnToStart });

        // Configurar a árvore de comportamento
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
