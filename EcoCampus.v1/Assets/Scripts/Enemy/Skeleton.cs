using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Skeleton: MonoBehaviour
{

    [Header("Stats")]
    public float totalHealth;
    public float currentHealth;
    public Image healthBar;
    public bool isDead;


    [Header("components")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private AnimationControl animControl;


    private Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = totalHealth;
        player = FindObjectOfType<Player>();
        agent.updateRotation = false; //desativa a rotação automática do NavMeshAgent
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            agent.SetDestination(player.transform.position); //esqueleto se movimenta em direção ao jogador

            if (Vector2.Distance(transform.position, player.transform.position) <= agent.stoppingDistance)
            {
                //chegou no limite de distância para atacar / para
                animControl.PlayAnim(2); //animação de ataque
            }
            else
            {
                //segue o player 
                animControl.PlayAnim(1); //animação de andar

            }

            //Debug.Log(player.transform.position.x - transform.position.x);
            float posX = player.transform.position.x - transform.position.x;

            if (posX > 0)
            {
                transform.eulerAngles = new Vector2(0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector2(0, 180); // rotaciona o esqueleto para olhar para o player
            }

        }
    }
}
