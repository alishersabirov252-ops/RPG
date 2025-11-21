using UnityEngine;
using System.Collections.Generic;

public class NPC_TALK : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public Animator interactAnim;
    public DialogueSo currentConversation;
    public List<DialogueSo> conversations;



    private void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }


    private void Start()
    {
        QuestEvents.onQuestAccepted += onQuestAccepted_RemoveOfferings;
    }
    
    private void Destroy()
    {
        QuestEvents.onQuestAccepted -= onQuestAccepted_RemoveOfferings;
    }

    private void OnEnable()
    {
        rb.linearVelocity = Vector2.zero;
        anim.Play("Idle");
        interactAnim.Play("open");
    }

    private void OnDisable()
    {
        interactAnim.Play("close");
    }


    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            

            if (DialogeManager.Instance == null)
            {
                Debug.LogError("DialogeManager.Instance is NULL!");
                return;
            }


            if (DialogeManager.Instance.isDialogueActive)
                DialogeManager.Instance.AdvanceDialogue();

            else
            {
                CheckForNewConversation();
                if (currentConversation != null)

                    DialogeManager.Instance.StartDialogue(currentConversation);
                else
                    Debug.Log("does not have the conversation");

            }


        }
    }
    
  


    private void CheckForNewConversation()
    {
        for (int i = 0; i < conversations.Count; i++)
        {
            var convo = conversations[i];

            if (convo != null && convo.IsConditionMet())
            {
                conversations.RemoveAt(i);
                currentConversation = convo;
                break;
            }

        }
    }
    



    private void onQuestAccepted_RemoveOfferings(QuestSo acceptedQuest)
    {
        for (int i = conversations.Count- 1; i >= 0; i--)
        {
            var convo = conversations[i];
            if (convo == null)
                continue;
            if (convo.offerQuestOnEnd == acceptedQuest)
                conversations.RemoveAt(i);
        }
    }
}
