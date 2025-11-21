using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopInfo : MonoBehaviour
{
    public CanvasGroup infoPanel;
    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;
    [Header("Stat Field")]

    public TMP_Text[] statText;

    private RectTransform infoPanelRect;


    private void Awake()
    {
        infoPanelRect = GetComponent<RectTransform>();
    }


    public void ShowItemInfo(ItemSo itemSo)
    {
        infoPanel.alpha = 1;
        itemNameText.text = itemSo.itemName;
        itemDescriptionText.text = itemSo.itemDescription;

        
        List<string> stats = new List<string>();
        if (itemSo.currentHealth > 0) stats.Add("Health:" + itemSo.currentHealth.ToString());
        if (itemSo.damage > 0) stats.Add("damage:" + itemSo.damage.ToString());
        if (itemSo.speed > 0) stats.Add("speed:" + itemSo.speed.ToString());
        if (itemSo.duration > 0) stats.Add("durantion:" + itemSo.duration.ToString());


        if (stats.Count <= 0)
            return;
        for (int i = 0; i < statText.Length; i++)
        {
            if (i < stats.Count)
            {


                statText[i].text = stats[i];
                statText[i].gameObject.SetActive(true);
            }
            else
            {
                statText[i].gameObject.SetActive(false);
            }
        }

    }

    public void HideItemInfo()
    {
        infoPanel.alpha = 0;
        itemNameText.text = "";
        itemDescriptionText.text = "";

    }

    public void FollovMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 offset = new Vector3(10, -10, 0);


        infoPanelRect.position=mousePosition + offset;
    }

    
}
