using UnityEngine;
using System.Collections;

public class UnitMoralComponent : MonoBehaviour
{
    public Unit unit = null;
    public float CurrentMorale = 0;
	public float MoraleCheckInterval = 1.0f;

    bool moralBreak = false;

    [SerializeField]
    private float collectiveMorale = 0;
	private float lastMoraleCheckTime = 0;

	// Use this for initialization
	void Awake()
    {
        unit = GetComponent<Unit>();
        CurrentMorale = unit.GetParams().baseMorale;
    }

    //public void Deinit()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
        collectiveMorale = 0;
        RaycastHit2D[] raycastHits = Physics2D.CircleCastAll(transform.position, unit.GetParams().moralCheckRadius, Vector2.zero, 0);
        foreach(RaycastHit2D hit in raycastHits)
        {
            if (hit.collider == null)
            {
                continue;
            }
            UnitMoralComponent otherUnit = hit.collider.GetComponent<UnitMoralComponent>();
            if (otherUnit == null
                || otherUnit == this)
            {
                continue;
            }
            if (otherUnit.unit.GetOwner() == unit.GetOwner())
            {
                collectiveMorale += otherUnit.CurrentMorale;
            }
            else
            {
                collectiveMorale -= otherUnit.CurrentMorale;
            }
        }

        SwitchMoralBreak(collectiveMorale);
    }

    void SwitchMoralBreak(float collectiveMoral)
    {
        if (collectiveMoral <= unit.GetParams().moralBreakValue
            && !moralBreak)
        {
			float rnd = Random.Range(0f, 1.0f);
			if (rnd < 0.5f)
			{
				StartMoralBreak();
			}
			else
			{
				StartAndreyBolkonskyMode();
			}
            return;
        }
        if (collectiveMoral > unit.GetParams().moralBreakValue
            && moralBreak)
        {
            StopMoralBreak();
            return;
        }
    }

    private void StartMoralBreak()
    {
        moralBreak = true;
        CurrentMorale = unit.GetParams().moralBreakMorale;
        unit.SetState(new MoralBreakUnitState(unit), unit.GetUnitState());
    }

    private void StopMoralBreak()
    {
        moralBreak = false;
        CurrentMorale = unit.GetParams().baseMorale;
        unit.SetState(new RunUnitState(unit), unit.GetUnitState());
    }

	private void StartAndreyBolkonskyMode()
	{
		CurrentMorale = 200;
		if (unit.GetOwner() == UnitOwner.PLAYER)
		{
			transform.Find("SpritePlayer").GetComponent<SpriteRenderer>().color = Color.black;
		}
		else
		{
			transform.Find("SpriteEnemy").GetComponent<SpriteRenderer>().color = Color.black;
		}

	}

}
