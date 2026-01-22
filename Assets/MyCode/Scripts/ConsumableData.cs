using UnityEngine;

// 소모품 전용 데이터
[CreateAssetMenu(fileName = "New Potion", menuName = "Item/Consumable")]
public class ConsumableData : ItemData
{
    [Header("Consumable Specific")]
    public int healAmount;
    public float buffDuration;
}
