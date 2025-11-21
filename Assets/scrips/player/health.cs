using UnityEngine;
using TMPro;

public class health : MonoBehaviour
{
    
    public TMP_Text healthText;
    public Animator healthAnim;
    private void Start()
    {
        healthText.text = "hp:" + statsManager.Instance.currentHealth + "/" + statsManager.Instance.maxHealth;
    }

    public void ChangeHealth(int amount)
    {
        statsManager.Instance.currentHealth += amount;
        healthAnim.Play("Animation update");
        healthText.text = "hp:" + statsManager.Instance.currentHealth + "/" + statsManager.Instance.maxHealth;
        if (statsManager.Instance.currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
