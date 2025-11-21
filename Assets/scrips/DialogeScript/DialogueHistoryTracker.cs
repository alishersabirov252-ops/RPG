using UnityEngine;
using System.Collections.Generic;


public class DialogueHistoryTracker : MonoBehaviour
{
   public static DialogueHistoryTracker Instance;
   private readonly HashSet<ActorSo> spokenNPCs=new HashSet<ActorSo>();

   private void Awake()
   {
    if(Instance!= null){
        Destroy(gameObject);
        return;
    }
    Instance=this;

   }


   public void Record(ActorSo actorSo)  
   {

    spokenNPCs.Add(actorSo);
    //Debug.Log("just spoke to"+ actorSo.speaker);
   }


   public bool HasspokenWith(ActorSo actorSo){
    return spokenNPCs.Contains(actorSo);
   }
}
