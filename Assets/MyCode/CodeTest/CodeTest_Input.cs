
using UnityEngine;


public class CodeTest_Input : MonoBehaviour
{
    public InventoryUI ui;
    public CodeTest_InventoryManager inventory;
    public PlayerGunController guncon;


    private void Awake()
    {
        
       
    }
    private void Start()
    {
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void OnInventory()
    {
        if (Cursor.lockState == CursorLockMode.Confined)
        { Cursor.lockState = CursorLockMode.Locked; }
        Cursor.lockState = CursorLockMode.Confined;
        ui.isInvenShow = !ui.isInvenShow;
        ui.inventoryObject.SetActive(ui.isInvenShow);
        ui.Refresh(inventory.inventory);
        guncon.enabled = !ui.isInvenShow;


    }
}
