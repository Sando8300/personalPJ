using System.Collections;
using System.Transactions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterController : MonoBehaviour
{

    public MonsterData monsterData;
    public TextMeshPro _name;
    public TextMeshPro _hpText;
    public SpriteRenderer _image;
    int maxHp;
    int currentHp;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Start()
    {
        _name.text = monsterData.monster.name;
        _image.sprite = monsterData.monster.icon;
        maxHp = monsterData.monster.hp;
        currentHp = maxHp;        
    }

    // Update is called once per frame
  public void MonsterUI(int dmg)
    {
        currentHp -= dmg;
        _hpText.text = currentHp.ToString();
        if(currentHp <= 0)
        {
            //죽는 애니메이션
            Destroy(gameObject, 5f);
        }
    }

}
