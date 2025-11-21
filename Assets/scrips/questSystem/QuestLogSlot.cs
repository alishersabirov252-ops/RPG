using TMPro;
using UnityEngine;

public class QuestLogSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text questName_Text;
    [SerializeField] private TMP_Text questLevelText;


    public QuestSo currentQuest;
    public QuestLogUI questLogUI;

    private void OnValidate()
    {
        if (currentQuest != null)
            SetQuest(currentQuest);
        else
            gameObject.SetActive(false);
        
        
    }


    public void SetQuest(QuestSo questSo)
    {
        currentQuest = questSo;
        questName_Text.text = questSo.questName;
        questLevelText.text = "lv. " + questSo.questLevel.ToString();
        gameObject.SetActive(true);
    }




    public void clearSlot()
    {
        currentQuest = null;
        gameObject.SetActive(false);
    }
    


    public void OnSlotClicled()
    {
        questLogUI.HandleQuestClick(currentQuest);
    }
}
