using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


public class EnemySimpleDamaged : MonoBehaviour
{
    public float maxHp = 100;
    float currentHp = 0;
    float cooltime = 0;
    MaterialPropertyBlock propblock;
    Renderer renderer;
    bool isRed = false;


    void Awake()
    {
        currentHp = maxHp;
        renderer = GetComponentInChildren<SkinnedMeshRenderer>();
        propblock = new MaterialPropertyBlock();
    }
    public void TakeDamage(float damage)
    {
       
        currentHp -= damage;
      
        if (currentHp < 50)
        {
            renderer.GetPropertyBlock(propblock);
            propblock.SetColor("_BaseColor", new Color(1, 0, 0));
            renderer.SetPropertyBlock(propblock);
            if (!isRed) isRed = true;
            
            if (currentHp <= 0)
            {
                transform.position += Vector3.up * 0.3f;           
                StartCoroutine(CallRegdoll());               
            }

        }
    }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        

    private void Start()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        if (cooltime <= 0)
        {
            cooltime = 1;
            // Debug.Log(currentHp);
        }
        if (cooltime > 0)
        {
            cooltime -= Time.deltaTime;

        }
    }

    IEnumerator CallRegdoll()
    {
        yield return new WaitForSeconds(0.3f);
        GetComponent<SubjectAI>().RagdollStart();
        yield return null;
    }

}


