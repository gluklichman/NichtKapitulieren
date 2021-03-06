﻿using UnityEngine;
using System.Collections.Generic;

public class UnitAimComponent : MonoBehaviour
{
    protected CircleCollider2D aimRadius = null;
    protected Unit unit = null;
    protected List<Unit> possibleAims = null;

    // Use this for initialization
    void Awake()
    {

    }

    public void Init(Unit unit)
    {
        this.unit = unit;

        if (aimRadius == null)
        {
            aimRadius = gameObject.AddComponent<CircleCollider2D>();
        }

        if (unit.GetParams() == null)
        {
            Debug.LogError("No unit params");
        }
        aimRadius.enabled = true;
        aimRadius.isTrigger = true;
        aimRadius.radius = unit.GetParams().aimRadius + Random.Range(-0.3f*unit.GetParams().aimRadius, 0.3f*unit.GetParams().aimRadius);

        possibleAims = new List<Unit>();
    }

    public void Deinit()
    {
        aimRadius.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Unit otherUnit = collision.GetComponent<Unit>();
        if (otherUnit != null
            && otherUnit.GetOwner() != unit.GetOwner()
            && collision == otherUnit.unitCollider
            && otherUnit.GetUnitState().GetStateType() != UnitStateType.IDLE)
        {
            otherUnit.unitDestroyed += OnUnitDestroyed;
            possibleAims.Add(otherUnit);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Unit otherUnit = collision.GetComponent<Unit>();
        if (otherUnit)
        {
            otherUnit.unitDestroyed -= OnUnitDestroyed;
            possibleAims.Remove(otherUnit);
        }
    }

    public virtual bool HasAvailableAims()
    {
        return possibleAims.Count > 0;
    }

    public virtual List<Unit> GetPossibleAims()
    {
        possibleAims.RemoveAll(unit => unit.GetUnitState().GetStateType() == UnitStateType.IDLE);
        return possibleAims;
    }

    public void OnUnitDestroyed(Unit unit)
    {
        possibleAims.Remove(unit);
    }
}
