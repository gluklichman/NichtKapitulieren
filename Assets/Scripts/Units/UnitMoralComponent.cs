using UnityEngine;
using System.Collections;

public class UnitMoralComponent : MonoBehaviour
{
    public Unit unit = null;
    public float CurrentMorale = 0;
	public float MoraleCheckInterval = 0.3f;
	public float BolkonskyModeDuration = 10.0f;

    bool moralBreak = false;
	bool BolkonskyModeOn = false;

    [SerializeField]
    private float collectiveMorale = 0;
	private float lastMoraleCheckTime = 0;
	private float BolkonskyModeStartTime = 0;

	// Use this for initialization
	void Awake()
    {
        unit = GetComponent<Unit>();
        CurrentMorale = unit.GetParams().baseMorale;
		
	}

	void Start()
	{
		lastMoraleCheckTime = Time.timeSinceLevelLoad;
	}

    //public void Deinit()
    //{

    //}

    // Update is called once per frame
    void Update()
    {
		if (Time.timeSinceLevelLoad - lastMoraleCheckTime < MoraleCheckInterval)
		{
			return;
		}
		if (!BolkonskyModeOn)
		{
			collectiveMorale = 0;
			RaycastHit2D[] raycastHits = Physics2D.CircleCastAll(transform.position, unit.GetParams().moralCheckRadius, Vector2.zero, 0);
			foreach (RaycastHit2D hit in raycastHits)
			{
				if (hit.collider == null)
				{
					continue;
				}
				UnitMoralComponent otherUnit = hit.collider.GetComponent<UnitMoralComponent>();
				if (otherUnit == null
					|| otherUnit == this
					|| hit.collider != otherUnit.unit.unitCollider)
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
			lastMoraleCheckTime = Time.timeSinceLevelLoad;
		}
		else
		{
			if(Time.timeSinceLevelLoad - BolkonskyModeStartTime > BolkonskyModeDuration)
			{
				BolkonskyModeOn = false;
				CurrentMorale = unit.GetParams().baseMorale;
				unit.SetHP(1);
				if (unit.GetOwner() == UnitOwner.PLAYER)
				{
					transform.Find("SpritePlayer/flag").gameObject.SetActive(false);
				}
				else
				{
					transform.Find("SpriteEnemy/flag").gameObject.SetActive(false);
				}
				//unit.soldierSprite.SwitchImage(unit.soldierSprite.runSprite);
			}
		}
    }

    void SwitchMoralBreak(float collectiveMoral)
    {
        if (collectiveMoral <= unit.GetParams().moralBreakValue
            && !moralBreak)
        {
			float rnd = Random.Range(0f, 1.0f);
			if (rnd < 0.95f)
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
		CurrentMorale = 1000;
		BolkonskyModeOn = true;
		BolkonskyModeStartTime = Time.timeSinceLevelLoad;
		unit.SetHP(5);
		
		if (unit.GetOwner() == UnitOwner.PLAYER)
		{
			transform.Find("SpritePlayer/flag").gameObject.SetActive(true);
		}
		else
		{
			transform.Find("SpriteEnemy/flag").gameObject.SetActive(true);
		}
		//unit.soldierSprite.SwitchImage(unit.soldierSprite.bolkonskySprite);

	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color(1, 1, 0, 0.75F);
		Gizmos.DrawSphere(transform.position, unit.GetParams().moralCheckRadius);
	}

}
