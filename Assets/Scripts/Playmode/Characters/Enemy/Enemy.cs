using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

	[SerializeField] private EnemyType enemyType;
	
	private HitSensor hitSensor;
	private Cast cast;
	private Player player;
	private Mover mover;
	private AutoAttack autoAttack;
	
	public bool IsPlayerSeen { get; set; }

	private void Awake()
	{
		InitializeComponents();
	}

	private void InitializeComponents()
	{
		hitSensor = transform.parent.GetComponentInChildren<HitSensor>();
		cast = GetComponent<Cast>();
		autoAttack = GetComponent<AutoAttack>();
		player = GameObject.FindWithTag(Tags.Player).GetComponentInChildren<Player>();
		mover = GetComponent<RootMover>();
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
			if (IsEnemyType(EnemyType.Melee))
			{
				mover.MoveToExactTarget(player.transform.position);
				
				if (autoAttack.IsAttacking) return;
				autoAttack.AutoAttackAtPlayer();
			}
			else
			{
				if (cast.IsCasting) return;
				
				//TODO move towards player if out of mana
				var spell = cast.ChooseRandomCastable();
				cast.CastSpellAtPlayer(spell);
			}
		}
	}

	public bool IsEnemyType(EnemyType enemyType)
	{
		return this.enemyType == enemyType;
	}
}
