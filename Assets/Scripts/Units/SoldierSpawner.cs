﻿using UnityEngine;
using System.Collections;

public class SoldierSpawner : MonoBehaviour
{
    public UnitOwner Owner = UnitOwner.PLAYER;

    public float periodBetweenUnits = 1.0f;
    public float waveLength = 5.0f;
    public float timeBetweenWaves = 5.0f;

    private bool isPlayerWave = true;
	private bool isEnemyWave = true;

	private AudioSource audioSource;

	private float lastSpawnTime = 0;
    private float lastWaveTime = 0;

    public float spawnPositionX = 0;
    [SerializeField]
    private float spawnAreaTop = 0;
    [SerializeField]
    private float spawnAreaBottom = 0;

    private Transform playerUnitsContainer = null;
    private Transform enemyUnitsContainer = null;

    // Use this for initialization
    void Start()
    {
        playerUnitsContainer = GameObject.Find(GlobalConstants.PLAYER_SOLDIERS_CONTAINER).transform;
        enemyUnitsContainer = GameObject.Find(GlobalConstants.ENEMY_SOLDIERS_CONTAINER).transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0)
            && Owner == UnitOwner.PLAYER)
        {
            SpawnTank();
        }

		if (Owner == UnitOwner.PLAYER)
		{
			if (isPlayerWave)
			{
				if (Time.timeSinceLevelLoad - lastSpawnTime > periodBetweenUnits)
				{
					SpawnSoldier();
					lastSpawnTime = Time.timeSinceLevelLoad;
				}
				if (Time.timeSinceLevelLoad - lastWaveTime > waveLength)
				{
					isPlayerWave = false;
					lastWaveTime = Time.timeSinceLevelLoad;
				}
			}
			else
			{
				if (Time.timeSinceLevelLoad - lastWaveTime > timeBetweenWaves || Input.GetKeyDown("space"))
				{
					lastWaveTime = Time.timeSinceLevelLoad;
					lastSpawnTime = Time.timeSinceLevelLoad - periodBetweenUnits;
					isPlayerWave = true;
					audioSource = gameObject.GetComponent<AudioSource>();
					audioSource.Play();
				}
			}
		}
		else {
			if (isEnemyWave)
			{
				if (Time.timeSinceLevelLoad - lastSpawnTime > periodBetweenUnits)
				{
					SpawnSoldier();
					lastSpawnTime = Time.timeSinceLevelLoad;
				}
				if (Time.timeSinceLevelLoad - lastWaveTime > waveLength)
				{
					isEnemyWave = false;
					lastWaveTime = Time.timeSinceLevelLoad;
				}
			}
			else
			{
				if (Time.timeSinceLevelLoad - lastWaveTime > timeBetweenWaves)
				{
					lastWaveTime = Time.timeSinceLevelLoad;
					lastSpawnTime = Time.timeSinceLevelLoad - periodBetweenUnits;
					isEnemyWave = true;
				}
			}
		}
    }

    private void SpawnSoldier()
    {
        Unit instance = UnitPool.Instance.GetUnitFromPool();
        instance.InitUnit(Owner);
        instance.GetComponent<BoxCollider2D>().enabled = true;

        float posY = Random.Range(spawnAreaBottom, spawnAreaTop);
        instance.transform.position = new Vector3(spawnPositionX, posY, 0);

        if (Owner == UnitOwner.PLAYER)
        {
            instance.transform.SetParent(playerUnitsContainer);
        }
        else
        {
            instance.transform.SetParent(enemyUnitsContainer);
        }
    }

    void OnDrawGizmosSelected()
    {        
        Gizmos.color = Color.red;
        float height = spawnAreaTop - spawnAreaBottom;
        float center = spawnAreaBottom + height / 2;
        Gizmos.DrawWireCube(new Vector3(spawnPositionX, center, 0), new Vector3(1, height, 1));       
    }

    void SpawnTank()
    {
        Unit instance = UnitPool.Instance.GetTankFromPool();
        instance.InitUnit(Owner);
        instance.GetComponent<BoxCollider2D>().enabled = true;
        
        float posY = Random.Range(spawnAreaBottom, spawnAreaTop);
        instance.transform.position = new Vector3(spawnPositionX, posY, 0);

        if (Owner == UnitOwner.PLAYER)
        {
            instance.transform.SetParent(playerUnitsContainer);
        }
        else
        {
            instance.transform.SetParent(enemyUnitsContainer);
        }
    }
}
