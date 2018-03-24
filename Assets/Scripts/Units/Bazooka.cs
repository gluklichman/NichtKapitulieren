using UnityEngine;
using System.Collections;

public class Bazooka : Unit
{
	protected override void DestroyInstance()
	{
		UnitPool.Instance.ReturnBazookaToPool(this);
	}

	public override BaseDamageComponent GetDamageComponent()
	{
		return new BazookaSplashDamage();
	}

	public override UnitType GetUnitType()
	{
		return UnitType.BAZOOKA;
	}

    protected override void CreateAimComponent()
    {
        if (aimComponent == null)
        {
            aimComponent = gameObject.AddComponent<GrenadesAimComponent>();
        }
    }
}
