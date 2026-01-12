
using System.Collections;
using System.Linq;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public UnityEngine.InputSystem.InputActionProperty fireAction;
    Transform spentCasePos;
    public Transform firePos;
    public GameObject bulletprefab, casingprefab;
    public GameObject[] Mag;
    bool isEmpty = false;
    bool isfire = false;
    bool isTriggerHeld = false;
    public int maxAmmo = 30;
    public int currentAmmo;

    public float reloadDelayTime = 0.3f;
    public float reloadCompleteTime = 0.2f;


    public AudioSource startSFX;
    public AudioSource loopSFX;
    public AudioSource gunSoundSFX;
    public AudioClip reloadSFX;
    public AudioClip emptyMagSFX;
    public AudioClip noAmmoSFX;
    public ParticleSystem muzzleFx1;
    public ParticleSystem muzzleFx2;
    public ParticleSystem muzzleFx3;
    public WFX_LightFlicker wFX_LightFlicker;
    public bool isAutomatic = true;
    public Animator gunAnimator;

    float fireRate = 0.063f;
    float nextFireTime = 0f;
    public int shotSpd = 50;
    Rigidbody rb;

    public GameManagerScript gameManager;

    CodeTest_Inventory exist;
    private void Awake()
    {
        gameManager = GameManagerScript.instance;

    }
    void Start()
    {
        
        for (int i = 0; i < PlayerCombat.Instance.currentWeapon.usedBulletInfoArray.Length; i++)
        {
            if (GameManagerScript.instance.inventoryManager.inventory.ContainsKey(PlayerCombat.Instance.currentWeapon.usedBulletInfoArray[i].ToString()))
            {
                exist = GameManagerScript.instance.inventoryManager.inventory[PlayerCombat.Instance.currentWeapon.uniqueId];
            }
            else
                exist = null;                
        }
        


        maxAmmo = PlayerCombat.Instance.currentWeapon.magazineSize;
        if (exist == null)
        {
            gameManager.uiManager.statusText.text = "I dont have any ammo, have to find one.";
            StartCoroutine(gameManager.uiManager.TextBlink());
            gunSoundSFX.PlayOneShot(noAmmoSFX);
            isEmpty = true;
        }

        if (exist != null)
        {
            if (exist.count >= maxAmmo)
                currentAmmo = exist.count / maxAmmo;
            else
               currentAmmo = exist.count;
        }

        Mag = new GameObject[maxAmmo];
        for (int i = 0; i < maxAmmo; i++)
        {
            Mag[i] = Instantiate(bulletprefab, firePos.position, firePos.rotation);
            Mag[i].SetActive(false);
        }

        StartCoroutine(gameManager.uiManager.ammoTextRefresh(currentAmmo));

    }

    // Update is called once per frame
    void Update()
    {

        if (isTriggerHeld == true && Time.time > nextFireTime && !isEmpty && isAutomatic )
        {
            nextFireTime = Time.time + fireRate;
            FireShot();
        }
    }
    public Coroutine reload;
    bool isReload = false;
    public void FireShot()
    {
        //딸깍소리출력
        if (isEmpty)
        {
            gunSoundSFX.PlayOneShot(emptyMagSFX);
            return;
        }
        int fireIndex = currentAmmo - 1;
        isfire = true;
        Mag[fireIndex].SetActive(true);
        Mag[fireIndex].transform.position = firePos.position;
        Mag[fireIndex].transform.rotation = firePos.rotation;
        rb = Mag[fireIndex].GetComponent<Rigidbody>();
        rb.angularVelocity = Vector3.zero;
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(firePos.forward * shotSpd, ForceMode.VelocityChange);
        if (!isTriggerHeld) startSFX.Play();
        wFX_LightFlicker.FlashOnce(0.05f);
        muzzleFx1.Stop();
        muzzleFx1.Play();
        muzzleFx2.Stop();
        muzzleFx2.Play();
        muzzleFx3.Stop();
        muzzleFx3.Play();


        if (isTriggerHeld) loopSFX.Play();
        StartCoroutine(Firerate(fireIndex));
        currentAmmo--;
        StartCoroutine(gameManager.uiManager.ammoTextRefresh(currentAmmo));
        if (currentAmmo == 0)
        {



            isEmpty = true;

            //장전로직 추가예정
        }

    }

    IEnumerator Firerate(int firedBulletIndex)
    {
        yield return new WaitForSeconds(fireRate);
        isfire = false;
        yield return new WaitForSeconds(3);
        Mag[firedBulletIndex].GetComponent<bulletDamage>().isDamaged = false;
        Mag[firedBulletIndex].SetActive(false);


    }



    public IEnumerator ReloadDuration()
    {
        isEmpty = true;
        var exist = gameManager.inventoryManager.inventory.Values.FirstOrDefault(item => item.itemDetail_Id.ToString() == "3000");
        if (exist == null)
        {
            gameManager.uiManager.statusText.text = "I dont have any ammo, have to find one.";
            StartCoroutine(gameManager.uiManager.TextBlink());
            gunSoundSFX.PlayOneShot(noAmmoSFX);

            yield break;
        }

        if (exist != null)
        {
            if (exist.count >= maxAmmo)
            {
                currentAmmo = Mathf.Clamp(exist.count, 1, maxAmmo);
                exist.count -= maxAmmo;
            }


            else
            {
                currentAmmo = exist.count;
                exist.count -= exist.count;
            }
        }
        if (exist.count == 0)
            gameManager.inventoryManager.RemoveBrokenItem();

        gunAnimator.SetTrigger("isReload");
        gunSoundSFX.PlayOneShot(reloadSFX);


        yield return new WaitForSeconds(reloadDelayTime);
        if (gunAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "root|MC_MP7_full_reload")
        {
            Debug.Log(gunAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
            yield return new WaitForSeconds(gunAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length - reloadCompleteTime);
        }
        else { yield return new WaitForSeconds(2.7f); Debug.Log("임시 장전시간 진행중"); }

        isEmpty = false;
        Debug.Log("리로드완료");
        StartCoroutine(gameManager.uiManager.ammoTextRefresh(currentAmmo));
        reload = null;
    }


    public void FireMode()
    {
        isAutomatic = !isAutomatic;
    }

    public void OnTriggerHeld()
    {
        isTriggerHeld = true;
        // Debug.Log("눌림");
    }
    public void OffTriggerHeld()
    {
        isTriggerHeld = false;
        // Debug.Log("때짐");
    }


}
