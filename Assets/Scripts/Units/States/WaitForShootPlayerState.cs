using UnityEngine;
using System.Collections;

public class WaitForShootPlayerState : BaseUnitState
{
    private float waitStartTime = 0;
    private float waitTime = 0;

    public WaitForShootPlayerState(Unit unit) : base(UnitStateType.SHOOT_WAIT, unit)
    {
        unit.soldierSprite.SwitchImage(unit.soldierSprite.shootSprite);
        waitStartTime = Time.timeSinceLevelLoad;
        waitTime = unit.GetParams().GetRandomWaitForShootTime();
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();
        if (Time.timeSinceLevelLoad - waitStartTime > waitTime)
        {
            unit.SetState(new ShootPlayerState(unit), this);
        }
    }
}
