﻿using UnityEngine;
using System.Collections;

public class SoldierSpawner : MonoBehaviour
{
    public UnitOwner Owner = UnitOwner.PLAYER;

    public float periodBetweenUnits = 1.0f;
    public float waveLength = 5.0f;
    public float timeBetweenWaves = 5.0f;

    private bool isWave = true;

    private float lastSpawnTime = 0;
    private float lastWaveTime = 0;

    [SerializeField]
    private float spawnPositionX = 0;
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
        if (isWave)
        {
            if (Time.timeSinceLevelLoad - lastSpawnTime > periodBetweenUnits)
            {
                SpawnSoldier();
                lastSpawnTime = Time.timeSinceLevelLoad;
            }
            if (Time.timeSinceLevelLoad - lastWaveTime > waveLength)
            {
                isWave = false;
                lastWaveTime = Time.timeSinceLevelLoad;
            }
        }
        else
        {
            if (Time.timeSinceLevelLoad - lastWaveTime > waveLength)
            {
                lastWaveTime = Time.timeSinceLevelLoad;
                isWave = true;
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
}