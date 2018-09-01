using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour {

	private Enemy enemy;

	private void Awake()
	{
		enemy = transform.root.GetComponentInChildren<Enemy>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<HitSensor>() == null) return;
		
		Debug.Log("I've seen the player!");
		enemy.IsPlayerSeen = true;
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.GetComponent<HitSensor>() == null) return;
		
		Debug.Log("I've lost the player!");
		enemy.IsPlayerSeen = false;
	}

}
