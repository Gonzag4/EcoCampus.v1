using UnityEngine;
using UnityEngine.AI;

public class Skeleton: MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private AnimationControl animControl;

    private Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindObjectOfType<Player>();
        agent.updateRotation = false; //desativa a rotação automática do NavMeshAgent
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position); //esqueleto se movimenta em direção ao jogador

        if(Vector2.Distance(transform.position, player.transform.position) <= agent.stoppingDistance)
        {
            //chegou no limite de distância para atacar / para
            animControl.PlayAnim(2); //animação de ataque
        }
        else
        {
            //segue o player 
            animControl.PlayAnim(1); //animação de andar

        }
    }
}
