using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordInventory : MonoBehaviour
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
        if (shop.nextSword != 0)
        {
            icons[shop.nextSword-1].SetActive(false);
        }
        
        icons[shop.nextSword].SetActive(true);
    }
}
