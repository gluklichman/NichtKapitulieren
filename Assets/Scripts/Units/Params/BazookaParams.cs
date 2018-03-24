using UnityEngine;


[CreateAssetMenu(fileName = "BazookaParams", menuName = "Unit/Bazooka")]
public class BazookaParams : UnitParams
{
    public float minShootError = 0.0f;
    public float maxShootError = 5.0f;
    public float explosionRadius = 3.0f;

    public GameObject explosionPrefab = null;
}
