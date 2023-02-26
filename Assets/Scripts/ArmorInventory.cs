using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorInventory : MonoBehaviour
{
    public GameObject[] icons;
    private Shop shop;
    private int iconIndex = 0;
    
    private void Start()
    {
        shop = FindObjectOfType<Shop>();
        icons[iconIndex].SetActive(true);
    }

    private void Update()
    {
        if (shop.nextArmor != 0)
        {
            icons[shop.nextArmor-1].SetActive(false);
        }
        
        icons[shop.nextArmor].SetActive(true);
    }
}
