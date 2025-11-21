using System;
using UnityEngine;

public static class QuestEvents
{
    public static Action<QuestSo> onQuestOfferRequested;
    public static Action<QuestSo> onQuestTurnInQeruested;
    public static Action<QuestSo> onQuestAccepted;

    public static Func<QuestSo, bool> isQuestComplete;



 
}
