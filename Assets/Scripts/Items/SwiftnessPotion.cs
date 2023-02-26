using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SwiftnessPotion : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private TextMeshProUGUI amountText;
    private Image image;
    private int value = 2;
    public int amount = 3;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        image = GetComponent<Image>();
        amountText = GetComponentInChildren<TextMeshProUGUI>();
        amountText.SetText("x" + amount);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
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
        StartCoroutine(Fast());
        amount--;
        amountText.SetText("x" + amount);
    }

    public void Add()
    {
        amount++;
        amountText.SetText("x" + amount);
    }

    private IEnumerator Fast()
    {
        playerMovement.moveSpeed *= 1.25f;
        yield return new WaitForSeconds(3);
        playerMovement.moveSpeed *= 0.8f;
    }
}