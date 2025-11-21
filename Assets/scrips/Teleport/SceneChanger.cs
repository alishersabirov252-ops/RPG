using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneLoad;
    public Animator Fadeanim;
    public float fadeTime = .5f;
   




    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Fadeanim.Play("FadeToWhite");
            
            
            StartCoroutine(DelayFade());
        }
    }


    IEnumerator DelayFade()
    {
        yield return new WaitForSeconds(fadeTime);
        
        SceneManager.LoadScene(sceneLoad);
    }
}
