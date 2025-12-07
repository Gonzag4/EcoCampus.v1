using JetBrains.Annotations;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; //singleton para ser acessado de qualquer lugar sem referenciar o objeto

    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {

        //faz com que o objeto(som) não seja destruido e não seja duplicado ao criar uma nova cena
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
        public void PlayBGM(AudioClip audio)
        {
            audioSource.clip = audio;
            audioSource.Play();
        }
    
}
