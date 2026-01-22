using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PreviewSlot : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Image img;
    public TextMeshProUGUI itemnameText;
    public TextMeshProUGUI belowtext;
    public CodeTest_Inventory previewData;
    Button useBtn;
    public Button equipBtn;
    public Button unEquipBtn;



    private void Awake()
    {
        useBtn = GetComponentInChildren<Button>();
    }
    public void UpdatePreviewUI(CodeTest_Inventory privateItem)
    {
        previewData = privateItem;
        if (previewData.type == "Weapon")
            PlayerCombat.Instance.currentWeaponitem = previewData;
        if (previewData.type == "Armor")
            PlayerCombat.Instance.currentArmoritem = previewData;
        img.sprite = privateItem.img;
        itemnameText.text = privateItem.name;
        StringBuilder sb = new StringBuilder();
        sb.Append($"Type : {privateItem.type}");
        sb.Append($"\n{privateItem.desc}");
        belowtext.text = sb.ToString();
        Debug.Log(previewData.itemDetail_Id);


        //소모아이템 사용
        useBtn.onClick.RemoveAllListeners();
        useBtn.onClick.AddListener(() => { GameManagerScript.instance.inventoryManager.UseItem(previewData.itemDetail_Id.ToString()); });



        //장비 장착
        equipBtn.onClick.RemoveAllListeners();
      
        if (previewData.type == "Weapon" && !PlayerCombat.Instance.currentWeaponitem.isEquipped)
            equipBtn.onClick.AddListener(() => { PlayerCombat.Instance.EquipWeapon(previewData.uniqueId, previewData); });
        else if (previewData.type == "Armor" && !PlayerCombat.Instance.currentArmoritem.isEquipped)
            equipBtn.onClick.AddListener(() => { PlayerCombat.Instance.EquipArmor(previewData.uniqueId, previewData); });
        
        //장비창 새로고침
        equipBtn.onClick.AddListener(() => { GameManagerScript.instance.invenUI.LoadOut(previewData.uniqueId, previewData.type); });

        //장비 해제
        unEquipBtn.onClick.RemoveAllListeners();
        

        if (previewData.type == "Weapon" && PlayerCombat.Instance.currentWeaponitem.isEquipped)
        {
            unEquipBtn.onClick.AddListener(() => { PlayerCombat.Instance.UnEquipWeapon(PlayerCombat.Instance.currentWeaponitem.currentMagCount); });
        }
        else if (previewData.type == "Armor" && PlayerCombat.Instance.currentArmoritem.isEquipped)
        {
            unEquipBtn.onClick.AddListener(() => { PlayerCombat.Instance.UnEquipArmor(); });
        }
        //장비창 새로고침
        unEquipBtn.onClick.AddListener(() => { GameManagerScript.instance.invenUI.LoadOut(previewData.uniqueId, previewData.type); });



    }


}
