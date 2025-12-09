using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public float Speed_walk;
    public float Jump_player;
    public bool isJump;
    public int LifeCount = 50;

    private Rigidbody2D rigidbody2D;
    private Animator animator;
    public GameObject Smoke;
    public static Player Instance;

    void Start()
    {

        //Inicializando variáveis
        Instance = this;

        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {

        Move();
        Jump();
        Attack();

        // Adicione esta verificação simples:
        if (rigidbody2D.linearVelocity.y == 0 && isJump)
        {
            isJump = false;
            animator.SetBool("jump", false);
        }


        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("SampleScene");
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            SceneManager.LoadScene("Credits");
        }

    }

    void Move()
    {
        if (animator.GetBool("atk") == false)
        {

            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
            // Horizontal => Input predefinido do Unity

            transform.position += move * Time.deltaTime * Speed_walk;

            if (Input.GetAxis("Horizontal") > 0f)
            {
                animator.SetBool("walk", true);
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
            if (Input.GetAxis("Horizontal") < 0f)
            {
                animator.SetBool("walk", true);
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
            if (Input.GetAxis("Horizontal") == 0f)
            {
                animator.SetBool("walk", false);
            }
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJump)
        {
            rigidbody2D.AddForce(new Vector2(0f, Jump_player), ForceMode2D.Impulse);
            animator.SetBool("jump", true);
            isJump = true;
        }
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (GameControl.Instance.ExtLevel > 1)
            {
                animator.SetBool("atk", true);
                Smoke.SetActive(true);

                GameControl.Instance.ExtLevel -= Time.deltaTime * 300;
            }
            else
            {
                GameControl.Instance.UpdateMenssage("Sem Gas!!!");
            }
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            animator.SetBool("atk", false);
            Smoke.SetActive(false);
            GameControl.Instance.UpdateMenssage("Mensagens");

        }
    }

    // Metodos padrao Unity: Ferificação de contado como o solo (layer 11)
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            isJump = false;
            animator.SetBool("jump", false);
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
        if (collider2D.gameObject.tag == "Vacuum")
        {

            animator.SetBool("die", true);
            LifeCount = 0;
            Destroy(gameObject, 1.2f);
        }
    }

    public void DestroyPlayer()
    {
        GameControl.Instance.GameOver();
        Destroy(gameObject);
    }
}
