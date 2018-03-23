using UnityEngine;
using System.Collections;

public class AreaHitComponent : MonoBehaviour
{
    [SerializeField]
    private int hits = 20;

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
        if (hits <= 0)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        Time.timeScale = 0;
    }
}
