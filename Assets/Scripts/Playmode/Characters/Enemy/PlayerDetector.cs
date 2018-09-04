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
		if (other.transform.root.CompareTag(Tags.Enemy)) return;
		
		enemy.IsPlayerSeen = true;
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.GetComponent<HitSensor>() == null) return;
		if (other.transform.root.CompareTag(Tags.Enemy)) return;
		
		enemy.IsPlayerSeen = false;
	}

}
