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



    private void Awake()
    {
        useBtn = GetComponentInChildren<Button>();
    }
    public void UpdatePreviewUI(CodeTest_Inventory privateItem)
    {
        previewData = privateItem;
        img.sprite = privateItem.img;
        itemnameText.text = privateItem.name;
        StringBuilder sb = new StringBuilder();
        sb.Append($"Type : {privateItem.type}");
        sb.Append($"\n{privateItem.desc}");
        belowtext.text = sb.ToString();
        Debug.Log(previewData.itemDetail_Id);
        useBtn.onClick.RemoveAllListeners();
        useBtn.onClick.AddListener(() => { GameManagerScript.instance.inventoryManager.UseItem(previewData.itemDetail_Id.ToString()); });
        
        equipBtn.onClick.AddListener(() => { PlayerCombat.Instance.EquipWeapon(previewData.uniqueId, previewData.itemDataSO as WeaponData); });


    }


}
