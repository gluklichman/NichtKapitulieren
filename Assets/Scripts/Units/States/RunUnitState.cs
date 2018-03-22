using UnityEngine;
using System.Collections;

public class RunUnitState : BaseUnitState
{
    public RunUnitState(Unit unit) : base(UnitStateType.RUN, unit)
    { }

    public override void HandleUpdate()
    {
        base.HandleUpdate();
        Vector2 direction = GetMoveDirection();
        float moveSpeed = unit.GetParams().moveSpeed;
        unit.transform.Translate(direction * moveSpeed * Time.deltaTime, 0);
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
