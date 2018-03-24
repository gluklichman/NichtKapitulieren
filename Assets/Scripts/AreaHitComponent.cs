using UnityEngine;
using System;

public class AreaHitComponent : MonoBehaviour
{
    [SerializeField]
    private int hits = 20;

    public Action damageDealt;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DealDamage(int damage)
    {
        hits -= damage;
        if (damageDealt != null)
        {
            damageDealt();
        }
        if (hits <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        EndGameScreen.ShowEndGameScreen();
    }
}
