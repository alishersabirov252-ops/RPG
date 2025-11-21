using UnityEngine;
using System;
using System.Collections.Generic;

public class ShopKeeper : MonoBehaviour
{
    public Animator anim;
    private bool playerinRange;
    private bool isShopOpen;
    public CanvasGroup shopCanvasGroup;
    public ShopManager shopManager;
    [SerializeField] private List<ShopItems> shopItems;
    [SerializeField] private List<ShopItems> shopWeapons;
    [SerializeField] private List<ShopItems> shopArmours;
    [SerializeField] private Camera shopKeeperCamera;
    [SerializeField] private Vector3 cameraoffset= new Vector3(6,1,-1);
     public static event Action<ShopManager, bool> OnShopStateChanged;

    void Update()
    {
        if (playerinRange)
        {
            if (Input.GetButtonDown("Interact"))
            {
                if (!isShopOpen)
                {
                    Debug.Log("jhdcbjdhs");
                    Time.timeScale = 0;
                    isShopOpen = true;
                    OnShopStateChanged?.Invoke(shopManager, true);
                    shopCanvasGroup.alpha = 1;
                    shopCanvasGroup.blocksRaycasts = true;
                    shopCanvasGroup.interactable = true;

                    //shopKeeperCamera.transform.position = transform.position + cameraoffset;
                    shopKeeperCamera.gameObject.SetActive(true);
                    OpenItemShop();
                }
            }
            else if (Input.GetButtonDown("Cancel"))
            {
                Time.timeScale = 1;
                isShopOpen = false;
                OnShopStateChanged?.Invoke(shopManager, false);
                shopCanvasGroup.alpha = 0;
                shopCanvasGroup.blocksRaycasts = false;
                shopCanvasGroup.interactable = false;

                shopKeeperCamera.gameObject.SetActive(false);
            }

        }

    }
    

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
    public void TestButton()
    {
        Debug.Log("button is knocked");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("jhd");
            anim.SetBool("PlayerInRange", true);
            playerinRange = true;
        }
    }
      private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetBool("PlayerInRange", false);
            playerinRange = false;
        }
    }
    
}
