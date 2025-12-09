using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sing : MonoBehaviour
{

    public string Text;

    void Start()
    {

    }

    // Verificando colisaão com o player
    void OnTriggerEnter2D(Collider2D collider2D)
    {

        if (collider2D.gameObject.tag == "Player")
        {
            GameControl.Instance.UpdateMenssage(Text);
        }
    }

    void OnTriggerExit2D(Collider2D collider2D)
    {
        GameControl.Instance.UpdateMenssage("Mensagens");
    }
}
