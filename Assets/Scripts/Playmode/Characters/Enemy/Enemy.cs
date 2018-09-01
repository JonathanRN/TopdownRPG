using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	private GameObject player;
	private Mover mover;
	private HitSensor hitSensor;
	private Cast cast;
	
	public bool IsPlayerSeen { get; set; }

	private void Awake()
	{
		InitializeComponents();
	}

	private void InitializeComponents()
	{
		IsPlayerSeen = false;
		player = GameObject.FindWithTag(Tags.Player).gameObject;
		mover = GetComponent<RootMover>();
		hitSensor = transform.parent.GetComponentInChildren<HitSensor>();
		cast = GetComponent<Cast>();
	}

	private void OnEnable()
	{
		hitSensor.OnHit += OnHit;
	}

	private void OnDisable()
	{
		hitSensor.OnHit -= OnHit;
	}

	private void OnHit(int hit)
	{
		IsPlayerSeen = true;
		Debug.Log("Ow! The player hit me!");
	}

	private void Update () {
		if (IsPlayerSeen)
		{
			//mover.MoveToExactTarget(player.transform);
			if (cast.IsCasting) return;
			cast.CastSpellAtPlayer(cast.ChooseRandomCastable());
		}
	}
}
