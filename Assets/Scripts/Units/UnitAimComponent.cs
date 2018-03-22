using UnityEngine;
using System.Collections.Generic;

public class UnitAimComponent : MonoBehaviour
{
    private CircleCollider2D aimRadius = null;
    private Unit unit = null;

    [SerializeField]
    private List<Unit> possibleAims = null;

    // Use this for initialization
    void Awake()
    {

    }

    public void Init(Unit unit)
    {
        this.unit = unit;
        Debug.Assert(aimRadius == null);

        if (aimRadius == null)
        {
            aimRadius = gameObject.AddComponent<CircleCollider2D>();
        }

        aimRadius.enabled = true;
        aimRadius.isTrigger = true;
        aimRadius.radius = unit.GetParams().aimRadius;

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
            && collision == otherUnit.unitCollider)
        {
            possibleAims.Add(otherUnit);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Unit otherUnit = collision.GetComponent<Unit>();
        possibleAims.Remove(otherUnit);
    }
}
