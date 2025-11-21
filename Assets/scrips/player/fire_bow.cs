
using UnityEngine;

public class fire_bow : MonoBehaviour
{
    public Transform LaunchPoint;
    public GameObject firePrefab;
    private Vector2 aimDirection = Vector2.right;
    public float shootcooldown = .5f;
    private float shootTimer;



    void Update()
    {
        shootTimer -= Time.deltaTime;

        HandleAiming();
        if (Input.GetButtonDown("Fire")&& shootTimer<=0)
        {
            


            Shoot();
        }
    }


    private void HandleAiming()
    {
        float horiznotal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horiznotal != 0 || vertical != 0)
        {
            aimDirection =new Vector2(horiznotal, vertical).normalized;
        } 
        

    }

    public void Shoot()
    {
        fire Fire = Instantiate(firePrefab, LaunchPoint.position, Quaternion.identity).GetComponent<fire>();
        Fire.direction = aimDirection;
        shootTimer = shootcooldown;
    }
  
}
