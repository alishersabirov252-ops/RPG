using UnityEngine;
using System;

public class Loot : MonoBehaviour
{
    public ItemSo itemSo;
    public SpriteRenderer sr;
    public Animator anim;
    public bool canbePickedUp = true;


    public int quantity;
    public static event Action<ItemSo, int> OnItemLooted;


    private void OnValidate()
    {
        if (itemSo == null)
            return;


        UpdateAppearance();
    }


    public void Initialize(ItemSo itemSo, int quantity)
    {
        this.itemSo = itemSo;
        this.quantity = quantity;
        canbePickedUp = false;
        
        UpdateAppearance();

    }
    private void UpdateAppearance()
    {
        sr.sprite = itemSo.icon;
        this.name = itemSo.itemName;


    }



    private void OnTriggerEnter2D(Collider2D collision)

    {
        if (collision.CompareTag("Player") && canbePickedUp == true)
        {
            anim.Play("Animation");
            OnItemLooted?.Invoke(itemSo, quantity);
            Destroy(gameObject, .5f);
        }

    }

   private void OnTriggerExit2D(Collider2D collision)
    {

        
        if (collision.CompareTag("Player"))
        {
            
            canbePickedUp = true;
        }

    }

 
}
