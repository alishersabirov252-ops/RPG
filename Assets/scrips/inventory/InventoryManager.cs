
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem.Interactions;
using System;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public InventorySlot[] itemSlots;
    public UseItem useItem;



    public int gold;
    public TMP_Text goldText;
    public GameObject LootPrefab;
    public Transform player;
    public static event Action<int> OnExperienceGained;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Start()
    {
        foreach (var slot in itemSlots)
        {
            slot.UpdateUI();
        }
    }

    private void OnEnable()
    {
        Loot.OnItemLooted += AddItem;

    }
    private void OnDisable()
    {
        Loot.OnItemLooted -= AddItem;

    }


    public void AddItem(ItemSo itemSo, int quantity)
    {
        if (itemSo.isGold)
        {
            gold += quantity;
            goldText.text = gold.ToString();
            return;
        }


        if (itemSo.isEXP)
        {
            OnExperienceGained?.Invoke(quantity);
            return;
        }

        foreach (var slot in itemSlots)
        {
            if (slot.itemSo == itemSo && slot.quantity < itemSo.stackSize)
            {
                int avaibleSpace = itemSo.stackSize - slot.quantity;
                int amountToAdd = Mathf.Min(avaibleSpace, quantity);

                slot.quantity += amountToAdd;
                quantity -= amountToAdd;

                slot.UpdateUI();

                if (quantity <= 0)
                    return;
            }
        }
        foreach (var slot in itemSlots)
        {
            if (slot.itemSo == null)
            {
                int amountToAdd = Mathf.Min(itemSo.stackSize - quantity);
                slot.itemSo = itemSo;
                slot.quantity = quantity;
                slot.UpdateUI();
                return;
            }
        }

        if (quantity > 0)
            DropLoot(itemSo, quantity);
        
    }

    public void DropItem(InventorySlot slot)
    {
        DropLoot(slot.itemSo, 1);
        slot.quantity--;
        if (slot.quantity <= 0)
        {
            slot.itemSo = null;
        }
        slot.UpdateUI();
    }

    private void DropLoot(ItemSo itemSo, int quantity)
    {
        Loot loot = Instantiate(LootPrefab, player.position, Quaternion.identity).GetComponent<Loot>();
        loot.Initialize(itemSo, quantity);
        LootPrefab.layer = LayerMask.NameToLayer("Default");
    }

    public void UseItem(InventorySlot slot)
    {
        if (slot.itemSo != null && slot.quantity >= 0)
        {
            useItem.ApplyItemEffects(slot.itemSo);
            slot.quantity--;
            if (slot.quantity <= 0)
            {
                slot.itemSo = null;
            }
            slot.UpdateUI();
        }

    }


    public bool HasItem(ItemSo itemSo)
    {
        foreach (var slot in itemSlots)
        {
            if (slot.itemSo == itemSo && slot.quantity > 0)
                return true;
        }
        return false;
    }



    public int GetItemQuantity(ItemSo itemSo)
    {
        int total = 0;
        foreach (var slot in itemSlots)
        {
            if (slot.itemSo =itemSo)
                total += slot.quantity;
        }

        return total;
    }

}
