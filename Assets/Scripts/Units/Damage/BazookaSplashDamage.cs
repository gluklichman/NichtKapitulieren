using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BazookaSplashDamage : BaseDamageComponent
{

    public override void DealDamage(List<Unit> possibleAims, Unit unit)
    {
        BazookaParams bazookaParams = unit.GetParams() as BazookaParams;

        if (possibleAims.Count == 0)
        {
            return;
        }

		float maxX = 0;
		int maxXindex = 0;
		for (int i =0; i < possibleAims.Count; i++)
		{
			if (possibleAims[i].transform.position.x > maxX)
			{
				maxX = possibleAims[i].transform.position.x;
				maxXindex = i;
			}
		}

        Unit aim = possibleAims[maxXindex];

        Vector2 aimCenter = aim.transform.position;
        Vector2 explosionPoint = aimCenter;

        RaycastHit2D[] hits = Physics2D.CircleCastAll(explosionPoint, bazookaParams.explosionRadius, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider == null)
            {
                continue;
            }
            Unit target = hit.collider.GetComponent<Unit>();
            if (target == null
                || target.unitCollider != hit.collider)
            {
                continue;
            }
            target.DealDamage(unit.GetParams().damage);
        }

        GameObject explosionInstance = GameObject.Instantiate(bazookaParams.explosionPrefab) as GameObject;
        explosionInstance.transform.position = explosionPoint;
    }
}
