using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    private float damageRate = 6f;
    private float nextDamageTime = 0f;
    
    private PlayerHealth playerHealth;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("collision with player");
            playerHealth = other.gameObject.GetComponent<PlayerHealth>();

            if (Time.time >= nextDamageTime)
            {
                playerHealth.TakeHealthDamage(1);
                nextDamageTime = Time.time + 1f / damageRate;
            }
            
        }
    }

    
}
