using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;

public class Health : MonoBehaviour {
	
	[Header("Variables")] 
	[SerializeField] private int maxHealth;
	private int healthPoints;

	private Target target;
	private HitSensor hitSensor;
	private FloatingDamage floatingDamage;

	public bool IsDead { get; private set; }
	
	public int HealthPoints 
	{
		get { return healthPoints; }
		private set { healthPoints = value <= 0 ? 0 : value; } 
	}

	private void Awake()
	{
		InitializeComponents();
	}

	private void InitializeComponents()
	{
		HealthPoints = maxHealth;
		target = GameObject.FindWithTag(Tags.GameController).GetComponent<Target>();
		hitSensor = transform.root.GetComponentInChildren<HitSensor>();
		floatingDamage = Resources.Load<FloatingDamage>("Prefabs/FloatingDamage");
	}

	private void Update()
	{
		IsDead = HealthPoints <= 0;
		DestroyObjectIfDead();
	}

	private void OnEnable()
	{
		hitSensor.OnHit += Hit;
	}

	private void OnDisable()
	{
		hitSensor.OnHit -= Hit;
	}

	public void Hit(int hit)
	{
		HealthPoints -= hit;

		InstantiateFloatDamage(hit);
	}

	private void InstantiateFloatDamage(int hit)
	{
		var damage = Instantiate(floatingDamage);
		Vector2 screenPosition = Camera.main.WorldToScreenPoint(
			new Vector2(transform.root.position.x + Random.Range(-0.5f, 0.5f), 
				transform.root.position.y + Random.Range(-0.5f, 0.5f))
		);
		
		damage.SetText(hit.ToString());
		damage.transform.position = screenPosition;
	}

	private void DestroyObjectIfDead()
	{
		if (IsDead)
		{
			target.RemoveTarget();
			Destroy(transform.root.gameObject);
		}
	}

	public int GetMaxHealth()
	{
		return maxHealth;
	}
}
