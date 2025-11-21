
using System.Collections;


using UnityEngine;



public class playerMovement : MonoBehaviour
{
    public playerAttack player;
    public fire Fire;
  
    public Rigidbody2D rb;
    public Animator anim;
    public int facingDirection = 1;

    public bool isShooting;
    
    private bool isKnockBack;


    private void Update()
    {
        if (Input.GetButtonDown("Slash") && player.enabled == true )
        {
            player.Attack();
        }
        
    }




    void FixedUpdate()
    {
        if (isShooting == true)
        {
            rb.linearVelocity = Vector2.zero;
        }
        else if (isKnockBack == false)
        {


            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            if (horizontal > 0 && transform.localScale.x < 0 // moving right, but facing left
            || horizontal < 0 && transform.localScale.x > 0 // moving left, but facing right
            )


            {
                Flip();

            }



            anim.SetFloat("horizontal", Mathf.Abs(horizontal));
            anim.SetFloat("vertical", Mathf.Abs(vertical));


            rb.linearVelocity = new UnityEngine.Vector2(horizontal, vertical) * statsManager.Instance.speed;
        }

    }
    void Flip()
    {

        facingDirection *= -1;
        transform.localScale = new UnityEngine.Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
    public void KnockBack(Transform enemy, float force, float stunTime)
    {
        isKnockBack=true;
        Vector2 direction= (transform.position- enemy.position).normalized;
        rb.linearVelocity=direction* force;
        StartCoroutine(KnockBackCounter(stunTime));


    }
   IEnumerator KnockBackCounter(float stunTime)
{
    yield return new WaitForSeconds(stunTime);
    rb.linearVelocity = Vector2.zero;   
    isKnockBack = false;
}   
    
}
