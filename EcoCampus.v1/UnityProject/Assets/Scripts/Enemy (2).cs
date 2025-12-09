using UnityEngine;

public class Enemy : MonoBehaviour {
    [Header("Configurações")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float stopDistance = 1f;
    
    private Transform player;
    private Animator animator;
    private bool playerOn = false;
    
    void Start() {
        animator = GetComponent<Animator>();
        
        // Procura o player de forma segura
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) {
            player = playerObj.transform;
        } else {
            Debug.LogWarning("Enemy: Player não encontrado na cena!");
            enabled = false; // Desabilita o script se não tem player
        }
    }
    
    void Update() {
        if (player == null || Player.Instance == null || Player.Instance.isDead) {
            playerOn = false;
            return;
        }
        
        if (playerOn) {
            // Calcula direção para o player
            Vector2 direction = (player.position - transform.position).normalized;
            
            // Move em direção ao player
            transform.position = Vector2.MoveTowards(
                transform.position, 
                player.position, 
                speed * Time.deltaTime
            );
            
            // Rotação baseada na direção
            if (direction.x > 0) {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            } else if (direction.x < 0) {
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
            
            // Verifica se atingiu o jogador
            float distance = Vector2.Distance(transform.position, player.position);
            if (distance <= stopDistance) {
                AttackPlayer();
            }
        }
    }
    
    void AttackPlayer() {
        if (Player.Instance == null || Player.Instance.isDead) return;
        
        // Causa dano ao jogador
        Player.Instance.LifeCount = Mathf.Max(0, Player.Instance.LifeCount - 3);
        
        // Se matou o jogador
        if (Player.Instance.LifeCount <= 0 && !Player.Instance.isDead) {
            Player.Instance.Die(); // MUDADO: Usa Die() em vez de DestroyPlayer()
        }
    }
    
    void OnTriggerEnter2D(Collider2D collider2D) {
        if (collider2D.CompareTag("Player")) {
            playerOn = true;
        }
        
        if (collider2D.CompareTag("ColExtit")) {
            Destroy(gameObject);
        }
    }
    
    void OnTriggerExit2D(Collider2D collider2D) {
        if (collider2D.CompareTag("Player")) {
            playerOn = false;
            
            // Ataque quando o player sai do trigger (opcional)
            AttackPlayer();
        }
    }
    
    #if UNITY_EDITOR
    void OnDrawGizmosSelected() {
        // Visualiza a distância de parada
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stopDistance);
        
        // Visualiza direção de perseguição
        if (player != null) {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, player.position);
        }
    }
    #endif
}