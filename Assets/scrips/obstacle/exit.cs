using UnityEngine;

public class exit : MonoBehaviour
{

    public Collider2D[] mauntainColliders;
    public Collider2D[] boundaryColliders;
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("i left");
            foreach (Collider2D mountain in mauntainColliders)
                mountain.enabled = true;

            foreach (Collider2D boundary in boundaryColliders)
                boundary.enabled = false;

            collision.GetComponent<SpriteRenderer>().sortingOrder = 5;
        }
    }
}



