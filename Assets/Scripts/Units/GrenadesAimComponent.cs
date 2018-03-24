using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrenadesAimComponent : UnitAimComponent
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override List<Unit> GetPossibleAims()
    {
        if (HasTankInLine())
        {
            List<Unit> tankAims = new List<Unit>();
            Unit tank = possibleAims.Find(aim => aim.GetUnitType() == UnitType.TANK);
            if (tank)
                tankAims.Add(tank);
            return tankAims;
        }
        return base.GetPossibleAims();
    }

    public override bool HasAvailableAims()
    {
        if (HasTankInLine())
        {
            return possibleAims.Find(aim => aim.GetUnitType() == UnitType.TANK) != null;
        }
        return base.HasAvailableAims();
    }

    private bool HasTankInLine()
    {
        Vector2 direction = (unit.GetOwner() == UnitOwner.PLAYER) ? Vector2.right : Vector2.left;
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, aimRadius.radius, direction, 100);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider == null)
            {
                continue;
            }
            Unit target = hit.collider.GetComponent<Unit>();
            if (target == null)
            {
                continue;
            }
            if (hit.collider != target.unitCollider
                || unit.GetOwner() == target.GetOwner())
            {
                continue;
            }
            if (target.GetUnitType() == UnitType.TANK)
            {
                return true;
            }
        }
        return false;
    }
}
