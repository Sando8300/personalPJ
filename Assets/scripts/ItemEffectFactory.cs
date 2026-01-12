using UnityEngine;

public static class ItemEffectFactory
{
    public static IItemEffect GetEffect(Itemtype type)
    {
        return type switch
        {
            Itemtype.Consume => new ConsumeEffect(),
            Itemtype.Weapon => new WeaponEffect(),
            _ => null

        };
    }
}
