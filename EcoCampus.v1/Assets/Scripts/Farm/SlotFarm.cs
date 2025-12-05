using Unity.VisualScripting;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
public class NewMonoBehaviourScript : MonoBehaviour
{

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite hole;
    [SerializeField] private Sprite carrot;

    [SerializeField] private int digAmount; //quantidade de escavação necessária para cavar o buraco
    private int initialDigAmount;


    void Start()
    {
        initialDigAmount = digAmount;
    }


    public void OnHit()
    {
      
        digAmount--;

        if (digAmount <= initialDigAmount / 2)
        {
            spriteRenderer.sprite = hole;
        }
       /* if (digAmount <= 0)
        {
            //plantar cenoura
            spriteRenderer.sprite = carrot;

        }*/
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dig"))
        {
            Debug.Log("cavou");
            OnHit();
        }

    }



    
    void Update()
    {
        
    }
}
