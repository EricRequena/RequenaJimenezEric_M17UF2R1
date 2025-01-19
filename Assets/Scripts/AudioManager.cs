using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //no se destruya al cambiar de escena
    public static AudioManager instance;
    public AudioSource audioSource;

    public AudioSource swordAudio;
    public AudioSource gunAudio;
    public AudioSource fireballAudio;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlaySwordAudio()
    {
        swordAudio.Play();
    }

    public void PlayGunAudio()
    {
        gunAudio.Play();
    }

    public void PlayFireballAudio()
    {
        fireballAudio.Play();
    }

    public void StopSwordAudio()
    {
        swordAudio.Stop();
    }

    public void StopFireballAudio()
    {
        fireballAudio.Stop();
    }

    
}
