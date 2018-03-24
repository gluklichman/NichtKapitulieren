using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleDamage : BaseDamageComponent
{
    public override void DealDamage(List<Unit> possibleAims, Unit unit)
    {
        if (possibleAims.Count == 0)
        {
            return;
        }
        
        System.Random rand = new System.Random();
        int index = rand.Next(possibleAims.Count - 1);

        Unit aim = possibleAims[index];

        Vector2 aimCenter = aim.transform.position;
        float radius = unit.GetParams().hitRadius;
        Vector2 point = Random.insideUnitCircle * radius + aimCenter;
        if (aim != null
            && aim.unitCollider.bounds.Contains(point))
        {
            aim.DealDamage(unit.GetParams().damage);
        }
    }
}
