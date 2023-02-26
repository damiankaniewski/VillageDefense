using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SimpleCollectibleScript : MonoBehaviour
{
    public enum CollectibleTypes
    {
        NoType,
        Healing,
        HealthAndArmor,
        Armor,
        Coin,
        Meat
    }; // you can replace this with your own labels for the types of collectibles in your game!

    public CollectibleTypes CollectibleType; // this gameObject's type

    public bool rotate; // do you want it to rotate?

    public float rotationSpeed;

    public AudioClip collectSound;

    public GameObject collectEffect;

    public GameManager gameManager;

    public PlayerHealth playerHealth;

    public ArmorBar armorBar;

    public HealthBar healthBar;


    // Use this for initialization
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerHealth = FindObjectOfType<PlayerHealth>();
        armorBar = FindObjectOfType<ArmorBar>();
        healthBar = FindObjectOfType<HealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rotate)
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Collect();
        }
    }

    public void Collect()
    {
        if (collectSound)
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        if (collectEffect)
            Instantiate(collectEffect, transform.position, Quaternion.identity);

        //Below is space to add in your code for what happens based on the collectible type

        if (CollectibleType == CollectibleTypes.NoType)
        {
            //Add in code here;

            Debug.Log("Do NoType Command");
        }

        if (CollectibleType == CollectibleTypes.Healing)
        {
            //Add in code here;
            playerHealth.RestoreHealth(50);
            healthBar.SetHealth(playerHealth.currentHealth);
        }

        if (CollectibleType == CollectibleTypes.HealthAndArmor)
        {
            //Add in code here;
            playerHealth.RestoreHealth(30);
            playerHealth.RestoreArmor(30);
            healthBar.SetHealth(playerHealth.currentHealth);
            armorBar.SetArmor(playerHealth.currentArmor);
        }

        if (CollectibleType == CollectibleTypes.Armor)
        {
            //Add in code here;
            playerHealth.RestoreArmor(50);
            armorBar.SetArmor(playerHealth.currentArmor);
        }

        if (CollectibleType == CollectibleTypes.Coin)
        {
            gameManager.coinScore++;
            Debug.Log("Do NoType Command");
        }

        if (CollectibleType == CollectibleTypes.Meat)
        {
            //Add in code here;

            Debug.Log("Do NoType Command");
        }

        Destroy(transform.parent.gameObject);
        //Destroy(gameObject);
    }
}