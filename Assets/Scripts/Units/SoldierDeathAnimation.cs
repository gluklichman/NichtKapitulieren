using UnityEngine;
using System.Collections;

public class SoldierDeathAnimation : MonoBehaviour
{
    public UnitOwner owner = UnitOwner.ENEMY;

    private const float ROTATION_SPEED = 180;
    private float rotationTime = 0.5f;
    private float layTime = 0.5f;

	private AudioSource audioSource;
	private GameObject audioSourceObject;
	private AudioManager am;

	private Transform spriteTransform = null;
    private Transform bloodTransform = null;

    // Use this for initialization
    void Start()
    {
        spriteTransform = transform.Find("Sprite");
        bloodTransform = transform.Find("blood");
        bloodTransform.gameObject.SetActive(false);

		audioSourceObject = GameObject.Find("AudioManager");
		audioSource = audioSourceObject.GetComponent<AudioSource>();
		am = audioSourceObject.GetComponent<AudioManager>();

		if (!audioSource.isPlaying)
		{
			int rndIndex = Random.Range(0, am.sounds.Length - 1);
			audioSource.clip = am.sounds[rndIndex];
			audioSource.Play();
		}

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
