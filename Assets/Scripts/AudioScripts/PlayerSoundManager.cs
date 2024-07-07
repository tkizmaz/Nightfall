using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource sfxSource;
    [SerializeField]
    private AudioSource abilitySource;
    [SerializeField]
    private AudioClip footStepSound;
    [SerializeField]
    private AudioClip swordHitEnemySound;
    [SerializeField]
    private AudioClip swordSwingSound;
    [SerializeField]
    private AudioClip kickSound;
    [SerializeField]
    private AudioClip blinkSound;
    [SerializeField]
    private AudioClip bendTimeSound;
    [SerializeField]
    private AudioClip darkVisionSound;
    private Dictionary<AbilityType, AudioClip> abilitySounds = new Dictionary<AbilityType, AudioClip>();

    private void Awake() 
    {
        sfxSource = GetComponent<AudioSource>();
    }

    private void Start() 
    {
        AssignAbilitiesToSounds();
    }

    private void AssignAbilitiesToSounds()
    {
        abilitySounds.Add(AbilityType.Blink, blinkSound);
        abilitySounds.Add(AbilityType.BendTime, bendTimeSound);
        abilitySounds.Add(AbilityType.DarkVision, darkVisionSound);
    }

    public void PlayFootStepSfx()
    {
        sfxSource.clip = footStepSound;
        sfxSource.Play();
    }

    public void PlaySwordHitEnemySfx()
    {
        sfxSource.clip = swordHitEnemySound;
        sfxSource.Play();
    }

    public void PlaySwordSwingSfx()
    {
        sfxSource.clip = swordSwingSound;
        sfxSource.Play();
    }

    public void PlayKickSfx()
    {
        sfxSource.clip = kickSound;
        sfxSource.Play();
    }

    public void PlayAbilitySfx(AbilityType abilityType)
    {
        abilitySource.clip = abilitySounds[abilityType];
        abilitySource.Play();
    }

}
