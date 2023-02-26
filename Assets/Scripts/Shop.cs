using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [HideInInspector] public bool isShopActive;

    private HealingPotion healingPotion;
    private ObsidianPotion obsidianPotion;
    private SwiftnessPotion swiftnessPotion;
    private GameManager gameManager;

    public GameObject[] armor;
    public GameObject[] sword;
    public int nextArmor = 0;
    public int nextSword = 0;

    public GameObject upgradeArmorButton;
    public GameObject upgradeSwordButton;

    private PlayerHealth playerHealth;
    private PlayerCombat playerCombat;

    public TextMeshProUGUI ArmorUpgradeText;
    public TextMeshProUGUI SwordUpgradeText;

    public TextMeshProUGUI ArmorUpgradeCashText;
    private int armorUpgradeCash = 50;
    public TextMeshProUGUI SwordUpgradeCashText;
    private int swordUpgradeCash = 40;

    private void Start()
    {
        healingPotion = FindObjectOfType<HealingPotion>();
        obsidianPotion = FindObjectOfType<ObsidianPotion>();
        swiftnessPotion = FindObjectOfType<SwiftnessPotion>();
        gameManager = FindObjectOfType<GameManager>();

        playerHealth = FindObjectOfType<PlayerHealth>();
        playerCombat = FindObjectOfType<PlayerCombat>();

        ArmorUpgradeText.SetText((playerHealth.maxArmor + (50 + playerHealth.maxArmor / 10)).ToString());
        ArmorUpgradeCashText.SetText((armorUpgradeCash.ToString()));

        SwordUpgradeText.SetText(playerCombat.attackPower.ToString());
        SwordUpgradeCashText.SetText(swordUpgradeCash.ToString());
    }

    public void AddHP() //healingPotion
    {
        if (gameManager.coinScore >= 10)
        {
            healingPotion.Add();
            gameManager.coinScore -= 10;
        }
    }

    public void AddOP() //obsidianPotion
    {
        if (gameManager.coinScore >= 10)
        {
            obsidianPotion.Add();
            gameManager.coinScore -= 10;
        }
    }

    public void AddSP() //swiftnessPotion
    {
        if (gameManager.coinScore >= 15)
        {
            swiftnessPotion.Add();
            gameManager.coinScore -= 15;
        }
    }

    public void UpgradeArmor()
    {
        if (nextArmor < 6 && gameManager.coinScore >= armorUpgradeCash)
        {
            armor[nextArmor].SetActive(false);

            playerHealth.maxArmor += (50 + playerHealth.maxArmor / 10);
            gameManager.coinScore -= armorUpgradeCash;
            armorUpgradeCash += (50 + armorUpgradeCash / 8);
            nextArmor++;
            ArmorUpgradeText.SetText((playerHealth.maxArmor + (50 + playerHealth.maxArmor / 10)).ToString());
            ArmorUpgradeCashText.SetText((armorUpgradeCash.ToString()));

            playerHealth.currentArmor = playerHealth.maxArmor;

            if (nextArmor < 6)
            {
                armor[nextArmor].SetActive(true);
            }
        }

        if (nextArmor == 6)
        {
            upgradeArmorButton.SetActive(false);
        }
    }

    public void UpgradeSword()
    {
        if (nextSword < 8 && gameManager.coinScore >= swordUpgradeCash)
        {
            sword[nextSword].SetActive(false);

            playerCombat.attackPower += (int)(0.3f * playerCombat.attackPower);
            gameManager.coinScore -= swordUpgradeCash;
            swordUpgradeCash += (50 + swordUpgradeCash / 8);
            nextSword++;
            SwordUpgradeText.SetText(((int)(playerCombat.attackPower + (0.3f * playerCombat.attackPower))).ToString());
            SwordUpgradeCashText.SetText((swordUpgradeCash.ToString()));

            if (nextSword < 8)
            {
                sword[nextSword].SetActive(true);
            }
        }

        if (nextSword == 8)
        {
            upgradeSwordButton.SetActive(false);
        }
    }
}