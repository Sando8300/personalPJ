using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("인벤토리")]
    public Dictionary<string, ItemSlot> slotDictionary = new Dictionary<string, ItemSlot>();
    public CodeTest_InventoryManager inventoryManager;
    public GameObject itemSlotPerfab;
    public Transform[] parentObject; //itemList스크롤뷰의 Contents 오브젝트를 가진 배열

    public bool isInvenShow = false;
    public GameObject inventoryObject;

   

    [Header("로드아웃")]
    public GameObject loadOutObject;
    public Image weaponIMG;
    public Image armorIMG;
    StringBuilder WPsb;
    StringBuilder ARsb;
    public TextMeshProUGUI effectText;
    public TextMeshProUGUI statText;
    

    private void Start()
    {
        WPsb = new StringBuilder();
        ARsb = new StringBuilder();
        inventoryManager.InventoryOnChanged += Refresh;
        inventoryObject.SetActive(false);
        loadOutObject.SetActive(false);
        // moveTOinven.onClick.AddListener(() => {); });
    }
    public void Refresh(Dictionary<string, CodeTest_Inventory> inventory)
    {

        /* if (!isInvenShow)
         {
             Debug.Log("리턴됨");
             inventoryObject.SetActive(isInvenShow);
             return;
         }*/
        foreach (var pair in inventory)
        {
            string key = pair.Key;
            var item = pair.Value;
            if (!slotDictionary.ContainsKey(key))
            {
                var go = Instantiate(itemSlotPerfab, parentObject[inventory[key].itemid - 1]);
                var slot = go.GetComponent<ItemSlot>();
                slotDictionary.Add(key, slot);
                Debug.Log("슬롯생성됨.");
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

    public void LoadOut(string Key, string type)
    {
        //loadOutObject.SetActive(!isInvenShow);
        //inventoryObject.SetActive(false);

        switch (type)
        {
            case "Weapon":
                Debug.Log("로드아웃 UI리프레쉬 실행");
                if (!GameManagerScript.instance.inventoryManager.inventory[Key].isEquipped)
                {
                    Debug.Log("로드아웃 찐 UI리프레쉬");
                    weaponIMG.sprite = null;
                    WPsb.Clear();

                }
                else
                {
                    weaponIMG.sprite = inventoryManager.inventory[Key].img;
                    WeaponData weaponData = inventoryManager.inventory[Key].itemDataSO as WeaponData;
                    WPsb.Append($"무기 정보: {inventoryManager.inventory[Key].name}\n{inventoryManager.inventory[Key].desc}\n");
                    WPsb.Append($"피해: {weaponData.damage}\n연사력: {weaponData.fireRate}\n탄창크기: {weaponData.magazineSize}\n");
                }
                break;

            case "Armor":
                if (!GameManagerScript.instance.inventoryManager.inventory[Key].isEquipped)
                {
                    armorIMG.sprite = null;
                    ARsb.Clear();


                }
                else
                {
                    armorIMG.sprite = inventoryManager.inventory[Key].img;
                    ArmorData armorData = inventoryManager.inventory[Key].itemDataSO as ArmorData;
                    ARsb.Append($"방어구 정보: {inventoryManager.inventory[Key].name}\n{inventoryManager.inventory[Key].desc}\n");
                    ARsb.Append($"피해 저항: {armorData.resist}\n");
                }
                break;

            default:
                inventoryManager.uiManager.statusText.text = "장착할 수 없는 아이템입니다.";
                inventoryManager.uiManager.TextBlink();
                break;
        }



        effectText.text = $"{WPsb.ToString()}{ARsb.ToString()}";
        statText.text = $"현재 총 공격력 및 피해 저항\n 피해 : {GameManagerScript.instance.damage}\n 피해 저항 : {GameManagerScript.instance.currentArmor} \n";
        
    }

}
