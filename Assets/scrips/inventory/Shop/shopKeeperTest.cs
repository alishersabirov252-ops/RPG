using UnityEngine;
using System;
using System.Collections.Generic;

public class ShopKeeperTest : MonoBehaviour
{
    public Animator anim;
 
    public ShopManager shopManager;
    [SerializeField] private List<ShopItems> shopItems;
    [SerializeField] private List<ShopItems> shopWeapons;
    [SerializeField] private List<ShopItems> shopArmours;
 
    

    public void OpenItemShop()
    {
        shopManager.PopulateShopItems(shopItems);
    }


    public void OpenWeaponShop()
    {
         shopManager.PopulateShopItems(shopWeapons);
    }

    
    public void OpenArmourShop()
    {
         shopManager.PopulateShopItems(shopArmours);
    }


    
}
