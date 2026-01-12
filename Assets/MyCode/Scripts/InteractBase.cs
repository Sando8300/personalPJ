using System;
using Unity.VisualScripting;
using UnityEngine;

public enum InteractorType { loot, door };
[Serializable]
public class InteractBase 
{
    public InteractorType interactorType;
    public Sprite icon;
    
    


}
