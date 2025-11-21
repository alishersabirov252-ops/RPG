using System.Collections;
using UnityEngine;

public class enemy_KnockBack : MonoBehaviour
{
    private Rigidbody2D rb;
    private enemy_movement enemy_Movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy_Movement = GetComponent<enemy_movement>();
    }
    public void KnockBack(Transform forceTransform, float knockbackForce, float knockBackTime, float stunTime)
    {
        enemy_Movement.ChangeState(EnemyState.KnockBack);
        StartCoroutine(stunTimer(knockBackTime, stunTime));
        Vector2 direction = (transform.position - forceTransform.position).normalized;
        rb.linearVelocity = direction * knockbackForce;
        Debug.Log("knockBack applied");


    }
    IEnumerator stunTimer(float knockBackTime, float stunTime)
    {
        yield return new WaitForSeconds(knockBackTime);
        
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(stunTime);
        enemy_Movement.ChangeState(EnemyState.idle);
    }
}
