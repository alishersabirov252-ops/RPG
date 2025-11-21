using TMPro;
using UnityEngine;

public class QuestObjectivesSlot : MonoBehaviour
{
    [SerializeField] private TMP_Text objectivesText;
    [SerializeField] private TMP_Text TrackingText;


    public void RefreshObjectives(string description, string progressText, bool isComplete)
    {
        objectivesText.text = description;
        TrackingText.text = progressText;
        Color color = isComplete ? Color.grey : Color.white;
        objectivesText.color = color;
        TrackingText.color = color;
    }

}
