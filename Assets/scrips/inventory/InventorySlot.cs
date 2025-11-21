
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public ItemSo itemSo;
    public int quantity;

    public Image ItemImage;
    public TMP_Text quantityText;
    private InventoryManager inventoryManager;
    private ShopManager activeshop;

    private void Start()
    {
        inventoryManager = GetComponentInParent<InventoryManager>();
    }


    private void OnEnable()
    {
        ShopKeeper.OnShopStateChanged += HandleShopStateChanged;
    }
     private void OnDisable()
    {
        ShopKeeper.OnShopStateChanged -= HandleShopStateChanged;
    }


    private void HandleShopStateChanged(ShopManager shopManager,bool isOpen)
    {
        activeshop = isOpen ? shopManager : null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {   


        if (quantity > 0)
        {

            if (eventData.button == PointerEventData.InputButton.Left)
            {

                if (activeshop != null)
                {
                    activeshop.SellItem(itemSo);
                    quantity--;
                    UpdateUI();
                }
                else
                {


                    if (itemSo.currentHealth > 0 && statsManager.Instance.currentHealth >= statsManager.Instance.maxHealth)
                        return;
                    inventoryManager.UseItem(this);
                }
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {

                inventoryManager.DropItem(this);
            }
            



        }
    }

    public void UpdateUI()
    {
        if (quantity <= 0)
            itemSo = null;
        if (itemSo != null)
            {


                ItemImage.sprite = itemSo.icon;
                ItemImage.gameObject.SetActive(true);
                quantityText.text = quantity.ToString();
            }
            else
            {
                ItemImage.gameObject.SetActive(false);
                quantityText.text = "";
            }
    }


   
}
