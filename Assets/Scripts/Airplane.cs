using UnityEngine;
using System.Collections;

public class Airplane : MonoBehaviour
{
    private const float MIN_X = -55;
    private const float MAX_X = 55;

    //private const float FLIGHT_SPEED = 45;
    //private const int EXPLOSION_DAMAGE = 25;
    //private const float EXPLOSION_RADIUS = 10;

    private UnitOwner owner = UnitOwner.PLAYER;
    public AirplaneParams config = null;

    // Use this for initialization
    void Start()
    {
        
    }

    public void Init(UnitOwner owner)
    {
        this.owner = owner;
        float x = (owner == UnitOwner.PLAYER) ? MIN_X : MAX_X;
        transform.position = new Vector3(x, -3, -5);
        if (owner == UnitOwner.ENEMY)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float dx = config.flightSpeed * Time.deltaTime;

        if (owner == UnitOwner.PLAYER)
        {
            transform.position = transform.position + new Vector3(dx, 0, 0);
        }
        else
        {
            transform.position = transform.position + new Vector3(-dx, 0, 0);
        }

        if (transform.position.x > MAX_X
            || transform.position.x < MIN_X)
        {
            //UnitPool.Instance.ReturnAirplane(this);
            StartExplosion();
        }
    }

    private void StartExplosion()
    {
        Vector2 explosionCenter = Vector2.zero;
        RaycastHit2D[] explosionTargets = Physics2D.CircleCastAll(explosionCenter, config.explosionRadius, Vector2.zero);
        int damaged = 0;
        Debug.Log("Possible targets: " + explosionTargets.Length);
        foreach (RaycastHit2D hit in explosionTargets)
        {
            if (hit.collider == null)
            {
                continue;
            }
            Unit target = hit.collider.GetComponent<Unit>();
            if (target == null
                || hit.collider != target.unitCollider)
            {
                continue;
            }

            damaged++;
            target.DealDamage(config.explosionDamage);
        }
        Debug.Log(damaged);

        GameObject explosionInstance = GameObject.Instantiate(config.explosionPrefab) as GameObject;
        explosionInstance.transform.position = explosionCenter;

        UnitPool.Instance.ReturnAirplane(this);
    }
}
