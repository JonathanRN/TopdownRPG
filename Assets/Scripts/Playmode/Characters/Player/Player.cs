using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
	private Cast cast;
	private Spell _spell;
	private Mover mover;
	private Target target;
	private Animator animator;

	private MouseTarget mouseTarget;
	
	private void Awake()
	{
		InitializeComponents();
	}

	private void InitializeComponents()
	{
		cast = GetComponent<Cast>();
		mover = GetComponent<RootMover>();
		target = GameObject.FindWithTag(Tags.GameController).GetComponent<Target>();
		mouseTarget = GameObject.FindWithTag(Tags.GameController).GetComponent<MouseTarget>();
		animator = transform.root.GetComponentInChildren<Animator>();
	}

	private void Update()
	{
		ProcessPlayerMovement();
		UpdatePlayerRotation();

		ProcessPlayerAbilities();
	}

	private void ProcessPlayerAbilities()
	{
		if (cast.IsCasting) return;

		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			cast.CastSpellAtAttackableTarget(Spells.Fireball);
			//InterruptMoving();
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			cast.CastSpellAtAttackableTarget(Spells.Frostbolt);
			//InterruptMoving();
		}
	}

	private void ProcessPlayerMovement()
	{
		if (mouseTarget.IsMouseTargetSet())
		{
			mover.MoveToExactTarget(mouseTarget.GetLastMouseTarget().transform.position);
			cast.InterruptCasting();
			animator.SetBool("IsRunning", true);
		}
	}

	private void UpdatePlayerRotation()
	{
		if (cast.IsCasting)
		{
			mover.RotateSpriteTowardsTarget(target.currentTarget.transform.root, transform.root);
		}
		else
		{
			if (!mouseTarget.IsMouseTargetSet()) return;
			mover.RotateTowardsTarget(mouseTarget.GetLastMouseTarget().transform.position);
		}
	}
}
