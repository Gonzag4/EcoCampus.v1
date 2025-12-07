using System.Xml.Serialization;
using UnityEngine;

public class ActorSFX : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    public void PlaySFX(AudioClip clip)
    {
         audioSource.PlayOneShot(clip);
    }



}
