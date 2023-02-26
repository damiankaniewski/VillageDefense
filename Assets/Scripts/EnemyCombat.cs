using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    private Animator animator;
    private PlayerHealth playerHealth;
    
    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            playerHealth = other.GetComponent<PlayerHealth>();
            Debug.Log("Player in collider!");
            if (!playerHealth.isDead())
            {
                animator.SetTrigger("Attack");
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player not in collider!");
            animator.ResetTrigger("Attack");
        } 
    }
}
