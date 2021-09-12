using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : Singleton<SoundEffectManager>
{
    [SerializeField]
    private List<AudioClip> clips = new List<AudioClip>();

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnFood()
    {
        PlayClip(0);
    }
    public void OnGameOver()
    {
        PlayClip(1);
    }
    public void OnPowerUp(bool isPositive)
    {
        if (isPositive)
        {
            PlayClip(2);
        }
        else
        {
            PlayClip(3);
        }
    }
    public void OnRevival()
    {
        PlayClip(4);
    }

    private void PlayClip(int n)
    {
        audioSource.clip = clips[n];
        audioSource.Play();
    }
}
