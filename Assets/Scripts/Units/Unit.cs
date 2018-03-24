using System;
using System.Collections.Generic;
using UnityEngine;

public enum UnitOwner
{
    PLAYER,
    ENEMY
}

public enum UnitType
{
    SOLDIER,
	BAZOOKA,
    TANK
}

public class Unit : MonoBehaviour {

    public static int ID = 0;
    public int uniqueID = 0;

    [SerializeField]
    protected UnitOwner owner = UnitOwner.PLAYER;
    protected BaseUnitState unitState = null;

    [SerializeField]
    UnitParams unitParams = null;

    public UnitAimComponent aimComponent = null;
    public UnitMoralComponent moralComponent = null;
    public SoldierSprite soldierSprite = null;
    public BoxCollider2D unitCollider = null;

    public Action<Unit> unitDestroyed;

    [SerializeField]
    private int hitpoint = 0;

	// Use this for initialization
	void Awake () {
        unitCollider = GetComponent<BoxCollider2D>();
        Debug.Assert(unitParams);
        //SetState(new RunUnitState(this), unitState);

        uniqueID = ID;
        ID++;
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
            DestroyUnit(false);
        }
	}

    public void InitUnit(UnitOwner owner)
    {
        this.owner = owner;

        CreateAimComponent();
        aimComponent.Init(this);
        moralComponent = gameObject.AddComponent<UnitMoralComponent>();

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
        soldierSprite = transform.GetComponentInChildren<SoldierSprite>();
        hitpoint = GetParams().hitpoints;

        SetState(new RunUnitState(this), unitState);
    }

    protected virtual void CreateAimComponent()
    {
        if (aimComponent == null)
        {
            aimComponent = gameObject.AddComponent<UnitAimComponent>();
        }
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

    public void DealDamage(int damage)
    {
        hitpoint -= damage;
        if (hitpoint <= 0)
        {
            DestroyUnit(true);
        }
    }

	public void SetHP(int hp)
	{
		hitpoint = hp;
	}

    public void DestroyUnit(bool animation)
    {
        if (unitDestroyed != null)
        {
            unitDestroyed(this);
        }
        unitState = null;
        aimComponent.Deinit();
        Destroy(moralComponent);

        if (animation)
        {
            CreateDeathAnimation();
        }

        DestroyInstance();
        //UnitPool.Instance.ReturnUnitToPool(this);
    }

    protected virtual void DestroyInstance()
    {
        UnitPool.Instance.ReturnUnitToPool(this);
    }

    private void CreateDeathAnimation()
    {
        GameObject deathPrefab = (owner == UnitOwner.PLAYER) ? GetParams().deathPrefabPlayer : GetParams().deathPrefabEnemy;
        GameObject instance = Instantiate(deathPrefab) as GameObject;
        instance.transform.position = transform.position;
        instance.transform.Translate(0, 0, 1);
    }

    public virtual BaseDamageComponent GetDamageComponent()
    {
        return new SimpleDamage();
    }

    public virtual UnitType GetUnitType()
    {
        return UnitType.SOLDIER;
    }
}
