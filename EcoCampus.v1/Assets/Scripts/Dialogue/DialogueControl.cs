using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    [System.Serializable]
    public enum idiom
    {
        pt,
        eng,
        spa
    }

    public idiom language; //variavel para selecionar o idioma

    [Header("Components")]  // cabeçalho no inspetor
    public GameObject dialogueObj; //janela de dialogo
    public Image profileSprite;// sprite de perfil
    public Text speechText; // texto da fala
    public Text actorNameText; // nome do npc 

    [Header("Settings")]
    public float typingSpeed; //velocidade da fala 


    //variaveis de controle:
    public bool isShowing; // se o dialogo esta aparecendo (visivel)
    private int index; // indice para controlar as falas
    private string[] sentences; // array de frases
    private string[] currentActorName;
    private Sprite[] actorSprite;

    private Player1 player; 

    public static DialogueControl Instance;

    // é chamado antes de todos os Start() ba hierarquia de execução de scripts 
    private void Awake()
    {
        Instance = this;
    }


    //é chamado ao inicializar 
    void Start()
    {
        player = FindObjectOfType<Player1>();
    }

    IEnumerator TypeSentence()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed); //variavel pra controlar a velocidade da fala 
        }
    }

    //avança para a proxima fala
    public void NextSentence()
    {
        if (speechText.text == sentences[index])
        {
          if (index < sentences.Length - 1 )
          {
                index++;  
                profileSprite.sprite = actorSprite[index]; //atualiza o sprite do perfil
                actorNameText.text = currentActorName[index]; //atualiza o nome do npc
                speechText.text = ""; // apaga o texto anterior para pular para o proximo
                StartCoroutine(TypeSentence());

          }
          else //quando terminar os textos
          {
                speechText.text = "";
                actorNameText.text = "";
                index = 0; 
                dialogueObj.SetActive(false); // desativa o objeto de dialogo
                sentences = null; // limpa as sentenças
                isShowing = false;
                player.isPaused = false; // libera o jogador
            }
        }
    }

    //chamar a fala do npc 
    public void Speech(string[] txt, string[] actorName, Sprite[] actorProfile)
    {
        if(!isShowing)
        {
            dialogueObj.SetActive(true);
            sentences = txt;
            currentActorName = actorName;
            actorSprite = actorProfile;
            profileSprite.sprite = actorSprite[index]; //atualiza o sprite do perfil
            actorNameText.text = currentActorName[index]; //atualiza o nome do npc
            StartCoroutine(TypeSentence());
            isShowing = true;
            player.isPaused = true; // pausa o jogador enquanto o dialogo estiver ativo
        }
    }
}
