using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "UnitParams", menuName = "Unit/Params")]
public class UnitParams : ScriptableObject
{
    public float moveSpeed = 0;
    public float aimRadius = 0;

    public float waitForShootMinTime = 0;
    public float waitForShootMaxTime = 0.5f;
    public float GetRandomWaitForShootTime()
    {
        return Random.Range(waitForShootMinTime, waitForShootMaxTime);
    }

    public float hitRadius = 1;
}
