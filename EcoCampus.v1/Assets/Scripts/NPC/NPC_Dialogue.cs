using System.Collections.Generic;
using UnityEngine;

public class NPC_Dialogue : MonoBehaviour
{

    public float dialogueRange;
    public LayerMask playerLayer;

    public DialogueSettings dialogue;

    bool playerHit;

    // a sentensa está sendo passada em maneira de lista e armazenada aqui 
    private List<string> sentences = new List<string>();

    private void Start()
    {
        GetNPCInfo();
    }

    //é chamado a cada frame 
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerHit)
        {
            //converte a lista em array e passa para o DialogueControl
            DialogueControl.Instance.Speech(sentences.ToArray());
        }
    }

    //selecionar um idioma e armazenar as sentenças na lista
    void GetNPCInfo()
    {
        for (int i =  0; i < dialogue.dialogues.Count; i++)
        {
            switch(DialogueControl.Instance.language)
            {
                case DialogueControl.idiom.pt:
                    sentences.Add(dialogue.dialogues[i].sentence.portuguese);
                    break;
                
                case DialogueControl.idiom.eng:
                    sentences.Add(dialogue.dialogues[i].sentence.english);
                    break;
               
                case DialogueControl.idiom.spa:
                    sentences.Add(dialogue.dialogues[i].sentence.spanish);
                    break;
            }

            
        }
    }

    // usado pela física 
    void FixedUpdate()
    {
        ShowDialogue();
    }

    void ShowDialogue()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, dialogueRange, playerLayer);

        if (hit != null)
        {
            playerHit = true;
        }
        else
        {
            playerHit= false;
            // DialogueControl.Instance.dialogueObj.SetActive(false);         --> não é mais necessário fechar o diálogo aqui, esta no DialogueControl
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, dialogueRange);
    }
}
