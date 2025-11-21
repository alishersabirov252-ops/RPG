using UnityEngine;

public class enemy : MonoBehaviour
{
    public Transform AttackPoint;
    public float weaponRange;
    public float knockBackForce;
    public float stunTime;
    public LayerMask playerLayer;
    public int damage = 1;

   
    public void Attack()
    {
        Collider2D[] hits= Physics2D.OverlapCircleAll(AttackPoint.position, weaponRange, playerLayer);
        if(hits.Length>0){
            hits[0].GetComponent<health>().ChangeHealth(-damage);
            hits[0].GetComponent<playerMovement>().KnockBack(transform, knockBackForce, stunTime);
        }
    }

}
