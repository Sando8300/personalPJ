using UnityEngine;

public class DropBoxScript : MonoBehaviour
{
    CodeTest_InventoryManager inventoryManager;
    [SerializeField] ItemData[] itemBox;
    bool isGet = false;
    public bool getAll;

    private void Start()
    {
        inventoryManager = FindFirstObjectByType<CodeTest_InventoryManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
       


        if (other.CompareTag("Player"))
        {
            if (isGet) return;
            /* if (getAll)
             {
                 for (int i = 0; i < itemBox.Length; i++)
                 {
                     ItemData randomItem1 = itemBox[i];
                     inventoryManager.Additem(randomItem1.item.itemDetail_ID.ToString(), randomItem1);
                     if (randomItem.item.itemid == 2)
                     {
                         inventoryManager.Equipitem(randomItem1.item.itemDetail_ID.ToString());
                     }
                     isGet = true;
                 }
                 return;
             }*/
            ItemData randomItem = itemBox[Random.Range(0, itemBox.Length)];
            inventoryManager.Additem(randomItem.itemDetail_ID.ToString(), randomItem);
            //inventoryManager.Equipitem(randomItem.itemDetail_ID.ToString());
            isGet = true;

        }
    }
}
