using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int[] dmgList = {8,9,10};
    float attackTime = 0;
    bool isAttack = false;
    int attackDmg = 5;
    Transform toEnemyMaxDealing;
    MonsterController enemy;
    private void Update()
    {
        if(!isAttack) return;
        if (Time.time >= attackTime)
            isAttack = false;
        
            
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (!isAttack)
        {
            attackDmg = Random.Range(dmgList[0], dmgList[dmgList.Length-1]+1);
            Debug.Log("공격성공");
            isAttack = true;
            attackTime = Time.time + 0.5f;
            if (other.TryGetComponent<MonsterController>(out enemy))
            {
                if (Mathf.Max(dmgList) == attackDmg)
                {

                    enemy.MonsterUI(attackDmg);
                    GameManager.Instance.inventoryManager.ReduceDur();
                    

                }
                enemy.MonsterUI(attackDmg);
            }
            Debug.Log(attackDmg);
           
        }
    }
    


  
}
