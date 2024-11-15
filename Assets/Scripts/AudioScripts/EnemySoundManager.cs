using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundManager : MonoBehaviour
{
    private AudioSource sfxSource;
    [SerializeField]
    private AudioClip swordSwingSound;
    [SerializeField]
    private AudioClip swordHitPlayerSound;
    [SerializeField]
    private AudioClip hurtSound;
    [SerializeField]
    private AudioClip deathSound;
    [SerializeField]
    private AudioClip painSound;
    [SerializeField]
    private AudioClip pistolShootSound;

    private void Awake() 
    {
        sfxSource = GetComponent<AudioSource>();
    }

    public void PlaySwordSwingSfx()
    {
        sfxSource.clip = swordSwingSound;
        sfxSource.Play();
    }

    public void PlaySwordHitPlayerSfx()
    {
        sfxSource.clip = swordHitPlayerSound;
        sfxSource.Play();
    }

    public void PlayHurtSfx()
    {
        sfxSource.clip = hurtSound;
        sfxSource.Play();
    }

    public void PlayDeathSfx()
    {
        sfxSource.clip = deathSound;
        sfxSource.Play();
    }

    public void PlayPainSfx()
    {
        sfxSource.clip = painSound;
        sfxSource.Play();
    }

    public void PlayPistolShootSound()
    {
        sfxSource.clip = pistolShootSound;
        sfxSource.Play();
    }
}
