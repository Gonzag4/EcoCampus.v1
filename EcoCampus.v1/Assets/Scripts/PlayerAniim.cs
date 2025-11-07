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
            anim.SetInteger("Transition", 1); //andando
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
