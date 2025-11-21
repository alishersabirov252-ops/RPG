using UnityEngine;

[CreateAssetMenu(fileName="new item")]
public class ItemSo : ScriptableObject
{
    public string itemName;
    [TextArea] public string itemDescription;
    public Sprite icon;

    public bool isGold;
    public bool isEXP;
    public int stackSize = 3;
    [Header("Stats")]
    public int currentHealth; 
    public int maxHealth;
    public int speed;
    public int damage;
    


    [Header("Temprorary items")]
    public float duration;
    
}
