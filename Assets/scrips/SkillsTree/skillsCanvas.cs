using UnityEngine;

public class skillsCanvas : MonoBehaviour
{
    private bool OpenStats = false;
    public CanvasGroup SkillsCanvas;
    private void Update()
    {
        if (Input.GetButtonDown("Panel"))


            if (OpenStats)
            {
                //Time.timeScale = 1;
                SkillsCanvas.alpha = 0;
                OpenStats = false;
                SkillsCanvas.blocksRaycasts = false;
            }


            else
            {
                //Time.timeScale = 0;
                SkillsCanvas.alpha = 1;
                OpenStats = true;
                SkillsCanvas.blocksRaycasts = true;
            }


    } 
}
