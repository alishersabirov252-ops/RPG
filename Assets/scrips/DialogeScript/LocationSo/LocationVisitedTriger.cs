using UnityEngine;

public class LocationVisitedTriger : MonoBehaviour
{
    [SerializeField] private LocationSo locationVisited;
    [SerializeField] private bool destroyOnTouch=true;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            LocationHistoryTracker.Instance.RecordLocation(locationVisited);
            if (destroyOnTouch)
            {
                Destroy(gameObject);
            }
            
        }
    }
}
