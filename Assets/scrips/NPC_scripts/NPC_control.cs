
using System.Collections;


using UnityEngine;

public class NPC_control : MonoBehaviour
{

    public Vector2[] controlPoints;
    public float speed = 2;

    public float PauseDirection = 1.5f;
    private bool isPausing;




    private int currentcontrolPoints;
    private  Vector2 target;
    private Rigidbody2D rb;

    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        
        StartCoroutine(SetControlPoint());
        
    }

    // Update is called once per frame
    void Update()
    {

        if (isPausing)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        Vector2 direction = ((Vector3)target - transform.position).normalized;
        if (direction.x < 0 && transform.localScale.x > 0 || direction.x > 0 && transform.localScale.x < 0)
            transform.localScale = new Vector3(transform.localScale.x* -1, transform.localScale.y, transform.localScale.z);
        
        rb.linearVelocity = direction * speed;

        if(Vector2.Distance(transform.position, target)< .1f)
        {
            StartCoroutine(SetControlPoint());
        }

    }
    

    IEnumerator SetControlPoint()
    {
        isPausing = true;
        anim.Play("Idle");

        yield return new WaitForSeconds(PauseDirection);
        currentcontrolPoints = (currentcontrolPoints + 1) % controlPoints.Length;
        target = controlPoints[currentcontrolPoints];
        isPausing = false;
        anim.Play("walk");
        
    }
}
