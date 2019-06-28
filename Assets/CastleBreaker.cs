using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleBreaker : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

public void OnTriggerEnter(Collider other)
{
	Rigidbody rd = other.gameObject.GetComponent<Rigidbody>();
	rd.useGravity = true;
	rd.isKinematic = false;
rd.AddForce((transform.position - other.transform.position) * -1 *(Random.Range(1.0f, 3.0f)));
}
}
