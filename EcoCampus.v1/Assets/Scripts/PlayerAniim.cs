using System.Collections.Generic;
using System.Collections;
using UnityEngine;
public class PlayerAniim : MonoBehaviour
{
    private Player player;
    private Animator anim;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        onMove();
        onRun();

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
}
