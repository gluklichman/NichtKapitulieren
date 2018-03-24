using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TankSplashDamage : BaseDamageComponent
{
    /*private const float MIN_DELTA = 1.0f;
    private const float MAX_DELTA = 5.0f;
    private const float RADIUS = 3.0f;*/

    public override void DealDamage(List<Unit> possibleAims, Unit unit)
    {
        TankParams tankParams = unit.GetParams() as TankParams;
        possibleAims.RemoveAll(target => target == null);

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

        Vector2 radiusVector = (point - (Vector2)unit.transform.position).normalized;
        float delta = Random.Range(tankParams.minShootError, tankParams.maxShootError);
        Vector2 explosionPoint = point + radiusVector * delta;

        RaycastHit2D[] hits = Physics2D.CircleCastAll(explosionPoint, tankParams.explosionRadius, Vector2.zero);
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

        GameObject explosionInstance = GameObject.Instantiate(tankParams.explosionPrefab) as GameObject;
        explosionInstance.transform.position = explosionPoint;
    }
}
