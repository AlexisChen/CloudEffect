﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {



	public Transform explosionPrefab;
	void OnCollisionEnter(Collision collision)
	{
		ContactPoint contact = collision.contacts[0];
		Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
		Vector3 pos = contact.point;
		Instantiate(explosionPrefab, pos, rot);
		Destroy(gameObject);
	}

	// Use this for initialization
	void Start()
	{
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
