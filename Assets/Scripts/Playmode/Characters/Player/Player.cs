using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
	private Cast cast;
	private Spell _spell;
	private Mover mover;
	private Target target;
	private SpriteRenderer spriteRenderer;

	private void Awake()
	{
		InitializeComponents();
	}

	private void InitializeComponents()
	{
		cast = GetComponent<Cast>();
		mover = GetComponent<RootMover>();
		target = GameObject.FindWithTag(Tags.GameController).GetComponent<Target>();
		spriteRenderer = transform.root.GetComponentInChildren<SpriteRenderer>();
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
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			cast.CastSpellAtAttackableTarget(Spells.Frostbolt);
		}
	}

	private void ProcessPlayerMovement()
	{
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

	private void UpdatePlayerRotation()
	{
		if (target.IsSomethingTargeted())
		{
			mover.RotateSpriteTowardsTarget(target.currentTarget.transform.root, spriteRenderer.gameObject.transform);
		}
		else
		{
			mover.RotateSpriteTowardsMouse(spriteRenderer.gameObject.transform);
		}
	}
}
