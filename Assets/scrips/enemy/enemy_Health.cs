using UnityEngine;

public class enemy_Health : MonoBehaviour
{
    public int exReward = 3;
    public delegate void MonsterDefeated(int exp);
    public static event MonsterDefeated OnMonsterDefeated;
    public int currentHealth;
    public int MaxHealth;

    private void Start()
    {
        currentHealth = MaxHealth;
    }
    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
        if (currentHealth > MaxHealth)
        {
            currentHealth = MaxHealth;
        }
        else if (currentHealth <= 0)
        {
            OnMonsterDefeated(exReward);
            Destroy(gameObject);
        }
    }
}
