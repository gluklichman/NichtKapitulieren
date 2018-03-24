using UnityEngine;

[CreateAssetMenu(fileName = "AirplaneParams", menuName = "Unit/Airplane")]
public class AirplaneParams : ScriptableObject
{
    public float flightSpeed = 45;
    public int explosionDamage = 25;
    public float explosionRadius = 10;

    public GameObject explosionPrefab;
}
