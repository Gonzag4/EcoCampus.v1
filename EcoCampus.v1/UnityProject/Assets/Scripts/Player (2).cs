using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Configurações")]
    public float Speed_walk = 5f;
    public float Jump_player = 10f;
    public int LifeCount = 50;

    [Header("Referências")]
    public GameObject Smoke;

    [Header("Estado")]
    public bool isJump = false;
    public bool isDead = false;

    public static Player Instance;

    private Rigidbody2D rb;
    private Animator anim;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        isDead = false;

#if UNITY_EDITOR
        if (rb == null) Debug.LogError("Player precisa de Rigidbody2D");
        if (anim == null) Debug.LogError("Player precisa de Animator");
#endif
    }

    void Update()
    {
        if (isDead || GameControl.Instance == null || GameControl.Instance.IsGameOver()) return;
        if (GameControl.Instance.IsPaused()) return;

        Move();
        Jump();
        Attack();
    }

    void Move()
    {
        if (anim.GetBool("atk")) return;

        float horizontal = Input.GetAxis("Horizontal");
        Vector3 move = new Vector3(horizontal, 0f, 0f);
        transform.position += move * Time.deltaTime * Speed_walk;

        if (horizontal > 0f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
        else if (horizontal < 0f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }
        else
        {
            anim.SetBool("walk", false);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJump)
        {
            rb.AddForce(new Vector2(0f, Jump_player), ForceMode2D.Impulse);
            anim.SetBool("jump", true);
        }
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (GameControl.Instance.ExtLevel > 1f)
            {
                anim.SetBool("atk", true);
                if (Smoke != null) Smoke.SetActive(true);
                GameControl.Instance.ReduceExtLevel(Time.deltaTime * 300f);
            }
            else
            {
                GameControl.Instance.UpdateMenssage("Sem Gás!!!");
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            anim.SetBool("atk", false);
            if (Smoke != null) Smoke.SetActive(false);
            GameControl.Instance.ResetMessage();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            isJump = false;
            anim.SetBool("jump", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            isJump = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (isDead) return;

        if (collider2D.CompareTag("Vacuum"))
        {
            Die();
        }
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        LifeCount = 0;

        anim.SetBool("die", true);

        // Desabilita controles
        this.enabled = false;

        // Destrói após animação
        Destroy(gameObject, 1.2f);
    }

    // Mantido para compatibilidade com scripts antigos
    public void DestroyPlayer()
    {
        Die();
    }
}