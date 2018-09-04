using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarPlayer : MonoBehaviour
{
	private Text healthText;
	private Slider healthBar;

	private Health health;
	private HitSensor hitSensor;

	private void Awake()
	{
		InitializeComponents();
		RefreshValues();
	}
	
	private void InitializeComponents()
	{
		healthBar = GetComponentInChildren<Slider>();
		healthText = GetComponentInChildren<Text>();
		
		health = GameObject.FindWithTag(Tags.Player).GetComponentInChildren<Health>();
		hitSensor = GameObject.FindWithTag(Tags.Player).GetComponentInChildren<HitSensor>();
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
		RefreshValues();
	}
	
	private void RefreshValues()
	{
		if (health == null) return;
		healthText.text = "HP: " + health.HealthPoints;
		healthBar.value = (health.HealthPoints * 100f) / health.GetMaxHealth();
	}
}
