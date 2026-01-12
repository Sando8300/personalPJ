using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Dictionary<string, ItemSlot> slotDictionary = new Dictionary<string, ItemSlot>();
    public CodeTest_InventoryManager inventoryManager;
    public GameObject itemSlotPerfab;
    public Transform parentObject;
    
    public bool isInvenShow = false;
    public GameObject inventoryObject;

    private void Start()
    {
        inventoryManager.InventoryOnChanged += Refresh;
        inventoryObject.SetActive(false);
    }
    public void Refresh(Dictionary<string, CodeTest_Inventory> inventory)
    {

        if (!isInvenShow)
        {
            inventoryObject.SetActive(isInvenShow);
            return;
        }
        foreach (var pair in inventory)
        {
            string key = pair.Key;
            var item = pair.Value;
            if (!slotDictionary.ContainsKey(key))
            {
                var go = Instantiate(itemSlotPerfab, parentObject);
                var slot = go.GetComponent<ItemSlot>();
                slotDictionary.Add(key, slot);
            }

            slotDictionary[key].Getiteminfo(item);
        }

        foreach (var pair in slotDictionary.Keys.ToList())
        {
            if (!inventory.ContainsKey(pair) || inventory[pair].count <= 0)
            {
                Destroy(slotDictionary[pair].gameObject);
                slotDictionary.Remove(pair);           
            }
        }

    }



}
