using UnityEngine;

public class QuestBoard : MonoBehaviour
{
    [SerializeField] private QuestSo questOffer;
    [SerializeField] private QuestSo questToturnin;
    private bool playerInRange;




    private void Update()
    {
        if(playerInRange && Input.GetButtonDown("Interact"))
        {

            bool canTurnIn = questToturnin != null && QuestEvents.isQuestComplete?.Invoke(questToturnin) == true;
            if (canTurnIn)
            {
                QuestEvents.onQuestTurnInQeruested?.Invoke(questToturnin);

            }
            else
            {
                QuestEvents.onQuestOfferRequested?.Invoke(questOffer);
            }
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
