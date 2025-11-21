using UnityEngine;

public class enemy_movement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private int facingDirection = 1;
    private Transform player;
    private Animator anim;
    private EnemyState enemystate;
    public float AttackRange = 2;
    public float attackCooldown = 2;
    private float attackCooldownTimer;
    public float playerDetectRange = 5;
    public Transform DetectionPoint;
    public LayerMask playerLayer;

    private Vector3 initialScale;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        initialScale = transform.localScale;
        ChangeState(EnemyState.idle);
    }

    void Update()
    {
        if (enemystate != EnemyState.KnockBack)
        {


            CheckForPlayer();

            if (attackCooldownTimer > 0)
                attackCooldownTimer -= Time.deltaTime;

            if (enemystate == EnemyState.chasing)
            {
                Chase();
            }
            else if (enemystate == EnemyState.Attaking)
            {
                rb.linearVelocity = Vector2.zero;
                // do attacking stuff here
            }
        }
    }

    void Chase()
    {
        if ((player.position.x > transform.position.x && facingDirection == -1) 
        || (player.position.x < transform.position.x && facingDirection == 1))
        {
            Flip();
        }

        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(initialScale.x * facingDirection, initialScale.y, initialScale.z);
    }

    private void CheckForPlayer()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(DetectionPoint.position, playerDetectRange, playerLayer);
        if (hits.Length > 0)
        {
            player = hits[0].transform;

            if (Vector2.Distance(transform.position, player.position) <= AttackRange && attackCooldownTimer <= 0)
            {
                attackCooldownTimer = attackCooldown;
                ChangeState(EnemyState.Attaking);
            }
            else if (Vector2.Distance(transform.position, player.position) > AttackRange && enemystate != EnemyState.Attaking)
            {
                ChangeState(EnemyState.chasing);
            }
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            ChangeState(EnemyState.idle);
        }
    }

   public void ChangeState(EnemyState newState)
    {
        // Exit current state
        if (enemystate == EnemyState.idle)
            anim.SetBool("isIdle", false);
        else if (enemystate == EnemyState.chasing)
            anim.SetBool("isChasing", false);
        else if (enemystate == EnemyState.Attaking)
            anim.SetBool("isAttaking", false);

        // Update state
        enemystate = newState;

        // Enter new state
        if (enemystate == EnemyState.idle)
            anim.SetBool("isIdle", true);
        else if (enemystate == EnemyState.chasing)
            anim.SetBool("isChasing", true);
        else if (enemystate == EnemyState.Attaking)
            anim.SetBool("isAttaking", true);
    }
    private void OnDrawGizmosSelected(){
        Gizmos.color= Color.red;
        Gizmos.DrawWireSphere(DetectionPoint.position, playerDetectRange);
    }
}


public enum EnemyState
{
    idle,
    chasing,
    Attaking,
    KnockBack
}
