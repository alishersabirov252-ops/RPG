
using UnityEngine;

public class fire : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 direction = Vector2.right;
    public float lifeSpawn = 2;
    public float speed;
    public LayerMask enemyLayer;
    public int damage=2;

  



    void Start()
    {
        rb.linearVelocity = direction * speed;
        RotateRaw();
        Destroy(gameObject, lifeSpawn);
    }
    private void RotateRaw()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        if ((enemyLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            
            collision.gameObject.GetComponent<enemy_Health>().ChangeHealth(-damage);
        }
    }



}
