using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    InteractableObject obj;
    public Image playerinteractorUI;
    int id;
    bool isNull;
    public bool isLoot = false;
    public bool isDoor;
    public AudioClip lootSound;
    public AudioClip openSound;
    public AudioSource source;

    private void Update()
    {if (!isLoot) return;
       if(Input.GetKeyDown(KeyCode.E))
        { 

            Debug.Log("æ∆¿Ã≈€ »πµÊ");
            GameManagerScript.instance.inventoryManager.Additem(id.ToString(), obj.itemData);
            obj.GetComponent<Animator>().Play(obj.animName);
            source.PlayOneShot(lootSound);
            obj.isLoot = !obj.isLoot;
            isLoot = !isLoot;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Interactor") || other.CompareTag("Enemy"))
        {
            
            isNull = other.TryGetComponent<InteractableObject>(out obj);
            if(!isNull)
            {
                obj = other.GetComponentInParent<InteractableObject>();
            }
            if (obj.isLoot)
                return;
            playerinteractorUI.sprite = obj.icon ;
            playerinteractorUI.color = GameManagerScript.instance.uiManager.cON;
            id = obj.itemData.itemDetail_ID;
            Debug.Log("¿ŒΩƒ");

            isLoot = true;
            
            


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Interactor") || other.CompareTag("Enemy"))
        {
            
            playerinteractorUI.color = GameManagerScript.instance.uiManager.cOFF;
            obj = null;
            isLoot = false;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

}
