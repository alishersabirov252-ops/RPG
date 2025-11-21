
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class ShopSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    public ItemSo itemSo;
    public TMP_Text itemNameText;
    public TMP_Text priceText;
    public Image itemImage;

    [SerializeField] private ShopManager shopManager;
    [SerializeField] private ShopInfo shopInfo;

    public int price;
    public void Initialize(ItemSo newitemSo, int price)
    {
        itemSo = newitemSo;
        itemImage.sprite = itemSo.icon;
        itemNameText.text = itemSo.itemName;
        this.price = price;
        priceText.text = price.ToString();

    }

    public void OnBuyButtonClick()
    {
        shopManager.TryBuyItem(itemSo, price);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemSo != null)
        {
            shopInfo.ShowItemInfo(itemSo);
        }
            
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
            shopInfo.HideItemInfo();
        
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (itemSo != null)
            shopInfo.FollovMouse();
    }
}
