using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    public float sfxVolume = 0.5f; // if too quite, adjust
    public AudioClip gunSFX;
    public AudioClip swordSFX;
    public AudioClip deathSFX;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayGunSFX()
    {
        PlaySFX(gunSFX);
    }

    public void PlaySwordSFX()
    {
        PlaySFX(swordSFX);
    }

    public void PlayDeathSFX()
    {
        PlaySFX(deathSFX);
    }

    private void PlaySFX(AudioClip clip)
    {
        GameObject sfxObject = new GameObject("SFX");
        AudioSource audioSource = sfxObject.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = sfxVolume;
        audioSource.Play();
        Destroy(sfxObject, clip.length);
    }
}