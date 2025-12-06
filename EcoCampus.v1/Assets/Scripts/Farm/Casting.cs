using UnityEngine;

public class Casting : MonoBehaviour
{

    [SerializeField] private int percentege; // percentual de chance de pegar um peixe
    [SerializeField] private GameObject fishPrefab;

    private PlayerItems player;
    private PlayerAnim playerAnim;
    private bool detectingPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindObjectOfType<PlayerItems>();
        playerAnim = player.GetComponent<PlayerAnim>();
    }

    // Update is called once per frame
    void Update()
    {
        if (detectingPlayer && Input.GetKeyDown(KeyCode.E))
        {
            playerAnim.OnCastingStarted();    
        }
    }

    public void OnCasting()
    {
        int randomValue = Random.Range(1, 100);

        if (randomValue <= percentege)
        {
            // conseguiu pescar um peixe
            Instantiate(fishPrefab, player.transform.position + new Vector3(Random.Range(-2.5f,-1f), 0f, 0f), Quaternion.identity);
            Debug.Log("Você pescou um peixe!");
        }
        else
        {
            // Sobra nada pro betinha 
            Debug.Log("Sobra nada pro betinha.");
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
