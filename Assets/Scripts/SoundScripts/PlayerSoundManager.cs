using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    private AudioSource sfxSource;
    [SerializeField]
    private AudioClip footStepSound;


    private void Awake() 
    {
        sfxSource = GetComponent<AudioSource>();
    }

    public void PlayFootStepSfx()
    {
        sfxSource.clip = footStepSound;
        sfxSource.Play();
    }

}
