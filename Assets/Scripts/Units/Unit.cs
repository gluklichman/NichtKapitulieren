using System;
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

    public UnitAimComponent aimComponent = null;
    public BoxCollider2D unitCollider = null;

    public Action<Unit> unitDestroyed;

	// Use this for initialization
	void Awake () {
        unitCollider = GetComponent<BoxCollider2D>();
        Debug.Assert(unitParams);
        SetState(new RunUnitState(this), unitState);
	}
	
	// Update is called once per frame
	void Update () {
		if (unitState != null)
        {
            unitState.HandleUpdate();
        }
        if (transform.position.x < GlobalConstants.leftFieldBorder
            || transform.position.x > GlobalConstants.rightFieldBorder)
        {
            DestroyUnit();
        }
	}

    public void InitUnit(UnitOwner owner)
    {
        this.owner = owner;
        if (aimComponent == null)
        {
            aimComponent = gameObject.AddComponent<UnitAimComponent>();
        }
        aimComponent.Init(this);

        if (owner == UnitOwner.PLAYER)
        {
            transform.Find("SpriteEnemy").gameObject.SetActive(false);
            transform.Find("SpritePlayer").gameObject.SetActive(true);
            transform.Find("SpritePlayer/shotFire").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("SpriteEnemy").gameObject.SetActive(true);
            transform.Find("SpriteEnemy/shotFire").gameObject.SetActive(false);
            transform.Find("SpritePlayer").gameObject.SetActive(false);
        }

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

    public void DestroyUnit()
    {
        if (unitDestroyed != null)
        {
            unitDestroyed(this);
        }
        unitState = null;
        aimComponent.Deinit();
        UnitPool.Instance.ReturnUnitToPool(this);
    }
}
