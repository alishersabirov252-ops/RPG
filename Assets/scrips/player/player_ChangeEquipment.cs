using NUnit.Framework;
using UnityEngine;

public class player_ChangeEquipment : MonoBehaviour
{
    public playerAttack attack;
    public Player_bow bow;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("ChangeEquipment"))
        {
            attack.enabled = !attack.enabled;
            bow.enabled = !bow.enabled;
        }
    }
}
