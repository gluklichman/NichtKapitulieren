using UnityEngine;
using System.Collections.Generic;

public class ShootPlayerState : BaseUnitState
{
    private bool shootPerformed = false;

    public ShootPlayerState(Unit unit) : base(UnitStateType.SHOOT, unit)
    {
        //Shoot();
    }

    private void Shoot()
    {
        List<Unit> possibleAims = unit.aimComponent.GetPossibleAims();
        if (possibleAims.Count > 0)
        {
            System.Random rand = new System.Random();
            int index = rand.Next(possibleAims.Count - 1);

            Unit aim = possibleAims[index];
            
            Vector2 aimCenter = aim.transform.position;
            float radius = 1f;
            Vector2 point = Random.insideUnitCircle * radius + aimCenter;
            if (aim.unitCollider.bounds.Contains(point))
            {
                aim.DestroyUnit();
                //possibleAims.RemoveAt(index);
            }
        }

        if (possibleAims.Count > 0)
        {
            unit.SetState(new WaitForShootPlayerState(unit), this);
        }
        else
        {
            unit.SetState(new RunUnitState(unit), this);
        }
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();
        if (!shootPerformed)
        {
            Shoot();
            shootPerformed = false;
        }
    }
}
