﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSounds : MonoBehaviour {
	public AudioClip[] shotSounds;

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
