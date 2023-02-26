using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggro : MonoBehaviour
{
    private Enemy enemy;
    private SphereCollider sphereCollider;

    private void Awake()
    {
        enemy = GetComponentInChildren<Enemy>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemy.isInAggroRange = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemy.isInAggroRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            enemy.isInAggroRange = false;
        }
    }
}