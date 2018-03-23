using UnityEngine;
using System.Collections;

public class MoralBreakUnitState : BaseUnitState
{
    private float moveSpeed = 0;

    public MoralBreakUnitState(Unit unit) : base(UnitStateType.MORAL_BREAK, unit)
    {
        unit.soldierSprite.SwitchImage(unit.soldierSprite.runSprite);
        moveSpeed = Random.Range(unit.GetParams().minMoralBreakSpeed, unit.GetParams().maxMoralBreakSpeed);
        unit.transform.Rotate(0, 180, 0);
    }

    public override void DestroyState()
    {
        base.DestroyState();
        unit.transform.Rotate(0, 180, 0);
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();
        Vector2 direction = GetMoveDirection();
        unit.transform.Translate(direction * moveSpeed * Time.deltaTime, 0);
    }

    private Vector2 GetMoveDirection()
    {
        if (unit.GetOwner() == UnitOwner.ENEMY)
        {
            return -GlobalConstants.ENEMY_ATTACK_DIRECTION;
        }
        else
        {
            return -GlobalConstants.PLAYER_ATTACK_DIRECTION;
        }
    }
}
