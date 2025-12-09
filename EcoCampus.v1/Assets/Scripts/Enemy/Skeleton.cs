using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Skeleton: MonoBehaviour
{

    [Header("Stats")]
    public float radius;
    public LayerMask layer;
    public float totalHealth;
    public float currentHealth;
    public Image healthBar;
    public bool isDead;


    [Header("components")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private AnimationControl animControl;


    private Player1 player;
    private bool detectPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = totalHealth;
        player = FindObjectOfType<Player1>();
        agent.updateRotation = false; //desativa a rotação automática do NavMeshAgent
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead && detectPlayer)
        {
            agent.isStopped = false;
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

    private void FixedUpdate()
    {
        DetectPlayer();
    }


    public void DetectPlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, layer);

        if (hit != null)
        {
            Debug.Log("Player Detectado!");
            detectPlayer = true;
        }
        else
        {
            Debug.Log("Player Não Detectado!");
            detectPlayer = false;
            animControl.PlayAnim(0); //animação de idle
            agent.isStopped = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
