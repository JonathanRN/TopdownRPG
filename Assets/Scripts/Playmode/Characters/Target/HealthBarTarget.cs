using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarTarget : MonoBehaviour
{
	private Target _target;

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
		_target = GameObject.FindWithTag(Tags.GameController).GetComponent<Target>();

		healthBar = GetComponentInChildren<Slider>();
		healthText = GetComponentInChildren<Text>();

		hitSensor = _target.currentTarget.GetComponentInChildren<HitSensor>();
	}

	private void OnEnable()
	{
		hitSensor.OnHit += OnHit;
		_target.OnChangeUi += OnChangeUi;
	}

	private void OnDisable()
	{
		hitSensor.OnHit -= OnHit;
		_target.OnChangeUi += OnChangeUi;
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
		if (_target.IsSomethingTargeted())
		{
			_health = _target.currentTarget.GetComponentInChildren<Health>();
		}

		if (_health == null) return;
		healthText.text = "HP: " + _health.HealthPoints;
		healthBar.value = (_health.HealthPoints * 100f) / _health.GetMaxHealth();
	}
}