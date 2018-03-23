using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BazookaSplashDamage : BaseDamageComponent
{
    /*private const float MIN_DELTA = 1.0f;
    private const float MAX_DELTA = 5.0f;
    private const float RADIUS = 3.0f;*/

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
       // float radius = unit.GetParams().hitRadius;
       // Vector2 point = Random.insideUnitCircle * radius + aimCenter;

        //Vector2 radiusVector = (point - (Vector2)unit.transform.position).normalized;
        //float delta = Random.Range(bazookaParams.minShootError, bazookaParams.maxShootError);
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
            //if (target.GetOwner() == unit.GetOwner())
            //{
            //    continue;
            //}
            target.DealDamage(unit.GetParams().damage);
        }

        GameObject explosionInstance = GameObject.Instantiate(bazookaParams.explosionPrefab) as GameObject;
        explosionInstance.transform.position = explosionPoint;
    }
}
