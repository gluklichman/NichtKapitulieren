using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitOwner
{
    PLAYER,
    ENEMY
}

public class Unit : MonoBehaviour {

    [SerializeField]
    protected UnitOwner owner = UnitOwner.PLAYER;
    protected BaseUnitState unitState = null;

    [SerializeField]
    UnitParams unitParams = null;

	// Use this for initialization
	void Start () {
        Debug.Assert(unitParams);
        SetState(new RunUnitState(this), unitState);
	}
	
	// Update is called once per frame
	void Update () {
		if (unitState != null)
        {
            unitState.HandleUpdate();
        }
	}

    public void InitUnit(UnitOwner owner)
    {
        this.owner = owner;
        SetState(new RunUnitState(this), unitState);
    }

    public void SetState(BaseUnitState newState, BaseUnitState prevState)
    {
        if (prevState != unitState)
        {
            Debug.Assert(false);
        }
        if (unitState != null)
        {
            unitState.DestroyState();
        }
        unitState = newState;
    }

    public BaseUnitState GetUnitState()
    {
        return unitState;
    }

    public UnitOwner GetOwner()
    {
        return owner;
    }

    public UnitParams GetParams()
    {
        return unitParams;
    }
}
