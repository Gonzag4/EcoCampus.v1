using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float speed;
    private float initialSpeed;

    private int index;
    private Animator anim;

    public List<Transform> paths = new List<Transform>(); // lista de pontos do caminho


    void Start()
    {
        initialSpeed = speed;
        anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        // pausa o npc quando o player estiver em diálogo
        if (DialogueControl.Instance.isShowing) 
        {
            speed = 0f;
            anim.SetBool("isWalking", false);
        }
        else
        {
            speed = initialSpeed;
            anim.SetBool("isWalking", true);
        }

        transform.position = Vector2.MoveTowards(transform.position, paths[index].position, speed * Time.deltaTime); // move o npc em direção ao ponto do caminho

        // verifica se o npc chegou no ponto do caminho
        if (Vector2.Distance(transform.position, paths[index].position) < 0.1f)
        {
            if (index < paths.Count - 1)
            {
                //index++;
                index = Random.Range(0, paths.Count); // escolhe um ponto aleatório do caminho
            }
            else
            {
                index = 0;
            }
        }
        // subitrair a posição do path menos a do npc para saber a direção

        Vector2 direction = paths[index].position - transform.position; //valores no eixo X

        if (direction.x > 0) // se o npc estiver indo para a direita pois x = positivo
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        if (direction.x < 0) // se o npc estiver indo para a esquerda pois x = negativo
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
        

    }
}
