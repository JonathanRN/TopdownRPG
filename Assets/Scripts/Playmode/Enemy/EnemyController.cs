using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	[SerializeField] private float moveSpeed;

	private GameObject player;
	public bool IsPlayerSeen { get; set; }

	private void Awake()
	{
		IsPlayerSeen = false;
		player = GameObject.FindWithTag(Tags.Player).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		MoveTowardPlayer();
	}

	private void MoveTowardPlayer()
	{
		if (IsPlayerSeen)
		{
			transform.root.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), player.transform.position, moveSpeed * Time.deltaTime);
		}
	}
}
