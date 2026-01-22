using UnityEngine;

// 방어구 전용 데이터
[CreateAssetMenu(fileName = "New Armor", menuName = "Item/Armor")]
public class ArmorData : ItemData
{
    [Header("Armor Specific")]
    public int resist;
}
