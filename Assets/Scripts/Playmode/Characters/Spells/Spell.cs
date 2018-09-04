using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
	[Header("Variables")]
	[SerializeField] public Spells Name;
	[SerializeField] public float CastTime;
	[SerializeField] public int Damage;
	[SerializeField] public int Cost;
	
	public Targets targetToCastTo { get; set; }

	private Mover mover;
	private HitStimulus hitStimulus;
	private Target _target;
	private GameObject lastTarget;
	private GameObject player;
	
	private void Awake()
	{
		InitializeComponents();
	}

	private void InitializeComponents()
	{
		mover = GetComponent<RootMover>();
		hitStimulus = transform.parent.GetComponentInChildren<HitStimulus>();
		_target = GameObject.FindWithTag(Tags.GameController).GetComponent<Target>();
		player = GameObject.FindWithTag(Tags.Player).gameObject;
	}

	private void OnEnable()
	{
		hitStimulus.OnHit += OnHit;
	}

	private void OnDisable()
	{
		hitStimulus.OnHit -= OnHit;
	}

	private void OnHit()
	{
		Destroy(transform.root.gameObject);
	}

	private void Update()
	{
		Act();
	}

	private void Act()
	{
		if (targetToCastTo == Targets.Player)
		{
			MoveSpellTowardsPlayer();
		}
		else if (targetToCastTo == Targets.Enemy)
		{
			MoveSpellTowardsEnemy();
		}
	}

	private void MoveSpellTowardsPlayer()
	{
		mover.MoveToExactTarget(player.transform);
	}

	private void MoveSpellTowardsEnemy()
	{
		if (_target.IsSomethingTargeted())
		{
			lastTarget = _target.currentTarget;
		}

		if (lastTarget == null)
		{
			Destroy(transform.parent.gameObject);
			return;
		}
		mover.MoveToExactTarget(lastTarget.transform);
	}
}
