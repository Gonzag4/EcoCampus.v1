using System.Collections.Generic;
using System.Collections;
using UnityEngine;
public class PlayerAnim : MonoBehaviour
{

    [Header("Attack Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask enemyLayer;

    private Player player;
    private Animator anim;

    private Casting cast;

    private bool isHitting;
    private float recoveryTime = 1f;
    private float timeCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();

        cast = FindObjectOfType<Casting>();
    }

    // Update is called once per frame
    void Update()
    {
        onMove();
        onRun();

        timeCount += Time.deltaTime;
        
        if (isHitting)
        {
            if (timeCount >= recoveryTime)
            {
                isHitting = false;
                timeCount = 0f;
            }
        }
    }


    #region Movement
    void onMove()
    {
        if (player.Direction.sqrMagnitude > 0)
        {
            //só podera rolar se estiver andando
            if (player.isRolling)
            {
                anim.SetTrigger("isRoll");
            } else
            { 
                anim.SetInteger("Transition", 1); //andando
        
            }
        }
        else
        {
            anim.SetInteger("Transition", 0); //parado
        }

        if (player.Direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }

        if (player.Direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }

        if (player.isCutting)
        {
            anim.SetInteger("Transition", 3); // cortando
        }
        
        if (player.isDigging)
        {
            anim.SetInteger("Transition", 4); // cavando 
        }

        if (player.isWatering)
        {
            anim.SetInteger("Transition", 5); // regando
        }
    }

    void onRun()
    {
        if (player.isRunning)
        {
            anim.SetInteger("Transition", 2); // correndo
        }
    }
    #endregion

    #region Attack

    public void OnAttack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, enemyLayer);
        if (hit != null)
        {
            //attacou o inimigo
            //Debug.Log("Inimigo atingido!");
            hit.GetComponentInChildren<AnimationControl>().OnHit(); // procura os scripts filhos do inimigo para ativar a animação de hit 
        }

    }

    //criando um gizmo para visualizar o ponto de ataque no editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }

    #endregion


    // é chamado quando o jogador inicia o botão de ação na lagoa 
    public void OnCastingStarted()
    {
        anim.SetTrigger("isCasting");
        player.isPaused = true;
    }

    // é chamado no final da animação de pesca
    public void OnCastingEnded()
    {
        cast.OnCasting();
        player.isPaused = false;
    }

    public void OnHammeringStarted()
    {
        anim.SetBool("isHammering", true);
        
    }

    public void OnHammeringEnded()
    {
        anim.SetBool("isHammering", false);
    }

    public void OnHit()
    {
        if (!isHitting)
        { 
            anim.SetTrigger("hit");
            isHitting = true;
        }
    }
}
