using UnityEngine;

public class ArmorEffect : IItemEffect
{
 
    public void Apply(CodeTest_Inventory item)
    {
        // 1. 데이터 가져오기 (형변환)
        var armorData = item.itemDataSO as ArmorData;

        if (armorData != null)
        {
            Debug.Log($"[효과] 덜그럭! {armorData.itemName} 방어구 장착 (방어력 +{armorData.resist})");

           
            GameManagerScript.instance.currentArmor += armorData.resist;
            
        }
    }

    public void Remove(CodeTest_Inventory item)
    {
        var armorData = item.itemDataSO as ArmorData;

        if (armorData != null)
        {
            Debug.Log($"[효과] 덜그럭! {armorData.itemName} 방어구 탈착 (방어력 -{armorData.resist})");


            GameManagerScript.instance.currentArmor -= armorData.resist;

        }
    }
}
