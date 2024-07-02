using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class Sword : MonoBehaviour
{
    private BoxCollider swordCollider;
    public UnityEvent onPlayerSlash;
    public PlayerAnimationController playerAnimationController;

    // Start is called before the first frame update
    void Start()
    {
        swordCollider = this.gameObject.GetComponent<BoxCollider>();
        swordCollider.enabled = false;
        if(playerAnimationController != null)
        {
            onPlayerSlash.AddListener(playerAnimationController.PlaySlashAnimation);
        }
    }

    private void Update() 
    {
        CheckAttack();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("hit enemy");
        }
    }

    private void CheckAttack()
    {
        if(Input.GetMouseButtonDown(0))
        {
            swordCollider.enabled = true;
            onPlayerSlash.Invoke();
        }
    }
}
