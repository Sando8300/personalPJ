using UnityEngine;
using System.Linq;
using System.Collections;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public EnemySimpleDamaged[] enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        double avgHP = 0;
        enemy = FindObjectsByType<EnemySimpleDamaged>(sortMode: 0);
        avgHP = enemy.Average(e => e.maxHp);
        Debug.Log(avgHP);
        
    } 
   
}

    
