using Unity.VisualScripting;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
public class SlotFarm: MonoBehaviour
{

    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite hole;
    [SerializeField] private Sprite carrot;

    [Header("Settings")]
    [SerializeField] private int digAmount; //quantidade de escavação necessária para cavar o buraco
    [SerializeField] private float waterAmount; //total de agua para nascer uma cenoura
   
    [SerializeField] private bool detecting;



    private int initialDigAmount;
    private float currentWater;

    private bool dugHole;

    PlayerItems playerItems;

    void Start()
    {
        playerItems = FindObjectOfType<PlayerItems>();
        initialDigAmount = digAmount;
        
    }

    void Update()
    {
        if (dugHole)
        {
            if (detecting)
            {
                currentWater += 0.01f;
            }

            //encheu a quantidade de agua necessária para nascer a cenoura
            if (currentWater >= waterAmount)
            {
                //plantar cenoura
                spriteRenderer.sprite = carrot;
                
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //colher cenoura
                    spriteRenderer.sprite = hole;
                    playerItems.carrots++;
                    Debug.Log("Cenoura Colhida! Total: " + playerItems.carrots);    

                    currentWater = 0f;
                }
            
            }
        }
     
    }


    public void OnHit()
    {
      
        digAmount--;

        if (digAmount <= initialDigAmount / 2)
        {
            spriteRenderer.sprite = hole;
            dugHole = true;
        }
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dig"))
        {
            Debug.Log("cavou");
            OnHit();
        }

        if (collision.CompareTag("Water"))
        {
            detecting = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            detecting = false;
        }
    }

}
