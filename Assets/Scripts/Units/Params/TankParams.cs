using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "TankParams", menuName = "Unit/Tank")]
public class TankParams : UnitParams
{
    public float minShootError = 0.0f;
    public float maxShootError = 5.0f;
    public float explosionRadius = 3.0f;

    public GameObject explosionPrefab = null;
}
