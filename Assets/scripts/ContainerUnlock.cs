using System.Collections;
using UnityEngine;

public class ContainerUnlock : MonoBehaviour
{

    [SerializeField] AudioSource unlockSFX1;
    [SerializeField] Rigidbody doorRb;
    Coroutine lockCoroutine;
    bool isLockLT = true;
    bool isLockRT = true;
    


    private void Awake()
    {
        doorRb = GetComponent<Rigidbody>();



    }
    private void Start()
    {

        //unlockSFX1 = GetComponentInChildren<AudioSource>();
       // if (isLockLT || isLockRT) doorRb.constraints = RigidbodyConstraints.FreezeAll;
    }


    private void Update()
    {

    }
    IEnumerator DoorCheck()
    {

        if (isLockLT || isLockRT)
        {
            doorRb.constraints = RigidbodyConstraints.FreezeAll;
        }
        if (!isLockLT && !isLockRT)
        {
            yield return new WaitForSeconds(2);
            doorRb.constraints = RigidbodyConstraints.None;
        }

    }

    Coroutine cooldownLT;
    Coroutine cooldownRT;
    void UnlockSFX()
    {
        if (!isLockLT)
        {
            if (cooldownLT != null)
                return;
            if (unlockSFX1 != null)
                unlockSFX1.Play();
            cooldownLT = StartCoroutine(PlayCoolDownLT());

        }

        if (!isLockRT)
        {
            if (cooldownRT != null)
                return;
            if (unlockSFX1 != null)
                unlockSFX1.Play();
            cooldownRT = StartCoroutine(PlayCoolDownRT());

        }



    }
    public void OnLockStateChanged()
    {

        if (lockCoroutine != null)
        { StopCoroutine(DoorCheck()); }
        else
        {

            lockCoroutine = StartCoroutine(DoorCheck());
        }
    }
    IEnumerator PlayCoolDownLT()
    {
        yield return new WaitForSeconds(2.5f);
        
        cooldownLT = null;

    }
    IEnumerator PlayCoolDownRT()
    {
        yield return new WaitForSeconds(2.5f);
        cooldownRT = null;
        

    }

    IEnumerator SFXController()
    {
        if (!isLockRT)
        {

        }
        yield return null;
    }

    public void UnlockDoorLF(float value)
    {
        if (value == 1)
        {

            isLockLT = false;
            UnlockSFX();



        }
        if (unlockSFX1 != null && value < 1)
        {

            isLockLT = true;

        }
        OnLockStateChanged();
    }

    public void UnlockDoorRT(float value)
    {
        if (value == 1)
        {

            isLockRT = false;
            UnlockSFX();



        }
        if (value < 1)
        {

            isLockRT = true;

        }
        OnLockStateChanged();
    }


}
