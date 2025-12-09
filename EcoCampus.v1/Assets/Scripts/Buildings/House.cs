using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    [Header("Amaunts")]
    [SerializeField] private int woodAmount;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    [SerializeField] private float timeAmount;

    [Header("Components")]
    [SerializeField] private GameObject houseColl;
    [SerializeField] private SpriteRenderer houseSprite;
    [SerializeField] private Transform point;


    private Player1 player1;
    private bool detectingPlayer;
    private PlayerAnim playerAnim;
    private PlayerItems playerItems;

    private float timeCount;
    private bool isBegining;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player1 = FindObjectOfType<Player1>();
        playerAnim = player1.GetComponent<PlayerAnim>();
        playerItems = player1.GetComponent<PlayerItems>();
    }

    // Update is called once per frame
    void Update()
    {
        if (detectingPlayer && Input.GetKeyDown(KeyCode.E) && playerItems.totalWood >= woodAmount)
        {
            //construção inicializada 

            isBegining = true; 
            playerAnim.OnHammeringStarted();
            houseSprite.color = startColor;
            player1.transform.position = point.position;
            player1.isPaused = true;
            playerItems.totalWood -= woodAmount;
        }

        if (isBegining)
        {
            timeCount += Time.deltaTime;
          
            if (timeCount >= timeAmount)
            {
                //casa é finalizada
                playerAnim.OnHammeringEnded();
                houseSprite.color = endColor;
                player1.isPaused = false;
                houseColl.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detectingPlayer = true;
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detectingPlayer = false;
        }
    }
}
