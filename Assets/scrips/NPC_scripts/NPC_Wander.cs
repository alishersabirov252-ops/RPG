using System.Collections;

using UnityEngine;

public class NPC_Wander : MonoBehaviour
{
    [Header("Wander Area")]
    public float wanderWight = 5;
    public float wanderHight = 5;
    public Vector2 startingPosition;
    private Rigidbody2D rb;
    public float speed = 2;
    public Vector2 target;

    public float PausingDuration = 1;
   

    private bool isPausing;
    private Animator anim;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

    }

   
    







    private void Update()
    {
        if (isPausing)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }


        if (Vector2.Distance(transform.position, target) < .1f)
            StartCoroutine(PauseandPickNewDestination());

        Vector2 direction = (target - (Vector2)transform.position).normalized;
        if (direction.x < 0 && transform.localScale.x > 0 || direction.x > 0 && transform.localScale.x < 0)
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        rb.linearVelocity = direction * speed;
    }



    private void OnEnable()
    {
        if (!enabled) return;
        StartCoroutine(PauseandPickNewDestination());
    }

    IEnumerator PauseandPickNewDestination()
    {
        anim.Play("Idle");
        isPausing = true;
        yield return new WaitForSeconds(PausingDuration);
        target = GetRandomTarget();
        isPausing = false;
        anim.Play("walk");
    }








    private Vector2 GetRandomTarget()
    {
        
        float halfwight = wanderWight / 2;
        float halfHeight = wanderHight / 2;
        int edge = Random.Range(0, 4);

        return edge switch
        {
            0 => new Vector2(startingPosition.x - halfwight, Random.Range(startingPosition.y - halfHeight, startingPosition.y + halfHeight)),//left
            1 => new Vector2(startingPosition.x + halfwight, Random.Range(startingPosition.y - halfHeight, startingPosition.y + halfHeight)),//right
            2 => new Vector2(Random.Range(startingPosition.x - halfwight, startingPosition.x + halfwight), startingPosition.y - halfHeight),//bottom

            _ => new Vector2(Random.Range(startingPosition.x - halfwight, startingPosition.x + halfwight), startingPosition.y + halfHeight),//top

        };
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(startingPosition, new Vector3(wanderWight, wanderHight, 0));
    }
}
