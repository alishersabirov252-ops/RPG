using System;
using System.Collections.Generic;
using UnityEngine;


public class ShopManager : MonoBehaviour
{

   
    
    [SerializeField] private ShopSlot[] shopSlots;
    [SerializeField] private InventoryManager inventoryManager;


    


    public void PopulateShopItems(List<ShopItems>shopItems)
    {
        for (int i = 0; i < shopItems.Count && i < shopSlots.Length; i++)
        {
            
            ShopItems shopItem = shopItems[i];
            shopSlots[i].Initialize(shopItem.itemSo, shopItem.price);
            shopSlots[i].gameObject.SetActive(true);
        }

        for (int i = shopItems.Count; i < shopSlots.Length; i++)
        {
            
            shopSlots[i].gameObject.SetActive(false);
        }
    }
    public void TryBuyItem(ItemSo itemSo, int price)
    {
        if (itemSo != null && inventoryManager.gold >= price)
        {
            if (HasSpaceForItem(itemSo))
            {
                inventoryManager.gold -= price;
                inventoryManager.goldText.text = inventoryManager.gold.ToString();
                inventoryManager.AddItem(itemSo, 1);

            }
        }
    }
    public bool HasSpaceForItem(ItemSo itemSo)
    {
        foreach (var slot in inventoryManager.itemSlots)
        {
            if (slot.itemSo == itemSo && slot.quantity < itemSo.stackSize)
                return true;
            else if (slot.itemSo == null)
                return true;
        }
        return false;
    }

    public void SellItem(ItemSo itemSo)
    {
        if (itemSo == null)
            return;

        foreach (var slot in shopSlots)
        {
            if (slot.itemSo == itemSo)
            {
                inventoryManager.gold += slot.price - 1;
                inventoryManager.goldText.text = inventoryManager.gold.ToString();
                return;
            }
        }
    }


}



[System.Serializable]
public class ShopItems
{
    public ItemSo itemSo;
    public int price;
}
