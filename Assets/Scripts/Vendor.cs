using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour
{
    private SphereCollider sphereCollider;
    public GameObject vendorPopGameObject;
    public GameObject shopGameObject;
    public Shop shop;
    private bool isShopOpened;
    private bool isInRange;

    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }


    void Update()
    {
        if (isInRange)
        {
            if (isShopOpened == false)
            {
                vendorPopGameObject.SetActive(true);
            }
            else
            {
                vendorPopGameObject.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.E) && isShopOpened == false)
            {
                shopGameObject.SetActive(true);
                
                isShopOpened = true;
                shop.isShopActive = true;
                
                Cursor.lockState = CursorLockMode.None;
            }
            else if (Input.GetKeyDown(KeyCode.E) && isShopOpened)
            {
                shopGameObject.SetActive(false);
                
                isShopOpened = false;
                shop.isShopActive = false;
                
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        else
        {
            vendorPopGameObject.SetActive(false);
            shopGameObject.SetActive(false);
            
            isShopOpened = false;
            shop.isShopActive = false;
            
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
        }
    }
}