using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarPlayer : MonoBehaviour
{
	private Text healthText;
	private Slider healthBar;

	private Health _health;
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
		
		_health = GameObject.FindWithTag(Tags.Player).GetComponentInChildren<Health>();
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
		if (_health == null) return;
		healthText.text = "HP: " + _health.HealthPoints;
		healthBar.value = (_health.HealthPoints * 100f) / _health.GetMaxHealth();
	}
}
