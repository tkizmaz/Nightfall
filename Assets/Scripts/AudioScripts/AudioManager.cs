using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource sfxSource;
    [SerializeField]
    private AudioClip swallowSound;

    public void PlaySwallowSfx(StatType statType, int potionCount) 
    {
        sfxSource.clip = swallowSound;
        sfxSource.Play();
    }


}
