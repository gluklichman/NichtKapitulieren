using UnityEngine;

public enum UnitStateType
{
    RUN,
    SHOOT,
    SHOOT_WAIT,
    DEAD
}

public class BaseUnitState
{
    protected UnitStateType stateType = UnitStateType.RUN;
    protected Unit unit = null;

    public BaseUnitState(UnitStateType stateType, Unit unit)
    {
        Debug.Assert(unit);
        this.unit = unit;
        this.stateType = stateType;
    }

    public virtual void DestroyState()
    { }

    public virtual void HandleUpdate()
    { }

    public UnitStateType GetStateType()
    {
        return stateType;
    }


}
