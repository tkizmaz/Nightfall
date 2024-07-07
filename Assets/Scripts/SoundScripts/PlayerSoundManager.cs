using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    private AudioSource sfxSource;

    [SerializeField]
    private AudioClip swordSwingSound;

    [SerializeField]
    private AudioClip swordHitEnemySound;

    [SerializeField]
    private AudioClip kickSound;
    [SerializeField]
    private AudioClip footStepSound;


    private void Awake() 
    {
        sfxSource = GetComponent<AudioSource>();
    }

    public void KickSfxRoutine()
    {
        sfxSource.clip = kickSound;
        sfxSource.Play();
    }

    public void SwordSwingSfxRoutine()
    {
        sfxSource.clip = swordSwingSound;
        sfxSource.Play();
    }

    public void SwordHitEnemySfxRoutine()
    {
        sfxSource.clip = swordHitEnemySound;
        sfxSource.Play();
    }

    public void PlayFootStepSfx()
    {
        sfxSource.clip = footStepSound;
        sfxSource.Play();
    }

}
