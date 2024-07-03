using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Blink : Ability
{   
    [SerializeField]
    private Camera fpsCam;
    [SerializeField]
    private float maxDistance;
    [SerializeField]
    private float offsetDistance = 0.5f;
    [SerializeField]
    private ParticleSystem blinkParticle;
    private Vector3 blinkDestination;
    private bool isBlinkInterrupted = false;
    private KeyCode blinkInterruptionKey = KeyCode.F;
    private void Awake() 
    {
        abilityType = AbilityType.Blink;
        blinkParticle.Stop();
        manaCost = 20;
        cooldownDuration = 2f;
    }

    private void Update()
    {
        PerformAbility();
    }

    protected override void PerformAbility()
    {
        bool hasEnoughMana = HasEnoughMana();
        if(isReadyToPerform && hasEnoughMana && isAbilitySelected)
        {
            if(Input.GetMouseButton(1) && !isBlinkInterrupted)
            {
                RaycastHit hit;
                if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, maxDistance))
                {
                    Vector3 nearestPoint = hit.point - fpsCam.transform.forward * offsetDistance;
                    Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * hit.distance, Color.black);

                    nearestPoint.y = CheckGround(nearestPoint.y);
                    blinkDestination = nearestPoint;
                }
                else
                {
                    Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * maxDistance, Color.black);
                    blinkDestination = fpsCam.transform.position + fpsCam.transform.forward * maxDistance;
                    blinkDestination.y = CheckGround(blinkDestination.y);
                }
                blinkParticle.transform.position = blinkDestination;
                blinkParticle.Play();

                if(Input.GetKeyDown(blinkInterruptionKey))
                {
                    isBlinkInterrupted = true;
                }
            }

            else if(Input.GetMouseButtonUp(1))
            {
                if(isBlinkInterrupted)
                {
                    isBlinkInterrupted = false;
                    blinkParticle.Stop();
                    return;
                }
                else
                {
                    CharacterController cc = this.gameObject.GetComponent<CharacterController>();
                    if (cc != null)
                    {
                        cc.enabled = false;
                        this.gameObject.transform.position = blinkDestination;
                        cc.enabled = true;
                    }
                    else
                    {
                        this.gameObject.transform.position = blinkDestination;
                    }
                    blinkParticle.Stop();
                    base.PerformAbility();
                }
            }
        }
        
    }

    private float CheckGround(float yPoint)
    {
        return yPoint < 0.0f ? 0.0f : yPoint;
    }
}
