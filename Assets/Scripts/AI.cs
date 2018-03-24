using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour
{
    private PlayerControls controls = null;

    private enum AwaitingUnit
    {
        GRENADIER,
        SOLDIER,
        TANK,
        AIRPLANE
    }

    private AwaitingUnit currentUnit = AwaitingUnit.GRENADIER;

    // Use this for initialization
    void Start()
    {
        controls = GetComponent<PlayerControls>();
        controls.aiDriven = true;
        GenerateAwaitingUnit();
    }

    private void GenerateAwaitingUnit()
    {
        System.Random rnd = new System.Random();

        string[] names = System.Enum.GetNames(typeof(AwaitingUnit));
        int unitIndex = rnd.Next(names.Length - 1);
        currentUnit = (AwaitingUnit)System.Enum.Parse(typeof(AwaitingUnit), names[unitIndex]);
        Debug.Log("Ai select unit: " + currentUnit);
    }

    // Update is called once per frame
    void Update()
    {
        if (TryGenerateUnit())
        {
            GenerateAwaitingUnit();
        }
    }

    private bool TryGenerateUnit()
    {
        if (currentUnit == AwaitingUnit.SOLDIER)
        {
            return controls.TrySpawnSoldiers();
        }
        else if (currentUnit == AwaitingUnit.GRENADIER)
        {
            return controls.TrySpawnGrenadiers();
        }
        else if (currentUnit == AwaitingUnit.TANK)
        {
            return controls.TrySpawnTank();
        }
        else if (currentUnit == AwaitingUnit.AIRPLANE)
        {
            return controls.TrySpawnAirplane();
        }
        return false;
    }
}
