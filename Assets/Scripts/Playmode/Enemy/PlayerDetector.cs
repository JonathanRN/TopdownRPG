using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour {

	private EnemyController enemyController;

	private void Awake()
	{
		enemyController = transform.root.GetComponentInChildren<EnemyController>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("I've seen the player!");
		enemyController.IsPlayerSeen = true;
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		Debug.Log("I've lost the player!");
		enemyController.IsPlayerSeen = false;
	}

}
