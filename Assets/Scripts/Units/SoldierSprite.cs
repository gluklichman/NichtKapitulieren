using UnityEngine;
using System.Collections;

public class SoldierSprite : MonoBehaviour
{
    public Sprite runSprite = null;
    public Sprite shootSprite = null;

    public void SwitchImage(Sprite sprite)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
