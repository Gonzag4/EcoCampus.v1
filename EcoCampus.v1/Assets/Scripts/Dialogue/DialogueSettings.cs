using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 



//faz com que seja possivel criar um asset do tipo DialogueSettings no menu de criação de assets do Unity
[CreateAssetMenu(fileName = "New Dialogue", menuName = "New Dialogue/Dialogue")]
public class DialogueSettings : ScriptableObject //transforma em um gerenciador de scritable object
{
    [Header("Settings")]

    //actor é um padrão utilizado em todos os jogos
    
    public GameObject actor; //objeto que representa o personagem que está falando


    [Header("Dialogue")]
    public Sprite speakerSprite;
    public string sentence; // --> a fala 

    public List<Sentences> dialogues = new List<Sentences>(); //lista de falas

}


// a fala esta no sentences 
[System.Serializable]
public class Sentences
{
    public string actorName;
    public Sprite profile;
    public Languages sentence;

}

[System.Serializable]
public class Languages
{
    public string portuguese;
    public string english;
    public string spanish;
    
}

#if UNITY_EDITOR

[CustomEditor(typeof(DialogueSettings))] // --> referencia da classe que será customizada
public class BuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DialogueSettings ds = (DialogueSettings)target; //alvo onde vai armazenar e puxar as informações 
        
        
        Languages l = new Languages();
        l.portuguese = ds.sentence;

        Sentences s = new Sentences();
        s.profile = ds.speakerSprite;
        s.sentence = l;

        if (GUILayout.Button("Create Dialogue"))
        {
            if (ds.sentence != "")
            {
                ds.dialogues.Add(s);

                ds.speakerSprite = null;
                ds.sentence = "";
            }
        }
    }
}

#endif