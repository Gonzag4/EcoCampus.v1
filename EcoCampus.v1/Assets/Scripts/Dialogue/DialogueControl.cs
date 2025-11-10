using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    [Header("Components")]  // cabeçalho no inspetor
    public GameObject dialogueObj; //janela de dialogo
    public Image profileSprite;// sprite de perfil
    public Text speechTest; // texto da fala
    public Text actorNameText; // nome do npc 

    [Header("Settings")]
    public float typingSpeed; //velocidade da fala 


    //variaveis de controle:
    private bool isShowing; // se o dialogo esta aparecendo (visivel)
    private int index; // indice para controlar as falas
    private string[] sentences; // array de frases


    void Start()
    {
        
    }

  
    void Update()
    {
        
    }

    IEnumerator TypeSentence()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            speechTest.text += letter;
            yield return new WaitForSeconds(typingSpeed); //variavel pra controlar a velocidade da fala 
        }
    }

    //avança para a proxima fala
    public void NextSentence()
    {

    }

    //chamar a fala do npc 
    public void Speech(string[] txt)
    {
        if(!isShowing)
        {
            dialogueObj.SetActive(true);
            sentences = txt;
            StartCoroutine(TypeSentence());
            isShowing = true;
        }
    }
}
