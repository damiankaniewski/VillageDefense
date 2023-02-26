using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    private Enemy enemy;
    private Camera cam;
    private void Start()
    {
        cam = FindObjectOfType<Camera>();
        enemy = GetComponentInParent<Enemy>();
    }

    private void Update()
    {
        transform.LookAt(cam.transform);
        if (enemy.GetHealthPercent() > 0)
        {
            transform.Find("Bar").localScale = new Vector3(enemy.GetHealthPercent(), 1);
        }
        else
        {
            transform.Find("Bar").localScale = new Vector3(0, 1);
        }
        
    }
}
