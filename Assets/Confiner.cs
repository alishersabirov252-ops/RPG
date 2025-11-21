using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

public class Confiner : MonoBehaviour
{

    

    private void OnEnable()
    {
        SceneManager.sceneLoaded += onSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= onSceneLoaded;
    }

    private void onSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CinemachineConfiner2D confiner = GetComponent<CinemachineConfiner2D>();
        //confiner.BoundingShape2D = GameObject.FindWithTag("Confiner").GetComponent<PolygonCollider2D>();
        
    }

   
}
