using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    public static SoundManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    public void playAudio(AudioClip audioClip, Vector3 audioPos) {
        if (audioClip != null)
        {
            AudioSource.PlayClipAtPoint(audioClip, audioPos);
        }
    }
    
}
