using UnityEngine;
using System.Collections;

public class RunUnitState : BaseUnitState
{
	private float moveSpeed = 0;

    public RunUnitState(Unit unit) : base(UnitStateType.RUN, unit)
    {
        unit.soldierSprite.SwitchImage(unit.soldierSprite.runSprite);
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
            return;
        }

        CheckForWinCondition();
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

    private void CheckForWinCondition()
    {
        if (unit.GetOwner() == UnitOwner.PLAYER
            && unit.transform.position.x > GlobalConstants.enemySpawnerScript.spawnPositionX)
        {
            GlobalConstants.enemySpawner.GetComponent<AreaHitComponent>().DealDamage(1);
            GameObject.Destroy(unit);
            return;
        }

        if (unit.GetOwner() == UnitOwner.ENEMY
            && unit.transform.position.x < GlobalConstants.playerSpawnerScript.spawnPositionX)
        {
            GlobalConstants.playerSpawner.GetComponent<AreaHitComponent>().DealDamage(1);
            GameObject.Destroy(unit);
            return;
        }
    }
}
