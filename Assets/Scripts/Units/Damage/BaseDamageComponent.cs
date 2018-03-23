using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseDamageComponent {

    public abstract void DealDamage(List<Unit> possibleAims, Unit unit);
	
}
