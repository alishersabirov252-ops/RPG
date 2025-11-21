using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="QuestSo", menuName ="QuestSo")]
public class QuestSo : ScriptableObject
{
    public string questName;
    [TextArea] public string questDescription;
    public int questLevel;

    public List<QuestObjective> objectives;
    public List<QuestRewards> rewards;



    [System.Serializable]
    public class QuestObjective
    {
        public string description;

        [SerializeField] private Object target;
        public ItemSo targetItem => target as ItemSo;
        public ActorSo targetNPC => target as ActorSo;
        public LocationSo targetLocation => target as LocationSo;

        public int requiredamount;

    }





    [System.Serializable]
    public class QuestRewards
    {
        public ItemSo itemSo;
        public int quantity;
    }
    
}
