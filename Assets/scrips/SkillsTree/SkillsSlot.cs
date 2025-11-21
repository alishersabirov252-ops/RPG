

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;


public class SkillsSlot : MonoBehaviour
{

    public List<SkillsSlot> prerequisiteSkillSlot;
    public SkillsSo skillSo;
    public int currentLevel;
    public bool isUnlocked;
    public Image skillIcon;
    public Button skillButton;
    
    public TMP_Text skillLevelText;

    public static event Action<SkillsSlot> OnAbilityPointsSpent;
    public static event Action<SkillsSlot> OnSkillMaxed;


    private void OnValidate()
    {
        if (skillSo != null && skillLevelText != null)
        {

            UpdateUI();
        }

    }

    public void TryUpgradeSkill()
    {
        if (isUnlocked && currentLevel < skillSo.maxLevel)
        {
            currentLevel++;
            OnAbilityPointsSpent.Invoke(this);

            if (currentLevel >= skillSo.maxLevel)
            {
                OnSkillMaxed.Invoke(this);
            }
            UpdateUI();
        }
        
    }

    public bool CanUnlockSkill()
    {
        foreach (SkillsSlot slot in prerequisiteSkillSlot)
        {
            if (!slot.isUnlocked || slot.currentLevel< slot.skillSo.maxLevel)
            {
                return false;
            }
        }
        return true;
    }

    public void Unlocked()
    {
        isUnlocked = true;
        UpdateUI();

    }
    






    private void UpdateUI()
    {
        skillIcon.sprite = skillSo.skillIcon;
        if (isUnlocked)
        {
            skillButton.interactable = true;

            skillLevelText.text = currentLevel.ToString() + "/" + skillSo.maxLevel.ToString();
            skillIcon.color = Color.white;
        }
        else
        {
            skillButton.interactable = false;

            skillLevelText.text = "Locked";
            skillIcon.color = Color.grey;
        }

    }
}
