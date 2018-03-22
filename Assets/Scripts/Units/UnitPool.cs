using UnityEngine;
using System.Collections.Generic;

public class UnitPool : MonoBehaviour
{
    private static UnitPool _instance = null;
    public static UnitPool Instance
    {
        get
        {
            return _instance;
        }
    }

    private List<Unit> _pool = new List<Unit>();
    private const float POOL_SIZE = 100;
    private const float MAX_POOL_SIZE = 200;
    private static Vector3 POOL_POSITION = new Vector3(-1000, -1000);

    public GameObject UnitPrefab = null;

    // Use this for initialization
    void Awake()
    {
        _instance = this;
        PreparePools();
    }

    private void PreparePools()
    {
        for (int i=0; i<POOL_SIZE; ++i)
        {
            GameObject instance = Instantiate(UnitPrefab) as GameObject;
            Unit unit = instance.GetComponent<Unit>();
            unit.unitCollider.enabled = false;
            unit.enabled = false;
            instance.transform.position = POOL_POSITION;
            instance.transform.SetParent(transform);
            _pool.Add(instance.GetComponent<Unit>());
        }
    }

    public Unit GetUnitFromPool()
    {
        if (_pool.Count != 0)
        {
            Unit unit = _pool[_pool.Count - 1];
            _pool.RemoveAt(_pool.Count - 1);
            unit.enabled = true;
            unit.transform.SetParent(null);
            return unit;
        }
        else
        {
            GameObject instance = Instantiate(UnitPrefab) as GameObject;
            return instance.GetComponent<Unit>();
        }
    }

    public void ReturnUnitToPool(Unit unit)
    {
        unit.unitCollider.enabled = false;
        unit.enabled = false;
        
        if (_pool.Count <= MAX_POOL_SIZE)
        {
            unit.transform.position = POOL_POSITION;
            unit.transform.SetParent(transform);
            _pool.Add(unit);
        }
        else
        {
            Destroy(unit.gameObject);
        }
        
    }
}
