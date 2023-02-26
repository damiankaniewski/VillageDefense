using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackPower = 10;
    private int attackDamage;
    public float attackRate = 4f;
    private float nextAttackTime = 0f;
    
    public TextMeshProUGUI playerInfoDamage;

    public Shop shop;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInfoDamage.SetText(attackPower.ToString());
        
        animator.SetBool("isAttacking", true);
        //attack
        if (Time.time >= nextAttackTime)
        {
            animator.SetBool("isAttacking", false);
            if (Input.GetKeyDown(KeyCode.Mouse0) && animator.GetBool("isDead") == false && shop.isShopActive == false)
            {
                animator.SetTrigger("Attack");
                //Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    public void Attack()
    {
        animator.ResetTrigger("Hurt");
        attackDamage = Random.Range(attackPower - (attackPower / 5), attackPower + (attackPower / 5));

        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log("I hit " + enemy);
            enemy.GetComponentInChildren<Enemy>().TakeDamage(attackPower);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}