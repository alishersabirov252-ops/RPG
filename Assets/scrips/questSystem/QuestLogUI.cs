using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QuestLogUI : MonoBehaviour
{
    [SerializeField] private QuestManager questManager;
    [SerializeField] private TMP_Text questNametext;
    [SerializeField] private TMP_Text questDescription;
    [SerializeField] private QuestObjectivesSlot[] objectivesSlots;
    [SerializeField] private QuestRewardSlot[] rewardSlots;

    private QuestSo questSo;
    [SerializeField] private QuestSo NoAvailableQuestSo;

    [SerializeField] private QuestLogSlot[] questSlots;
    [SerializeField] private CanvasGroup QuestCanvas;
    [SerializeField] private CanvasGroup AcceptCanvasGroup;
    [SerializeField] private CanvasGroup DeclineCanvasGroup;
    [SerializeField] private CanvasGroup CompleteCanvasGroup;




    private void OnEnable()
    {
        QuestEvents.onQuestOfferRequested += ShowQuestOffer;
        QuestEvents.onQuestTurnInQeruested += ShowQuestTurnIn;
    }

    private void OnDisable()
    {
        QuestEvents.onQuestOfferRequested -= ShowQuestOffer;
        QuestEvents.onQuestTurnInQeruested -= ShowQuestTurnIn;
    }



    #region Show Quest Methods




    public void ShowQuestOffer(QuestSo incomingQuestSo)
    {

        if (questManager.IsQuestAccepted(incomingQuestSo)|| questManager.GetCompleteQuest(incomingQuestSo))
        {
            questSo = NoAvailableQuestSo;
            SetCanvasState(AcceptCanvasGroup, false);
            SetCanvasState(DeclineCanvasGroup, true);
            SetCanvasState(CompleteCanvasGroup, false);
        }
        else
        {
            questSo = incomingQuestSo;
            SetCanvasState(AcceptCanvasGroup, true);
            SetCanvasState(DeclineCanvasGroup, true);
            SetCanvasState(CompleteCanvasGroup, false);

        }

        HandleQuestClick(questSo);
        SetCanvasState(QuestCanvas, true);







    }


    public void ShowQuestTurnIn(QuestSo incomingQuestSo)
    {
        questSo = incomingQuestSo;

        HandleQuestClick(questSo);
        SetCanvasState(CompleteCanvasGroup, true);
        SetCanvasState(AcceptCanvasGroup, false);
        SetCanvasState(DeclineCanvasGroup, false);
        SetCanvasState(QuestCanvas, true);
    }
    #endregion


    #region On Button Clicked Methods



    public void onAcceptQuestClicked()
    {

        QuestEvents.onQuestAccepted?.Invoke(questSo);
        questManager.AcceptQuest(questSo);
        SetCanvasState(CompleteCanvasGroup, false);
        SetCanvasState(AcceptCanvasGroup, false);
        SetCanvasState(DeclineCanvasGroup, false);
        RefreshQuestList();
        HandleQuestClick(NoAvailableQuestSo);

    }


    public void OnDeclineQuestClicked()
    {
        SetCanvasState(QuestCanvas, false);
    }

    public void OnCompleteQuestClicked()
    {
        questManager.CompleteQuest(questSo);
        RefreshQuestList();
        HandleQuestClick(NoAvailableQuestSo);
        SetCanvasState(CompleteCanvasGroup, false);
    }

    #endregion





    private void SetCanvasState(CanvasGroup group, bool activate)
    {
        group.alpha = activate ? 1 : 0;
        group.blocksRaycasts = activate;
        group.interactable = activate;
    }
    
     public void RefreshQuestList()
    {
        List<QuestSo> activeQuests = questManager.GetActiveQuests();

        for (int i = 0; i < questSlots.Length; i++)
        {
            if (i < activeQuests.Count)
            {
                questSlots[i].SetQuest(activeQuests[i]);
            }
            else
            {
                questSlots[i].clearSlot();
            }
        }
    }



    public void HandleQuestClick(QuestSo questSo)
    {
        this.questSo = questSo;
        questNametext.text = questSo.questName;
        questDescription.text = questSo.questDescription;

        DisplayObjectives();
        DisplayRewards();

        
    }




    private void DisplayObjectives()
    {
        for (int i = 0; i < objectivesSlots.Length; i++)
        {
            if (i < questSo.objectives.Count)
            {
                var objective = questSo.objectives[i];
                questManager.UpdateObjectiveProgress(questSo, objective);

                int currentAmount = questManager.GetCurrentAmount(questSo, objective);
                string progress = questManager.GetProgressText(questSo, objective);
                bool isComplete = currentAmount >= objective.requiredamount;
                objectivesSlots[i].gameObject.SetActive(true);
                objectivesSlots[i].RefreshObjectives(objective.description, progress, isComplete);

            }
            else
            {
                objectivesSlots[i].gameObject.SetActive(false);
            }
        }
    }
    


    private void DisplayRewards()
    {
        for (int i = 0; i < rewardSlots.Length; i++)
        {
            if (i < questSo.rewards.Count)
            {

                var reward = questSo.rewards[i];
                rewardSlots[i].DisplayReward(reward.itemSo.icon, reward.quantity);
                rewardSlots[i].gameObject.SetActive(true);

            }
            else
            {
                rewardSlots[i].gameObject.SetActive(false);
            }
        }
    }
}
