﻿using UnityEngine;
using System.Collections;

public class GlobalConstants 
{
    public static string RENDER_OBJECT_NAME = "Render";
    public static string PLAYER_SOLDIERS_CONTAINER = "PlayerSoldiers";
    public static string ENEMY_SOLDIERS_CONTAINER = "EnemySoldiers";

    public static Vector2 PLAYER_ATTACK_DIRECTION = Vector2.right;
    public static Vector2 ENEMY_ATTACK_DIRECTION = Vector2.left;

    public static float leftFieldBorder = -40f;
    public static float rightFieldBorder = 40f;

    public static GameObject playerSpawner = null;
    public static SoldierSpawner playerSpawnerScript = null;
    public static GameObject enemySpawner = null;
    public static SoldierSpawner enemySpawnerScript = null;

    static GlobalConstants()
    {
        playerSpawner = GameObject.Find("SpawnPlayer");
        playerSpawnerScript = playerSpawner.GetComponent<SoldierSpawner>();
        enemySpawner = GameObject.Find("SpawnEnemy");
        enemySpawnerScript = enemySpawner.GetComponent<SoldierSpawner>();
    }
}
