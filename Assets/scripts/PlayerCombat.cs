using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    // 싱글톤으로 만들면 외부에서 접근하기 편함 (플레이어가 1명일 때)
    public static PlayerCombat Instance;

    private void Awake()
    {
        Instance = this;
        equipmentInfo = new Dictionary<string, string>();
    }

    public Dictionary<string, string> equipmentInfo;

    [Header("무기 상태 정보")]
    public CodeTest_Inventory currentWeaponitem; // 현재 장착한 무기 인벤정보
    public WeaponData weaponData; // 현재 장착한 무기 SO데이터
    GameObject currentPrefab; // 현재 사용 중인 무기 프리팹
    public Transform cameraPoint;

    [Header("방어구 상태 정보")]
    public CodeTest_Inventory currentArmoritem; // 현재 장착한 방어구 인벤정보
    public ArmorData armorData; // 현재 장착한 방어구 SO데이터



    // 무기 장착 함수
    public void EquipWeapon(string uniqueid, CodeTest_Inventory weapon)
    {
        if (!GameManagerScript.instance.inventoryManager.inventory.ContainsKey(uniqueid))
        {
            Debug.Log("장착할 무기가 없습니다.");
            return;
        }
        currentWeaponitem = GameManagerScript.instance.inventoryManager.inventory[weapon.uniqueId];
        weaponData = GameManagerScript.instance.inventoryManager.inventory[currentWeaponitem.uniqueId].itemDataSO as WeaponData;

        if (currentWeaponitem != null)
            Destroy(currentPrefab);
        currentPrefab = Instantiate(weaponData.weaponPrefab, cameraPoint);
        currentWeaponitem.isEquipped = true;
        var effect = ItemEffectFactory.GetEffect(weaponData.itemtype);
        effect?.Apply(currentWeaponitem);

        GetComponent<PlayerGunController>().gun = currentPrefab.GetComponent<Gun>();
        GetComponent<FPplayer>().fpsAnimator = currentPrefab.GetComponent<Animator>();
        equipmentInfo.Add(currentWeaponitem.itemid.ToString(), currentWeaponitem.uniqueId);
        Debug.Log($"{GameManagerScript.instance.inventoryManager.inventory[currentWeaponitem.uniqueId].name}을(를) 손에 들었습니다!");


    }

    // 무기 해제 함수
    public void UnEquipWeapon(int currentAmmo)
    {
        currentWeaponitem.currentMagCount = currentAmmo;
        currentWeaponitem.isEquipped = false;
        var effect = ItemEffectFactory.GetEffect(weaponData.itemtype);
        effect?.Remove(currentWeaponitem);
        Destroy(currentPrefab);
        GameManagerScript.instance.uiManager.ammoText.text = "";
        GameManagerScript.instance.uiManager.ammoTextRefresh(0);
        equipmentInfo.Remove(currentWeaponitem.itemid.ToString());
        Debug.Log("무기를 집어넣었습니다.");
    }

    public void EquipArmor(string uniqueid, CodeTest_Inventory armor)
    {
        if (!GameManagerScript.instance.inventoryManager.inventory.ContainsKey(uniqueid))
        {
            Debug.Log("장착할 방어구가 없습니다.");
            return;
        }
        currentArmoritem = GameManagerScript.instance.inventoryManager.inventory[armor.uniqueId];
        armorData = GameManagerScript.instance.inventoryManager.inventory[currentArmoritem.uniqueId].itemDataSO as ArmorData;
        currentArmoritem.isEquipped = true;
        equipmentInfo.Add(currentArmoritem.itemid.ToString(), currentArmoritem.uniqueId);
        var effect = ItemEffectFactory.GetEffect(currentArmoritem.itemDataSO.itemtype);
        effect?.Apply(currentArmoritem);

        Debug.Log($"{GameManagerScript.instance.inventoryManager.inventory[currentArmoritem.uniqueId].name} 장착했습니다.");


    }

    // 무기 해제 함수
    public void UnEquipArmor()
    {

        currentArmoritem.isEquipped = false;
        equipmentInfo.Remove(currentArmoritem.itemid.ToString());
        var effect = ItemEffectFactory.GetEffect(currentArmoritem.itemDataSO.itemtype);
        effect?.Remove(currentArmoritem);
        Debug.Log("방어구를 벗었습니다.");
    }

}
   


