using NUnit.Framework;
using UnityEngine;

public class skillManager : MonoBehaviour
{
    
    public fire_bow Fire;
    private void OnEnable()
    {
        SkillsSlot.OnAbilityPointsSpent += HandleAbilityPointSpent;
    }
    private void Disable()
    {
        SkillsSlot.OnAbilityPointsSpent -= HandleAbilityPointSpent;
    }

    private void HandleAbilityPointSpent(SkillsSlot slot)
    {
        string skillName = slot.skillSo.SkillName;
        switch (skillName)
        {
            case "Health Boost":
                statsManager.Instance.UpdateMaxHealth(1);
                break;
            
            case "Fire":
                Fire.enabled = true;
                break;
            default:
                Debug.LogWarning("Unknown skill:" + skillName);
                break;

        }
        
    }
}

