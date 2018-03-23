using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "PlayerControlsConfig", menuName = "PlayerControls")]
public class PlayerControlsConfig : ScriptableObject
{
    [Header("Energy")]
    public int initialEnergy = 0;
    public int maxEnergy = 0;
    public float oneEnergyRestoreTime = 0;

    [Header("SpellsConfig")]
    public int energyForSoldiers = 0;
    public int energyForTank = 0;
}
