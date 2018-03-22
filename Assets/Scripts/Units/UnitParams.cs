using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "UnitParams", menuName = "Unit/Params")]
public class UnitParams : ScriptableObject
{
    public float minSpeed = 0f;
	public float maxSpeed = 0f;
    public float aimRadius = 0;
}
