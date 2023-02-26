using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")] public int maxHealth = 100;
    public int currentHealth;
    public int healthRegenValue = 1;
    [Header("HealthAndArmor")] public int maxArmor = 50;
    public int currentArmor;
    public int armorRegenValue = 1;
    [Header("Components")] public HealthBar healthBar;
    public ArmorBar armorBar;
    private Animator animator;
    private Rigidbody rb;
    public TextMeshProUGUI playerInfoArmor;
    public TextMeshProUGUI playerInfoHealth;


    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        currentArmor = maxArmor;
        armorBar.SetMaxArmor(maxArmor);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        InvokeRepeating("ArmorRegen", 1, 2);
        InvokeRepeating("HealthRegen", 1, 2);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            currentArmor = 22;
            currentHealth = 56;
        }
        playerInfoArmor.SetText(maxArmor.ToString());
        playerInfoHealth.SetText(maxHealth.ToString());
    }

    public void TakeDamage(int damage)
    {
        if (currentArmor > 0 && currentHealth > 0)
        {
            animator.SetTrigger("Hurt");
            currentArmor -= damage;
        }
        else if (currentArmor <= 0 && currentHealth > 0)
        {
            animator.SetTrigger("Hurt");
            currentHealth -= damage;
        }
        else if (currentArmor <= 0 && currentHealth <= 0)
        {
            Die();
        }

        armorBar.SetArmor(currentArmor);
        healthBar.SetHealth(currentHealth);
    }

    public void TakeHealthDamage(int damage)
    {
        
        if (currentHealth > 0)
        {
            animator.SetTrigger("Hurt");
            currentHealth -= damage;
        }
        else
        {
            Die();
        }
        healthBar.SetHealth(currentHealth);
    }
    public void ArmorRegen()
    {
        if (currentArmor < maxArmor)
        {
            currentArmor += armorRegenValue;
        }

        armorBar.SetArmor(currentArmor);
    }

    public void HealthRegen()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += healthRegenValue;
        }

        healthBar.SetHealth(currentHealth);
    }

    public void RestoreHealth(int health)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += health;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }
    }
    public void RestoreArmor(int armor)
    {
        if (currentArmor < maxArmor)
        {
            currentArmor += armor;
            if (currentArmor > maxArmor)
            {
                currentArmor = maxArmor;
            }
        }
    }
    private void Die()
    {
        animator.SetTrigger("Die");
        animator.ResetTrigger("Die");
        animator.SetBool("isDead", true);
        Debug.Log("I died!");
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    public bool isDead()
    {
        if (animator.GetBool("isDead") == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}