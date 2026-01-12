using System.Collections;

using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    float damage = 10;
    Collider col;
    public float maxcooltime =  2;
    float cooltime = 0;

    private void Awake()
    {
        col = GetComponent<Collider>();
    }
    private void Update()
    {
        if(cooltime > 0)
        {
            cooltime -= Time.deltaTime;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("플레이어 타격판정");
            
            if (cooltime <= 0)
            {
                           
                other.GetComponent<SimpleDamaged>().TakeDamage(damage, gameObject);
            Debug.Log("데미지 입힘");
                gameObject.SetActive(false);

                cooltime = maxcooltime;
            }

        }
    }

    


    // Start is called once before the first execution of Update after the MonoBehaviour is created

}
