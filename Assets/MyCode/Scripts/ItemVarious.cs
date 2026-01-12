using JetBrains.Annotations;
using UnityEngine;



// 무기 전용 데이터 (ItemDataSO를 상속받음)
[CreateAssetMenu(fileName = "New Weapon", menuName = "Item/Weapon")]
public class WeaponData : ItemData
{
    [Header("Weapon Specific")]
    public int damage;
    public float fireRate;
    public int magazineSize;
    public GameObject weaponPrefab;
    public Vector3 offset;
    public int[] usedBulletInfoArray;
    
}

// 소모품 전용 데이터
[CreateAssetMenu(fileName = "New Potion", menuName = "Item/Consumable")]
public class ConsumableData : ItemData
{
    [Header("Consumable Specific")]
    public int healAmount;
    public float buffDuration;
}

// 잡템 전용 데이터
[CreateAssetMenu(fileName = "New Misc", menuName = "Item/Misc")]
public class MiscData : ItemData
{
    [Header("Misc Specific")]
    public int tier;
    public float price;
}

// 방어구 전용 데이터
[CreateAssetMenu(fileName = "New Armor", menuName = "Item/Armor")]
public class ArmorData : ItemData
{
    [Header("Armor Specific")]
    public int resist;
}

[CreateAssetMenu(fileName = "New Ammo", menuName = "Item/Ammo")]
public class AmmoData : ItemData
{
    [Header("Ammo Specific")]
    public bool isAP;
    public bool isFlame;

}

