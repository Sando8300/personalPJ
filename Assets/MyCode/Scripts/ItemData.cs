using System;
using UnityEngine;


[Serializable]
public enum Itemtype
{
    Weapon, Armor, Ammo, Consume, Misc
}
[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("CommonItemInfo")]
    public int itemid;
    public int itemDetail_ID;
    public string uniqueId;
    public string itemName;
    public Sprite itemIcon;
    public Sprite itemimg;
    public Itemtype itemtype;
    public string itemDesc;
    public int itemMaxdur = 100;
    public bool isStack;

}
