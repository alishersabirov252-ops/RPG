using System.Diagnostics;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public enum NPCState { Default, Idle, Control, Wander, Talk }
    public NPCState currentState = NPCState.Control;
    public NPCState defaultState;
    public NPC_control control;
    public NPC_Wander wander;
    public NPC_TALK talk;

    void Start()
    {
        defaultState = currentState;
        SwitchState(currentState);


    }

    public void SwitchState(NPCState newState)
    {
        currentState = newState;
        control.enabled = newState == NPCState.Control;
        wander.enabled = newState == NPCState.Wander;
        talk.enabled = newState == NPCState.Talk;
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SwitchState(NPCState.Talk);
        }
    }



    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SwitchState(defaultState);
        }
    }
}
