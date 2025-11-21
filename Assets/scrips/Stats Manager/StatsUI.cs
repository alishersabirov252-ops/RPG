using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StatsUI : MonoBehaviour
{
    public GameObject[] statsSlots;
    public CanvasGroup StatsCanvas;
    private bool OpenStats = false;
    private void Start()
    {
        UpdateAll();
    }
    private void Update()
    {
        if (Input.GetButtonDown("ToggleStats"))


            if (OpenStats)
            {
                Time.timeScale = 1;
                UpdateAll();
                StatsCanvas.alpha = 0;
                OpenStats = false;
            }


            else
            {
                Time.timeScale = 0;
                StatsCanvas.alpha = 1;
                OpenStats = true;
            }
                
            
    }
    public void UpdateDamage()
    {
        statsSlots[0].GetComponentInChildren<TMP_Text>().text = "Damage:" + statsManager.Instance.damage;
    }
    public void UpdateSpeed()
    {
        statsSlots[1].GetComponentInChildren<TMP_Text>().text = "Speed:" + statsManager.Instance.speed;
    }

    public void UpdateAll()
    {
        UpdateDamage();
        UpdateSpeed();
        
    }
}
