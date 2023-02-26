using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealingPotion : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private TextMeshProUGUI amountText;
    private Image image;
    private int value = 25;
    public int amount = 3;

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
        image = GetComponent<Image>();
        amountText = GetComponentInChildren<TextMeshProUGUI>();
        amountText.SetText("x" + amount);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (amount > 0)
            {
                Use();
            }
        }

        if (amount == 0)
        {
            image.color = new Color32(75, 75, 75, 100);
        }
        else
        {
            image.color = new Color32(255, 255, 255, 255);
        }
    }

    void Use()
    {
        playerHealth.currentHealth += value;
        amount--;
        amountText.SetText("x" + amount);
    }

    public void Add()
    {
        amount++;
        amountText.SetText("x" + amount);
    }
}