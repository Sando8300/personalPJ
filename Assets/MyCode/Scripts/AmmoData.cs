using UnityEngine;

[CreateAssetMenu(fileName = "New Ammo", menuName = "Item/Ammo")]
public class AmmoData : ItemData
{
    [Header("Ammo Specific")]
    public bool isAP;
    public bool isFlame;

}
