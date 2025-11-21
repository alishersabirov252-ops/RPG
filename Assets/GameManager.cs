using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Persistent Object")]
    public GameObject[] persistentObjects;
    public LocationHistoryTracker LocationHistoryTracker;
    public DialogueHistoryTracker dialogueHistoryTracker;
    
   

    public void Awake()
    {
        if (Instance != null)
        {

            CleanUpandDestroy();
            
            return;
        }

        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            MarkpersistentObjects();
        }
    }



    private void MarkpersistentObjects()
    {
        foreach (GameObject obj in persistentObjects)
        {
            if (obj != null)
            {
                DontDestroyOnLoad(obj);
            }

        }
    }
    

    private void CleanUpandDestroy()
    {
        foreach (GameObject obj in persistentObjects)
        {
            Destroy(obj);
        }
        Destroy(gameObject);
    }
}
