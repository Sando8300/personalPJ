using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public InteractableScript interactableScript;
    public ItemData itemData;
    public ItemData[] randomSet;
    public string animName;
    public Sprite icon;
    public bool isLoot = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemData = randomSet[Random.Range(0, randomSet.Length)];
        icon = interactableScript.interactBase.icon;
        
    }


    // Update is called once per frame

}
