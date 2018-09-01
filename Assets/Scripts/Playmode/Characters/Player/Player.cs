using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
	private Cast cast;
	private Castable castable;
	private Mover mover;
	private Target target;

	private void Awake()
	{
		InitializeComponents();
	}

	private void InitializeComponents()
	{
		cast = GetComponent<Cast>();
		mover = GetComponent<RootMover>();
		target = GameObject.FindWithTag(Tags.GameController).GetComponent<Target>();
	}

	private void Update()
	{
		ProcessPlayerMovement();
		RotateTowardsTarget();

		if (cast.IsCasting) return;
		
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			cast.CastSpellAtAttackableTarget(Castables.Fireball);
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			cast.CastSpellAtAttackableTarget(Castables.Frostbolt);
		}
	}

	private void ProcessPlayerMovement()
	{
		//TODO rotation without affecting movement
		if (Input.GetKey(KeyCode.A))
		{
			mover.Move(Vector3.left);
			cast.InterruptCasting();
		}
		if (Input.GetKey(KeyCode.D))
		{
			mover.Move(Vector3.right);
			cast.InterruptCasting();
		}
		if (Input.GetKey(KeyCode.W))
		{
			mover.Move(Vector3.up);
			cast.InterruptCasting();
		}
		if (Input.GetKey(KeyCode.S))
		{
			mover.Move(Vector3.down);
			cast.InterruptCasting();
		}
	}

	private void RotateTowardsTarget()
	{
		if (target.IsSomethingTargeted())
		{
			mover.RotateTowardsTarget(target.currentTarget.transform.root);
		}
	}
}
