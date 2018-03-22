using UnityEngine;
using System.Collections;

public class RunUnitState : BaseUnitState
{
	private float moveSpeed = 0;

    public RunUnitState(Unit unit) : base(UnitStateType.RUN, unit)
    {		
		moveSpeed = unit.GetParams().minSpeed + Random.Range((float)unit.GetParams().minSpeed, (float)unit.GetParams().maxSpeed);
	}



    public override void HandleUpdate()
    {
        base.HandleUpdate();
        Vector2 direction = GetMoveDirection();
        unit.transform.Translate(direction * moveSpeed * Time.deltaTime, 0);

        if (unit.aimComponent.HasAvailableAims())
        {
            unit.SetState(new WaitForShootPlayerState(unit), this);
        }
    }

    private Vector2 GetMoveDirection()
    {
        if (unit.GetOwner() == UnitOwner.ENEMY)
        {
            return GlobalConstants.ENEMY_ATTACK_DIRECTION;
        }
        else
        {
            return GlobalConstants.PLAYER_ATTACK_DIRECTION;
        }
    }
}
