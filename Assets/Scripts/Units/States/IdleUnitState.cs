using UnityEngine;
using System.Collections;

public class IdleUnitState : BaseUnitState
{
    private const float ROTATION_SPEED = 180;
    private float rotationTime = 0.5f;
    private float layTime = 0.5f;

    public IdleUnitState(Unit unit) : base(UnitStateType.IDLE, unit)
    {
        unit.unitCollider.enabled = false;
        unit.aimComponent.enabled = false;
        unit.transform.Find("SpriteEnemy/shotFire").gameObject.SetActive(false);
        unit.transform.Find("SpritePlayer/shotFire").gameObject.SetActive(false);

    }
}
