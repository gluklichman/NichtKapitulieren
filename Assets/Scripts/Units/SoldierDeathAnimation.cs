using UnityEngine;
using System.Collections;

public class SoldierDeathAnimation : MonoBehaviour
{
    public UnitOwner owner = UnitOwner.ENEMY;

    private const float ROTATION_SPEED = 180;
    private float rotationTime = 0.5f;
    private float layTime = 0.5f;

    private Transform spriteTransform = null;
    private Transform bloodTransform = null;

    // Use this for initialization
    void Start()
    {
        spriteTransform = transform.Find("Sprite");
        bloodTransform = transform.Find("blood");
        bloodTransform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (rotationTime > 0)
        {
            float direction = (owner == UnitOwner.PLAYER) ? 1 : -1;
            spriteTransform.Rotate(0, 0, ROTATION_SPEED * Time.deltaTime * direction);
            rotationTime -= Time.deltaTime;
        }
        else if (layTime > 0)
        {
            bloodTransform.gameObject.SetActive(true);
            layTime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
