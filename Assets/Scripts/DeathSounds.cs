﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSounds : MonoBehaviour {
	public AudioClip[] deathSounds;

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
