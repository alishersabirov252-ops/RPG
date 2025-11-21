using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<QuestSo, Dictionary<QuestSo.QuestObjective, int>> QuestProgress = new();
    private List<QuestSo> completeQuest = new();



    private void OnEnable()
    {
        QuestEvents.isQuestComplete += IsQuestComplete;
    }
    private void OnDisable()
    {
        QuestEvents.isQuestComplete -= IsQuestComplete;
    }


    #region  Quest Accept Logic 



    #region Quest Complete Logic




    public bool IsQuestComplete(QuestSo questSo)
    {
        if (!QuestProgress.TryGetValue(questSo, out var progressDict))
            return false;

        foreach (var objective in questSo.objectives)
        {
            UpdateObjectiveProgress(questSo, objective);
        }
        foreach (var objective in questSo.objectives)
        {
            if (progressDict[objective] < objective.requiredamount)
                return false;
        }
        return true;
    }
    public void CompleteQuest(QuestSo questSo)
    {
        QuestProgress.Remove(questSo);
        completeQuest.Add(questSo);
        foreach (var reward in questSo.rewards)
        {
            InventoryManager.Instance.AddItem(reward.itemSo, reward.quantity);
        }
    }
    



    public bool GetCompleteQuest(QuestSo questSo)
    {
        return completeQuest.Contains(questSo);
    }
    #endregion
    




    public bool IsQuestAccepted(QuestSo questSo)
    {
        return QuestProgress.ContainsKey(questSo);
    }


    public List<QuestSo> GetActiveQuests()
    {
        return new List<QuestSo>(QuestProgress.Keys);
    }




    public void AcceptQuest(QuestSo questSo)
    {
        QuestProgress[questSo] = new Dictionary<QuestSo.QuestObjective, int>();

        foreach (var objective in questSo.objectives)
        {
            UpdateObjectiveProgress(questSo, objective);
        }
    }
    #endregion



   



    public void UpdateObjectiveProgress(QuestSo questSo, QuestSo.QuestObjective objective)
    {
        if (!QuestProgress.ContainsKey(questSo))
            return;

        var progressDictionary = QuestProgress[questSo];

        int newAmount = 0;

        if (objective.targetItem != null)
            newAmount = InventoryManager.Instance.GetItemQuantity(objective.targetItem);

        else if (objective.targetLocation != null && GameManager.Instance.LocationHistoryTracker.HasVisited(objective.targetLocation))
            newAmount = objective.requiredamount;
        else if (objective.targetNPC != null && GameManager.Instance.dialogueHistoryTracker.HasspokenWith(objective.targetNPC))
            newAmount = objective.requiredamount;


        progressDictionary[objective] = newAmount;
            
        
    }


    public string GetProgressText(QuestSo questSo, QuestSo.QuestObjective objective)
    {
        int currentAmount = GetCurrentAmount(questSo,objective);

        if (currentAmount >= objective.requiredamount)
            return "Complete";

        else if (objective.targetItem != null)
            return $"{currentAmount}/{objective.requiredamount}";
        else
            return "In progress";



    }
    

    public int GetCurrentAmount(QuestSo questSo, QuestSo.QuestObjective objective)
    {
        if (QuestProgress.TryGetValue(questSo, out var objectiveDictionary))
            if (objectiveDictionary.TryGetValue(objective, out int amount))
                return amount;
        return 0;
    }
}
