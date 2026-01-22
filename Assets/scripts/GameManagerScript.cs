using UnityEngine;


public class GameManagerScript : MonoBehaviour
{

    public static GameManagerScript instance;
    public CodeTest_InventoryManager inventoryManager;
    public CodeTest_UIManager uiManager;
    public InventoryUI invenUI;
    public AudioManager audioManager;
    public AmmoPoolingMG ammoPoolingMG;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float currentHp;
    public int maxhp;
    public int currentArmor = 0;
    public int damage = 0;
    public float hpRadio;
    public HpHand3DUI hpHand3DUI;
    public SimpleDamaged simpleDamagedPlayer;
    public int[] magRemainAmmo;
    public float timer;
    public AudioSource playersource;
    public AudioClip infosound;
    public GameObject[] subject1;
    public GameObject[] subject2;
    [SerializeField] bool gamestart = false;

    // public GameObject[] subjects;
    


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }

        else
        {
            Destroy(gameObject);
        }

        maxhp = 100;
        currentHp = maxhp;
        hpRadio = currentHp / maxhp;
        timer = 80;

        
    }

    void Start()
    {
        uiManager.statusText.text = "Prepare To Remove Sub, Get Ammo.";
        StartCoroutine(uiManager.TextBlink());
        playersource.PlayOneShot(infosound);
        timer = Time.time + 10;
        StartCoroutine(uiManager.HpRefresh());
    }

    public void HpModify(int amount)
    {
        currentHp += amount;
        currentHp = Mathf.Clamp(currentHp, 0, maxhp);
        StartCoroutine(uiManager.HpRefresh());
    }















    bool wave1done = false;
    bool wave2done = false;
    bool missiondone = false;



    void Update()
    {
        if (!gamestart)
            return;
        if (!wave1done && timer < Time.time)
        {
            uiManager.statusText.text = "Wave1 is coming...";
            foreach (GameObject obj in subject1)
            {
                obj.SetActive(true);
            }
            StartCoroutine(uiManager.TextBlink());
            playersource.PlayOneShot(infosound);
            
            wave1done = true;
            timer = 60;
        }
        if (wave1done && 60 < Time.time && !wave2done)
        {
            wave2done = true;
            uiManager.statusText.text = "Wave2 is coming...";
            foreach (GameObject obj in subject2)
            {
                obj.SetActive(true);
            }
            StartCoroutine(uiManager.TextBlink());
            playersource.PlayOneShot(infosound);

            timer = 100;


        }
        if (!missiondone && 100 < Time.time)
        {
            uiManager.statusText.text = "Welldone, Back to base.";
            StartCoroutine(uiManager.TextBlink());
            playersource.PlayOneShot(infosound);
            missiondone = true;

        }

    }
}
