using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSounds : MonoBehaviour {
	public AudioClip tankBoom;
	public AudioClip tankExplosion;

	private AudioSource audioSource;

	// Use this for initialization
	void Start()
	{
		audioSource = gameObject.GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{

	}
}
