
using UnityEngine;

// 1. 인터페이스 (규격)
public interface IItemEffect
{
    void Apply(CodeTest_Inventory item);
}

// 2. 소비 아이템 효과 (구현)
public class ConsumeEffect : IItemEffect
{
    public void Apply(CodeTest_Inventory item)
    {
        var consumeitem = item.itemDataSO as ConsumableData;       
        Debug.Log($"[효과] 냠냠! 체력이 {consumeitem.healAmount}만큼 {consumeitem.buffDuration}초 동안 회복됩니다.");
        
        if(consumeitem != null)
        {
            if(consumeitem.buffDuration == 0)
            {
                GameManagerScript.instance.HpModify(consumeitem.healAmount);
            }
            else
            {
                GameManagerScript.instance.GetComponent<StatusController>().AddHealOverTime(consumeitem.healAmount, consumeitem.buffDuration);
            }
        }
        
    }
}

// 3. 무기 아이템 효과 (구현 - 나중에 추가하기 쉬움)
public class WeaponEffect : IItemEffect
{
    public void Apply(CodeTest_Inventory item)
    {
        var weaponitem = item.itemDataSO as WeaponData;
        Debug.Log($"[효과] 철커덕! 공격력 {weaponitem.damage} 무기 장착!");
        // PlayerCombat.Instance.Equip(item);
    }
}