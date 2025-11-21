using UnityEngine;
using System.Collections;

public class UseItem : MonoBehaviour
{
   public void ApplyItemEffects(ItemSo itemSo){
       // itemSo.currentHealth = 5;
        //itemSo.speed = 1;
        //itemSo.damage=1;
        
    if (itemSo.currentHealth > 0)
        {


            statsManager.Instance.UpdateHealth(itemSo.currentHealth);
        }
        
        

    if (itemSo.maxHealth > 0)
            statsManager.Instance.UpdateMaxHealth(itemSo.maxHealth);

    

    if(itemSo.speed>0)
        statsManager.Instance.UpdateSpeed(itemSo.speed);
        
    


    if(itemSo.duration>0)
    StartCoroutine(EffectTimer(itemSo, itemSo.duration));
   }



   private IEnumerator EffectTimer(ItemSo itemSo, float duration ){
    yield return new WaitForSeconds(duration);

    if(itemSo.currentHealth>0)
    
        statsManager.Instance.UpdateHealth(-itemSo.currentHealth);
        

    if(itemSo.maxHealth>0)
        statsManager.Instance.UpdateMaxHealth(-itemSo.maxHealth);
     

    if(itemSo.speed>0)
        statsManager.Instance.UpdateSpeed(-itemSo.speed);
    
   }
}
