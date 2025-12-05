using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class Tree : MonoBehaviour
{

    [SerializeField] private float treeHealth;
    [SerializeField] private Animator anim;

    [SerializeField] private GameObject woodPrefab;
    [SerializeField] private int totalWood;

    [SerializeField] private ParticleSystem leafs;

    private bool isCut; // -> correção do bug de cortar a arvore varias vezes

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void OnHit()
    {
        treeHealth--;

        anim.SetTrigger("isHit");

        leafs.Play();

        if (treeHealth <= 0)
        { 
            //cria o toco e instancia os drops (madeira)
            for (int i = 0; i < totalWood; i++)
            {
                Instantiate(woodPrefab, transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0f), transform.rotation);
            }
            anim.SetTrigger("cut");

            isCut = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Axe") && !isCut) 
        { 
            Debug.Log("bateu");
            OnHit(); 
        }

    }

}
