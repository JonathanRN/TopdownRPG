using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
	[SerializeField] private float minimumAttackRange = 1f;
	[SerializeField] private int autoAttackDamage = 3;
	
	private Target target;
	private StatsController statsController;
	private ErrorMessage errorMessage;
	private GameObject player;
	
	public float TimeBeforeMeleeLeft { get; set; }
	public bool IsAttacking { get; set; }

	private float attackTimeAfterHaste;

	private void Awake()
	{
		InitializeComponents();
	}

	private void InitializeComponents()
	{
		target = GameObject.FindWithTag(Tags.GameController).GetComponent<Target>();
		statsController = GetComponent<StatsController>();
		errorMessage = GameObject.FindWithTag(Tags.ErrorMessage).GetComponent<ErrorMessage>();
		player = GameObject.FindWithTag(Tags.Player).gameObject;
	}

	private void CalculateHaste()
	{
		var haste = (statsController.Haste * 1.5f) / 100f; //TODO change Autoattack time
		attackTimeAfterHaste = 1.5f - haste;
	}

	public void AutoAttackAtTarget()
	{
		if (target.IsTargetAttackable() && IsInRangeWith(target.currentTarget))
		{
			StartAutoAttacking();
		}
		else
		{
			errorMessage.Show("Invalid target or too far away!");
		}
	}
	
	public void AutoAttackAtPlayer()
	{
		if (IsInRangeWith(player))
		{
			StartAutoAttacking();
		}
	}

	private void StartAutoAttacking()
	{
		CalculateHaste();

		IsAttacking = true;
		TimeBeforeMeleeLeft = attackTimeAfterHaste;
	}

	private void Update()
	{
		if (!IsAttacking) return;
		if (TimeBeforeMeleeLeft > 0)
		{
			TimeBeforeMeleeLeft -= Time.deltaTime;
		}
		else
		{
			StopAutoAttacking();
			//TODO change attacking
			player.GetComponentInChildren<Health>().Hit(autoAttackDamage);
		}
	}

	private void StopAutoAttacking()
	{
		IsAttacking = false;
		TimeBeforeMeleeLeft = 0;
	}

	public void InterruptAutoAttacking()
	{
		if (!IsAttacking) return;
		StopAutoAttacking();
		errorMessage.Show("Auto-attacking interrupted!");
	}

	public bool IsInRangeWith(GameObject other)
	{
		return Vector3.Distance(other.transform.position, gameObject.transform.position) < minimumAttackRange;
	}
}
