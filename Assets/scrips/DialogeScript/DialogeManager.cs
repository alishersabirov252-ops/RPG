using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DialogeManager : MonoBehaviour
{
    public static DialogeManager Instance;

    [Header("UI Reference")]
    public CanvasGroup canvasGroup;
    public Image portrait;
    public TMP_Text ActorName;
    public TMP_Text dialogeText;
    public bool isDialogueActive;
    public Button[] choiceButtons;

    private DialogueSo currentDialoge;
    private int dialogeIndex;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

       
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        if (choiceButtons != null)
        {
            for (int i = 0; i < choiceButtons.Length; i++)
            {
                if (choiceButtons[i] != null)
                {
                    choiceButtons[i].gameObject.SetActive(false);
                    choiceButtons[i].onClick.RemoveAllListeners();
                }
               
            }
        }
    }

    public void StartDialogue(DialogueSo dialogueSo)
    {
       

        if (dialogueSo == null)
        {
            
            EndDialogue();
            return;
        }

        if (dialogueSo.lines == null)
        {
            
            EndDialogue();
            return;
        }

        if (dialogueSo.lines.Length == 0)
        {
            
            EndDialogue();
            return;
        }

        
      

        currentDialoge = dialogueSo;
        dialogeIndex = 0;
        isDialogueActive = true;
        ShowDialoge();
    }

    private void ShowDialoge()
    {
        if (currentDialoge == null)
        {
            
            EndDialogue();
            return;
        }

        if (currentDialoge.lines == null)
        {
           
            EndDialogue();
            return;
        }

        if (dialogeIndex < 0 || dialogeIndex >= currentDialoge.lines.Length)
        {
            
            ShowChoices();
            return;
        }

        DialogueLine line = currentDialoge.lines[dialogeIndex];
        if (line == null)
        {
            
            dialogeIndex++;
            ShowDialoge();
            return;
        }

        // Record history if available
        if (DialogueHistoryTracker.Instance != null)
        {
            try
            {
                DialogueHistoryTracker.Instance.Record(line.speaker);
            }
            catch (System.Exception ex)
            {
                Debug.LogWarning("[DialogeManager] Exception when recording history: " + ex.Message);
            }
        }
        else
        {
            Debug.Log("[DialogeManager] DialogueHistoryTracker.Instance is NULL (ok if not used).");
        }

       
        if (portrait != null)
        {
            if (line.speaker != null && line.speaker.portrait != null)
                portrait.sprite = line.speaker.portrait;
            else
                portrait.sprite = null; 
        }

        if (ActorName != null)
            ActorName.text = (line.speaker != null) ? (line.speaker.ActorName ?? "") : "";

        if (dialogeText != null)
            dialogeText.text = line.Text ?? "";

        
        Time.timeScale = 0f;
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        dialogeIndex++;
    }

    public void AdvanceDialogue()
    {
        if (currentDialoge == null)
        {
            
            return;
        }

        if (dialogeIndex < currentDialoge.lines.Length)
            ShowDialoge();
        else
            ShowChoices();
    }

    private void ShowChoices()
    {
       

        ClearChoices();

        if (currentDialoge == null)
        {
            
            EndDialogue();
            return;
        }

        var options = currentDialoge.options;

        if (options == null || options.Length == 0)
        {
           
            if (currentDialoge.offerQuestOnEnd != null)
            {
                EndDialogue();
                QuestEvents.onQuestOfferRequested?.Invoke(currentDialoge.offerQuestOnEnd);
            }
            else
            {
                // show fallback End button if exists
                if (choiceButtons != null && choiceButtons.Length > 0 && choiceButtons[0] != null)
                {
                    var btn = choiceButtons[0];
                    var tmp = btn.GetComponentInChildren<TMP_Text>();
                    if (tmp != null) tmp.text = "End";
                    btn.onClick.AddListener(EndDialogue);
                    btn.gameObject.SetActive(true);

                    if (EventSystem.current != null)
                        EventSystem.current.SetSelectedGameObject(btn.gameObject);
                   
                }
                else
                {
                    
                    EndDialogue();
                }
            }

            return;
        }

       
        if (choiceButtons == null || choiceButtons.Length == 0)
        {
            
            EndDialogue();
            return;
        }

        int count = Mathf.Min(options.Length, choiceButtons.Length);
        for (int i = 0; i < count; i++)
        {
            var option = options[i];
            var btn = choiceButtons[i];
            if (btn == null)
            {
                
                continue;
            }

            var tmp = btn.GetComponentInChildren<TMP_Text>();
            if (tmp != null) tmp.text = option != null ? (option.OptionText ?? "Option") : "Option (null)";

            btn.gameObject.SetActive(true);

            // Capture option to avoid closure issue
            var capturedOption = option;
            btn.onClick.AddListener(() =>
            {
               
                ChooseOption(capturedOption != null ? capturedOption.nextDialogue : null);
            });
        }

        // Select first visible button
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (choiceButtons[i] != null && choiceButtons[i].gameObject.activeSelf)
            {
                if (EventSystem.current != null)
                    EventSystem.current.SetSelectedGameObject(choiceButtons[i].gameObject);
                break;
            }
        }
    }

    private void EndDialogue()
    {
       
        dialogeIndex = 0;
        isDialogueActive = false;
        ClearChoices();
        Time.timeScale = 1f;
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }

    private void ChooseOption(DialogueSo dialogueSo)
    {
        

        if (dialogueSo == null)
        {
            EndDialogue();
            return;
        }

        ClearChoices();
        StartDialogue(dialogueSo);
    }

    private void ClearChoices()
    {
        if (choiceButtons == null) return;
        foreach (var button in choiceButtons)
        {
            if (button == null) continue;
            button.onClick.RemoveAllListeners();
            button.gameObject.SetActive(false);
        }
    }





    private void OnEnable()
{
    SceneManager.sceneLoaded += OnSceneLoaded;
}

private void OnDisable()
{
    SceneManager.sceneLoaded -= OnSceneLoaded;
}

private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    FindDialogeCanvas();
}

private void FindDialogeCanvas()
{
    if (canvasGroup == null)
    {
        GameObject dc = GameObject.Find("DialogeCanvas");
        if (dc != null)
        {
            canvasGroup = dc.GetComponent<CanvasGroup>();
            Debug.Log("DialogueCanvas found and linked!");
        }
        else
        {
            Debug.LogWarning("DialogueCanvas NOT FOUND in this scene!");
        }
    }
}
}
