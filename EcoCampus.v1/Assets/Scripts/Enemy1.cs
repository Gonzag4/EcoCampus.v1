using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{

    public float Speed;
    public float StopEnemy;
    private Transform player;
    private Animator animator;
    public static Enemy Instance;

    private bool PlayerOn;

    void Start()
    {
        //Instance = this;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (PlayerOn)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, Speed * Time.deltaTime);
        }

        if (transform.position == player.position)
        {
            if (Player.Instance.LifeCount > 0)
            {
                Player.Instance.LifeCount -= 3;
                Debug.Log(Player.Instance.LifeCount);
            }

            if (Player.Instance.LifeCount < 0)
            {
                Player.Instance.DestroyPlayer();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            if (Vector2.Distance(transform.position, player.position) > StopEnemy)
            {
                PlayerOn = true;
            }
        }

        if (collider2D.gameObject.tag == "ColExtit")
        {
            Destroy(gameObject, 0f);
        }
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.tag == "Player")
        {
            if (Player.Instance.LifeCount > 0)
            {
                Player.Instance.LifeCount -= 3;
                Debug.Log(Player.Instance.LifeCount);
            }

            if (Player.Instance.LifeCount < 0)
            {
                Player.Instance.DestroyPlayer();
            }
        }
    }
}