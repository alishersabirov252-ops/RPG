using UnityEngine;
[System.Serializable]
public class DialogueLine
{
    public ActorSo speaker;
     public string Text;
}


[CreateAssetMenu(fileName = " new Dialoge", menuName = "Dialoge/Dialoge So")]
public class DialogueSo : ScriptableObject
{

    public DialogueLine[] lines;
    public DialogueOption[] options;

    [Header("Quest offer(optional)")]
    public QuestSo offerQuestOnEnd;

    [Header("conditional Requirements(Optinal)")]
    public ActorSo[] requiredNPC;
    public LocationSo[] requiredLocation;
    public ItemSo[] requiredItems;




    public bool IsConditionMet(){
        if (requiredNPC.Length > 0)
        {
            foreach (var npc in requiredNPC)
            {
                if (!DialogueHistoryTracker.Instance.HasspokenWith(npc))
                    return false;

            }


        }


        if (requiredLocation.Length > 0)
        {
            foreach (var location in requiredLocation)
            {
                if (!LocationHistoryTracker.Instance.HasVisited(location))
                    return false;
            }
        }


        if (requiredItems.Length > 0)
        {
            foreach(var item in requiredItems)
            {
                if (!InventoryManager.Instance.HasItem(item))
                    return false;
            }
        }
        return true;
    }



}

[System.Serializable]
public class DialogueOption
{
    public string OptionText;
    public DialogueSo nextDialogue;
    public QuestSo offerQuest;
}



