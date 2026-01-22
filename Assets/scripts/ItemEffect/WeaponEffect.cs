using UnityEngine;

// 3. 무기 아이템 효과 (구현 - 나중에 추가하기 쉬움)
public class WeaponEffect : IItemEffect
{
    public void Apply(CodeTest_Inventory item)
    {
        var weaponitem = item.itemDataSO as WeaponData;
        Debug.Log($"[효과] 철커덕! 공격력 {weaponitem.damage} 무기 장착!");
        GameManagerScript.instance.damage += weaponitem.damage;
    }
    public void Remove(CodeTest_Inventory item)
    {
        var weaponitem = item.itemDataSO as WeaponData;
        Debug.Log($"[효과] 철커덕! 공격력 {weaponitem.damage} 무기 탈착!");
        GameManagerScript.instance.damage -= weaponitem.damage;
    }
}
