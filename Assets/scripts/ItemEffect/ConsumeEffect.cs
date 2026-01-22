using UnityEngine;


public class ConsumeEffect : IItemEffect
{
    public void Apply(CodeTest_Inventory item)
    {
        var consumeitem = item.itemDataSO as ConsumableData;
        Debug.Log($"[효과] 냠냠! 체력이 {consumeitem.healAmount}만큼 {consumeitem.buffDuration}초 동안 회복됩니다.");

        if (consumeitem != null)
        {
            if (consumeitem.buffDuration == 0)
            {
                GameManagerScript.instance.HpModify(consumeitem.healAmount);
            }
            else
            {
                GameManagerScript.instance.GetComponent<StatusController>().AddHealOverTime(consumeitem.healAmount, consumeitem.buffDuration);
            }
        }
    }
    public void Remove(CodeTest_Inventory item)
    {
    }
}
