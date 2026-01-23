using UnityEngine;

public struct DamageInfo
{
    public float amount;
    public DamageType type;
    public GameObject attacker;
}
public enum DamageType
{
    Physical,
    Fire,
    Poison,
    True
}
