using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CodeTest_InventoryManager : MonoBehaviour
{

    public Dictionary<string, CodeTest_Inventory> inventory = new Dictionary<string, CodeTest_Inventory>();


    public ItemData itemData;
    public event Action<Dictionary<string, CodeTest_Inventory>> InventoryOnChanged;
    public Button myButton;
    public CodeTest_UIManager uiManager;
    public string equipId;



    private void Start()
    {
        StartCoroutine(RemoveBrokenItem());


    }



    public void Additem(string _itemid, ItemData _itemData)
    {
        if (_itemData.isStack == true)
        {

            var exist = inventory.Values.FirstOrDefault(item => item.itemDetail_Id.ToString() == _itemid);


            if (exist != null)
            {
                inventory[_itemid].count++;
                if (_itemData.itemid == 3)
                {
                    int mul = UnityEngine.Random.Range(30, 91);
                    inventory[_itemid].count += mul;
                }
            }
            else
            {
                inventory.Add(_itemid, new CodeTest_Inventory(_itemData));
                if (_itemData.itemid == 3)
                {
                    int mul = UnityEngine.Random.Range(30, 91);
                    inventory[_itemid].count *= mul;
                }
            }
        }
        else
        {
            string uniqueId = Guid.NewGuid().ToString();
            inventory.Add(uniqueId, new CodeTest_Inventory(_itemData));
            inventory[uniqueId].uniqueId = uniqueId;



        }

        InventoryOnChanged?.Invoke(inventory);
        StartCoroutine(uiManager.GetitemNotice(_itemData));

    }


    public void UseItem(string key)
    {
        if (!inventory[key].isStack)
        {
            uiManager.statusText.text = "사용할 수 없는 아이템입니다.";
            GameManagerScript.instance.audioManager.source.PlayOneShot(GameManagerScript.instance.audioManager.Audio["InvenErrorSFX"]);
            StartCoroutine(uiManager.TextBlink());
            return;
        }
        inventory[key].count--;
        var effect = ItemEffectFactory.GetEffect(inventory[key].itemDataSO.itemtype);
        effect?.Apply(inventory[key]);
        InventoryOnChanged?.Invoke(inventory);
    }

    public IEnumerator RemoveBrokenItem()

    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            foreach (var key in inventory.Keys.ToList())
            {
                //inventory[key].durability -= 1;

              /*  if (inventory[key].durability <= 0)
                {
                    if (inventory[key].count <= 1)
                    {
                        inventory.Remove(key);
                    }
                    else
                    {
                        inventory[key].count--;
                        inventory[key].durability = inventory[key].max_Durability;
                    }
                }*/
                if (inventory[key].count <= 0)
                {
                    inventory.Remove(key);
                }
                InventoryOnChanged?.Invoke(inventory);

            }
        }
    }

  
    public void ReduceDur()
    {
        foreach (var item in inventory)
        {

            if (item.Value.itemDetail_Id.ToString() == equipId)
            {
                Debug.Log(item.Value.durability);
                item.Value.durability -= 20;
                InventoryOnChanged?.Invoke(inventory);
                Debug.Log(item.Value.durability);
                return;
            }
        }
    }
}






