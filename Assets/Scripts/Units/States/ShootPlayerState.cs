using UnityEngine;
using System.Collections.Generic;

public class ShootPlayerState : BaseUnitState
{
    private bool shootPerformed = false;
	private AudioSource audioSource;
	private ShotSounds am;
	private GameObject audioSourceObject;

	private float waitAfterShoot = 0.05f;

    public ShootPlayerState(Unit unit) : base(UnitStateType.SHOOT, unit)
    {
        unit.soldierSprite.SwitchImage(unit.soldierSprite.shootSprite);
        GetShotFire().SetActive(true);
    }

    private void Shoot()
    {
        List<Unit> possibleAims = unit.aimComponent.GetPossibleAims();
        BaseDamageComponent damage = unit.GetDamageComponent();
        damage.DealDamage(possibleAims, unit);
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();
        if (!shootPerformed)
        {
            Shoot();
            shootPerformed = true;

			audioSourceObject = GameObject.Find("Audio_ShotSounds");
			audioSource = audioSourceObject.GetComponent<AudioSource>();
			am = audioSourceObject.GetComponent<ShotSounds>();

			if (!audioSource.isPlaying)
			{
				int rndIndex = Random.Range(0, am.shotSounds.Length - 1);
				audioSource.clip = am.shotSounds[rndIndex];
				audioSource.Play();
			}
		}
        else
        {
            waitAfterShoot -= Time.deltaTime;
            if (waitAfterShoot <= 0)
            {
                List<Unit> possibleAims = unit.aimComponent.GetPossibleAims();
                GetShotFire().SetActive(false);
                if (possibleAims.Count > 0)
                {
                    unit.SetState(new WaitForShootPlayerState(unit), this);
                }
                else
                {    
                    unit.SetState(new RunUnitState(unit), this);
                }
            }
        }
    }

    private GameObject GetShotFire()
    {
        if (unit.GetOwner() == UnitOwner.PLAYER)
        {
            return unit.transform.Find("SpritePlayer/shotFire").gameObject;
        }
        else
        {
            return unit.transform.Find("SpriteEnemy/shotFire").gameObject;
        }
    }
}
