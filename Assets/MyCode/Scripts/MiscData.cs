using UnityEngine;



// 잡템 전용 데이터
[CreateAssetMenu(fileName = "New Misc", menuName = "Item/Misc")]
public class MiscData : ItemData
{
    [Header("Misc Specific")]
    public int tier;
    public float price;
}


