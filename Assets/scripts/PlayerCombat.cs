using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // 싱글톤으로 만들면 외부에서 접근하기 편함 (플레이어가 1명일 때)
    public static PlayerCombat Instance;

    private void Awake()
    {
        Instance = this;
    }

    [Header("상태 정보")]
    public bool isEquipped = false; // 무기 장착 여부
    public WeaponData currentWeapon; // 현재 낀 무기 데이터
    GameObject currentPrefab; // 현재 사용 중인 무기 프리팹
    public Transform cameraPoint;

    // 무기 장착 함수
    public void EquipWeapon(string uniqueid, WeaponData newWeapon)
    {
        if (!GameManagerScript.instance.inventoryManager.inventory.ContainsKey(uniqueid))
        {
            Debug.Log("장착할 아이템이 없습니다.");
            return;
        }
        currentWeapon = newWeapon;
        isEquipped = true;

        if (currentWeapon != null)
            Destroy(currentPrefab);
        var gunprefab= Instantiate(currentWeapon.weaponPrefab, cameraPoint);
        
        GetComponent<PlayerGunController>().gun = gunprefab.GetComponent<Gun>();

        Debug.Log($"{newWeapon.itemName}을(를) 손에 들었습니다!");

    }

    // 무기 해제 함수
    public void UnEquipWeapon()
    {
        isEquipped = false;
        currentWeapon = null;
        Debug.Log("무기를 집어넣었습니다.");
    }
}