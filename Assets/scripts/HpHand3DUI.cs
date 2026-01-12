using UnityEngine;

public class HpHand3DUI : MonoBehaviour
{
    
    public Material red;
    public Material blue;
    [SerializeField] Renderer[] hpBar; 

    private void Awake()
    {
        hpBar = GetComponentsInChildren<Renderer>();
    }
    private void Start()
    {
        GameManagerScript.instance.hpHand3DUI = this;
        HPBarUpdate();

    }
    public void HPBarUpdate()
    {
       
        int value = Mathf.CeilToInt(((GameManagerScript.instance.currentHp/ GameManagerScript.instance.maxhp) * 10));
        Debug.Log(value);
        for (int i = hpBar.Length-1; i >= 0; i--)
        {
            if (i <= value- 1)
            {
                hpBar[i].material = red;
            }
            else
            {
                hpBar[i].material = blue ;
            }
        }
    }

}
