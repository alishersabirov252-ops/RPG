using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ExpManager : MonoBehaviour
{
    public int level;
    public int currentExp;
    public int expTolevel = 10;
    public float expGrowthMultiplier = 1.2f;
    public Slider expSlider;
    public TMP_Text currentLevelText;

    public static event Action<int> OnLevelUp;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GainExpereince(2);
        }
    }
    private void Start()
    {
        UpdateUI();
    }

    public void GainExpereince(int amount)
    {


        currentExp += amount;
        if (currentExp >= expTolevel)
        {
            LevelUp();
        }
        UpdateUI();

    }
    private void OnEnable()
    {
        enemy_Health.OnMonsterDefeated += GainExpereince;
        InventoryManager.OnExperienceGained += GainExpereince;
    }
    private void OnDisable()
    {
        enemy_Health.OnMonsterDefeated -= GainExpereince;
        InventoryManager.OnExperienceGained -= GainExpereince;
    }
    private void LevelUp()
    {
        level++;
        currentExp -= expTolevel;
        expTolevel = Mathf.RoundToInt(expTolevel * expGrowthMultiplier);
        OnLevelUp?.Invoke(1);
    }

    private void UpdateUI()
    {
        expSlider.maxValue = expTolevel;
        expSlider.value = currentExp;
        currentLevelText.text = "level:" + level;
    }
   
}
