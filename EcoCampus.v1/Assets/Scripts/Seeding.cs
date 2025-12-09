using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeding : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    public GameObject collectedEffect;
    void Start()
    {
        //Inicializando variáveis
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Verificando colisaão com o player
    void OnTriggerEnter2D(Collider2D collider2D)
    {

        if (collider2D.gameObject.tag == "Player")
        {

            spriteRenderer.enabled = false;
            collectedEffect.SetActive(true);

            Destroy(gameObject, 0.2f);
        }
    }
}