using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;

public class HealthController : MonoBehaviour {
	
	[Header("Variables")] 
	[SerializeField] private int maxHealth;
	[SerializeField] private int health;

	private TargetController targetController;

	public bool IsDead { get; private set; }
	public bool UiNeedsToUpdate { get; set; }
	
	public int Health 
	{
		get { return health; }
		private set { health = value <= 0 ? 0 : value; } 
	}

	private void Awake()
	{
		InitializeComponents();
	}
	
	
	private void InitializeComponents()
	{
		Health = maxHealth;
		targetController = GameObject.FindWithTag(Tags.GameController).GetComponent<TargetController>();
	}

	private void Update()
	{
		IsDead = Health <= 0;
		DestroyObjectIfDead();
	}

	public void Hit(int hit)
	{
		Health -= hit;
		UiNeedsToUpdate = true;
	}

	private void DestroyObjectIfDead()
	{
		if (IsDead)
		{
			targetController.RemoveTarget();
			Destroy(transform.root.gameObject);
		}
	}

	public int GetMaxHealth()
	{
		return maxHealth;
	}
}
