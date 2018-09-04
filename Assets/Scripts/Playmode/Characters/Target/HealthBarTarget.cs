using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarTarget : MonoBehaviour
{
	private Target target;

	private Text healthText;
	private Slider healthBar;

	private Health _health;
	private HitSensor hitSensor;

	private void Awake()
	{
		InitializeComponents();
	}

	private void InitializeComponents()
	{
		target = GameObject.FindWithTag(Tags.GameController).GetComponent<Target>();

		healthBar = GetComponentInChildren<Slider>();
		healthText = GetComponentInChildren<Text>();

		hitSensor = target.currentTarget.GetComponentInChildren<HitSensor>();
	}

	private void OnEnable()
	{
		hitSensor.OnHit += OnHit;
		target.OnChangeUi += OnChangeUi;
	}

	private void OnDisable()
	{
		hitSensor.OnHit -= OnHit;
		target.OnChangeUi += OnChangeUi;
	}

	private void OnChangeUi()
	{
		RefreshValues();
	}

	private void OnHit(int hit)
	{
		RefreshValues();
	}

	private void RefreshValues()
	{
		if (target.IsSomethingTargeted())
		{
			_health = target.currentTarget.GetComponentInChildren<Health>();
		}

		if (_health == null) return;
		healthText.text = "HP: " + _health.HealthPoints;
		healthBar.value = (_health.HealthPoints * 100f) / _health.GetMaxHealth();
	}
}