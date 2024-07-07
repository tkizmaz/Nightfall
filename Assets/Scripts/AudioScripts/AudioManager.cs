using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource sfxSource;
    [SerializeField]
    private AudioClip swallowSound;
    [SerializeField]
    private AudioClip swordHitEnemySound;
    [SerializeField]
    private AudioClip swordSwingSound;
    [SerializeField]
    private AudioClip playerDetectedSound;
    [SerializeField]
    private AudioClip cowardSound;
    [SerializeField]
    private AudioClip kickSound;
    [SerializeField]
    private AudioClip blinkSound;
    [SerializeField]
    private AudioClip bendTimeSound;
    [SerializeField]
    private AudioClip darkVisionSound;
    private Dictionary<AbilityType, AudioClip> abilitySounds = new Dictionary<AbilityType, AudioClip>();

    public static AudioManager instance;

    private void Awake() 
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
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

    public void PlaySwallowSfx() 
    {
        sfxSource.clip = swallowSound;
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

    public void PlayPlayerDetectedSfx()
    {
        sfxSource.clip = playerDetectedSound;
        sfxSource.Play();   
    }

    public void PlayCowardSfx()
    {
        sfxSource.clip = cowardSound;
        sfxSource.Play();   
    }

    public void PlayKickSfx()
    {
        sfxSource.clip = kickSound;
        sfxSource.Play();   
    }

    public void PlayAbilitySfx(AbilityType abilityType)
    {
        sfxSource.clip = abilitySounds[abilityType];
        sfxSource.Play();
    }


}
