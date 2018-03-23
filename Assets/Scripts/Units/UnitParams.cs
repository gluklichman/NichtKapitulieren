using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "UnitParams", menuName = "Unit/Params")]
public class UnitParams : ScriptableObject
{
    public float minSpeed = 0f;
	public float maxSpeed = 0f;
    public float aimRadius = 0;

    public float waitForShootMinTime = 0;
    public float waitForShootMaxTime = 0.5f;
    public float GetRandomWaitForShootTime()
    {
        return Random.Range(waitForShootMinTime, waitForShootMaxTime);
    }

    public float hitRadius = 1;

    public GameObject deathPrefabPlayer;
    public GameObject deathPrefabEnemy;

    public float moralCheckRadius = 10.0f;
    public float baseMorale = 30;
    public float moralBreakValue = 10;
    public float moralBreakMorale = -25f;

    public float minMoralBreakSpeed = 1.5f;
    public float maxMoralBreakSpeed = 2.5f;

    public int hitpoints = 0;
    public int damage = 0;

}

[CreateAssetMenu(fileName = "UnitParams", menuName = "Unit/Tank")]
public class TankParams : UnitParams
{
    public float minShootError = 0.0f;
    public float maxShootError = 5.0f;
    public float explosionRadius = 3.0f;

    public GameObject explosionPrefab = null;
}

[CreateAssetMenu(fileName = "UnitParams", menuName = "Unit/Bazooka")]
public class BazookaParams : UnitParams
{
	public float minShootError = 0.0f;
	public float maxShootError = 5.0f;
	public float explosionRadius = 3.0f;

	public GameObject explosionPrefab = null;
}
