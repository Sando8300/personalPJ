
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



