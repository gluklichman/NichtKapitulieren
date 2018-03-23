using UnityEngine;
using System.Collections;

public class Tank : Unit
{
    protected override void DestroyInstance()
    {
        UnitPool.Instance.ReturnTankToPool(this);
    }

    public override BaseDamageComponent GetDamageComponent()
    {
        return new TankSplashDamage();
    }

    public override UnitType GetUnitType()
    {
        return UnitType.TANK;
    }
}
