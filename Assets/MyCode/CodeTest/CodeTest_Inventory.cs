using UnityEngine;


public class CodeTest_Inventory
{
    public Sprite img;
    public int itemid;
    public int itemDetail_Id;
    public string uniqueId;
    public string name;
    public string type;
    public int durability;
    public int max_Durability;
    public int count;
    public string desc;
    public bool isStack;
    public ItemData itemDataSO;


    public CodeTest_Inventory(ItemData source, int addCount =0)
    {
        img = source.itemimg;
        isStack = source.isStack;
        itemid = source.itemid;
        itemDetail_Id = source.itemDetail_ID;
        name = source.itemName;
        type = source.itemtype.ToString() ;
        durability = Random.Range(1,source.itemMaxdur+1);
        max_Durability = source.itemMaxdur;   
        desc = source.itemDesc;
        count = 1;
        itemDataSO = source;
        
}
}
