using UnityEngine;
using System.Collections;

public class DeathUnitState : BaseUnitState
{
    private const float ROTATION_SPEED = 180;
    private float rotationTime = 0.5f;
    private float layTime = 0.5f;

    public DeathUnitState(Unit unit) : base(UnitStateType.DEAD, unit)
    {
        unit.unitCollider.enabled = false;
        unit.aimComponent.enabled = false;
        unit.transform.Find("SpriteEnemy/shotFire").gameObject.SetActive(false);
        unit.transform.Find("SpritePlayer/shotFire").gameObject.SetActive(false);

    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();
        /*if (rotationTime > 0)
        {
            float direction = unit.GetOwner() == UnitOwner.PLAYER ? 1 : -1;
            unit.transform.Rotate(0, 0, ROTATION_SPEED * Time.deltaTime * direction);
            rotationTime -= Time.deltaTime;
        }
        else if (layTime > 0)
        {
            layTime -= Time.deltaTime;
        }
        else
        {
            unit.DestroyUnit();
        }*/
        
    }
}
