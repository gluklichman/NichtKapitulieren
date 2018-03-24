using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    [Header("Config and scripts")]
    [SerializeField]
    private PlayerControlsConfig config = null;
    [SerializeField]
    private SoldierSpawner spawner = null;

    [Header("UI")]
    [SerializeField]
    private Image progressBar = null;
    [SerializeField]
    private bool invertProgress = false;
    [SerializeField]
    private Image[] abilityIcons = null;

    private int currentEnergy = 0;
    private float lastAddEnergyTime = 0;

    private int[] abilityCosts = null;

    public KeyCode soldiersButton;
    public KeyCode tankButton;
    public KeyCode grenadiersButton;
    public KeyCode airplaneButton;

    // Use this for initialization
    void Start()
    {
        Debug.Assert(config != null);
        Debug.Assert(spawner != null);

        abilityCosts = new int[]
        {
            config.energyForSoldiers,
            config.energyForTank,
            config.energyForGrenadiers,
            config.energyForAirplane
        };

        currentEnergy = config.initialEnergy;
        lastAddEnergyTime = Time.timeSinceLevelLoad;
        SetupAbilities();
        UpdateProgress();
    }

    void SetupAbilities()
    {
        for (int i=0; i<abilityCosts.Length; ++i)
        {
            Image abilityImage = abilityIcons[i];
            float width = progressBar.rectTransform.rect.width;
            float widthPercent = (float)abilityCosts[i] / config.maxEnergy;
            int multiplier = invertProgress ? -1 : 1;
            abilityImage.rectTransform.localPosition = new Vector3(width * widthPercent * multiplier, -12, 0);
            abilityImage.color = Color.black;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentEnergy >= config.maxEnergy)
        {
            lastAddEnergyTime = Time.timeSinceLevelLoad;
        }
        else
        {
            float delta = Time.timeSinceLevelLoad - lastAddEnergyTime;
            if (delta >= config.oneEnergyRestoreTime)
            {
                lastAddEnergyTime = Time.timeSinceLevelLoad;
                currentEnergy++;
                UpdateProgress();
            }
        }

		if (Input.GetKeyDown(soldiersButton))
		 {
			 TrySpawnSoldiers();
		 }
		 else
		if (Input.GetKeyDown(tankButton))
        {
            TrySpawnTank();
        }
        else if (Input.GetKeyDown(grenadiersButton))
        {
            TrySpawnGrenadiers();
        }
        else if (Input.GetKeyDown(airplaneButton))
        {
            TrySpawnAirplane();
        }
    }

    private void UpdateProgress()
    {
        float fillAmount = (float)currentEnergy / (float)config.maxEnergy;
        progressBar.fillAmount = fillAmount;
        for (int i=0; i<abilityCosts.Length; ++i)
        {
            Image abilityIcon = abilityIcons[i];
            if (currentEnergy < abilityCosts[i])
            {
                abilityIcon.color = Color.black;
            }
            else
            {
                abilityIcon.color = Color.white;
            }
        }
    }

    private void TrySpawnTank()
    {
        if (currentEnergy < config.energyForTank)
        {
            return;
        }
        currentEnergy -= config.energyForTank;
        spawner.SpawnTank();
        UpdateProgress();
    }

    private void TrySpawnSoldiers()
    {
        if (currentEnergy < config.energyForSoldiers)
        {
            return;
        }
        currentEnergy -= config.energyForSoldiers;
        UpdateProgress();
        spawner.StartSpawnSoldiers();
    }

    private void TrySpawnGrenadiers()
    {
        if (currentEnergy < config.energyForGrenadiers)
        {
            return;
        }
        currentEnergy -= config.energyForGrenadiers;
        UpdateProgress();
        spawner.SpawnGrenadiers(config.grenadiersCount);
    }

    private void TrySpawnAirplane()
    {
        if (currentEnergy < config.energyForAirplane)
        {
            return;
        }
        currentEnergy -= config.energyForAirplane;
        UpdateProgress();
        spawner.SpawnAirplane();
    }
}
