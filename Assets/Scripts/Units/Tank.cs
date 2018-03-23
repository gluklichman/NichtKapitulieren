using UnityEngine;
using System.Collections;

public class Tank : Unit
{
    protected override void DestroyInstance()
    {
        UnitPool.Instance.ReturnTankToPool(this);
    }
}
