using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
	[SerializeField]
	private GameObject objectPrefab;
	
	private Text healthText;
	private Slider healthBar;

	private HealthController healthController;

	private void Awake()
	{
		InitializeComponents();
	}
	
	private void InitializeComponents()
	{
		healthBar = GetComponentInChildren<Slider>();
		healthText = GetComponentInChildren<Text>();

		healthController = objectPrefab.GetComponentInChildren<HealthController>();
	}

	private void Update()
	{
		healthText.text = "HP: " + healthController.Health;
		healthBar.value = (healthController.Health * healthController.GetMaxHealth()) / 100f;
	}
}
