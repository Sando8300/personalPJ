using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI counttxt;
    int itemCount = 1;
    public Toggle toggle;
    public PreviewSlot previewSlot;
    public ItemSlot itemSlot;
   
     CodeTest_Inventory privateItem;

    private void Awake()
    {
        toggle.group = GetComponentInParent<ToggleGroup>();
    }
    public void Getiteminfo(CodeTest_Inventory item)
    {
        itemName.text = item.name;
        counttxt.text = item.count.ToString();
        privateItem = item;
    }

    public void ShowPreview()
    {
        previewSlot = FindAnyObjectByType<PreviewSlot>();
        previewSlot.UpdatePreviewUI(privateItem);
       
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created


}
