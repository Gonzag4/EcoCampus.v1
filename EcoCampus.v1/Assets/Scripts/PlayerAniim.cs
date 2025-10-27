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
        if (player.Direction.sqrMagnitude > 0)
        {
            anim.SetInteger("Transition", 1);
        }
        else
        {
            anim.SetInteger("Transition", 0);
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
}
