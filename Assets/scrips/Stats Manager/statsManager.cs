using TMPro;
using UnityEngine;

public class statsManager : MonoBehaviour
{
    public static statsManager Instance;
    public StatsUI statsUI;
        

    public TMP_Text healthText;
    [Header("Combat Stats")]
    public float weaponRange;
    public int damage;
    public float knockBackTime;
    public float knockbackForce;

    public int stunTime;
    [Header("Movement Stats")]
    public int speed;
    [Header("Health stats")]
    public int maxHealth;
    public int currentHealth; // !!!


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);


    }
    public void UpdateMaxHealth(int amount)
    {
        

        maxHealth += amount;
        healthText.text = "HP:" + currentHealth + "/" + maxHealth;
    }

    public void UpdateHealth(int amount)
    {


        currentHealth += amount;
        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;
               
        healthText.text = "HP:" + currentHealth + "/" + maxHealth;
    }

    public void UpdateSpeed(int amount)
    {
        speed += amount;
        statsUI.UpdateAll();
        
    }


}
