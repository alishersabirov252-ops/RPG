using TMPro;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public SkillsSlot[] skillsSlots;
    public TMP_Text pointsText;
    public int avaiblePoints;


    private void OnEnable()
    {
        SkillsSlot.OnAbilityPointsSpent += HandheAbilityPointsSpent;
        SkillsSlot.OnSkillMaxed += HandleSkillMaxed;
        ExpManager.OnLevelUp += UpdateAbilityPoints;
    }
    private void OnDisable()
    {
        SkillsSlot.OnAbilityPointsSpent -= HandheAbilityPointsSpent;
        SkillsSlot.OnSkillMaxed -= HandleSkillMaxed;
        ExpManager.OnLevelUp -= UpdateAbilityPoints;
        
    }

    private void Start()
    {
        foreach (SkillsSlot slot in skillsSlots)
        {
            slot.skillButton.onClick.AddListener(() => CheckAvaiblePoints(slot));
        }
        UpdateAbilityPoints(0);
    }

    private void CheckAvaiblePoints(SkillsSlot slot)
    {
        if (avaiblePoints > 0)
        {
            slot.TryUpgradeSkill();
        }
    }


    private void HandheAbilityPointsSpent(SkillsSlot skillsSlot)
    {
        if (avaiblePoints > 0)
        {
            UpdateAbilityPoints(-1);
        }
    }

    private void HandleSkillMaxed(SkillsSlot skillsSlot)
    {
        foreach (SkillsSlot slot in skillsSlots)
        {
            if (!slot.isUnlocked && slot.CanUnlockSkill())
            {


                slot.Unlocked();
            }
        }
    }



    public void UpdateAbilityPoints(int amount)
    {
        avaiblePoints += amount;
        pointsText.text = "Points" + avaiblePoints;
    }
}
