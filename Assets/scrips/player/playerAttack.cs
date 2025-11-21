using UnityEngine;

public class playerAttack : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public Animator anim;
    public float cooldown = 2f;
    private float timer;

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        if (timer <= 0)
        {
            anim.SetBool("isAttacking", true);
            timer = cooldown;
        }
    }

    // Called from animation event
    public void DealDamage()
    {
        if (attackPoint == null)  return;

        Collider2D[] enemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            statsManager.Instance.weaponRange,
            enemyLayer
        );

        foreach (var enemy in enemies)
        {
            var enemyHealth = enemy.GetComponent<enemy_Health>();
            if (enemyHealth != null)
            {
                enemyHealth.ChangeHealth(-statsManager.Instance.damage);
            }

            var knockBack = enemy.GetComponent<enemy_KnockBack>();
            if (knockBack != null)
            {
                knockBack.KnockBack(
                    transform,
                    statsManager.Instance.knockbackForce,
                    statsManager.Instance.knockBackTime,
                    statsManager.Instance.stunTime
                );
            }
        }
    }

    // Called from animation event
    public void FinishAttack()
    {
        anim.SetBool("isAttacking", false);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null || statsManager.Instance==null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, statsManager.Instance.weaponRange);
    }
}
