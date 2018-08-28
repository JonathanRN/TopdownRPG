using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarTargetController : MonoBehaviour
{
    private TargetController targetController;
	
    private Text healthText;
    private Slider healthBar;

    private HealthController healthController;

    private void Awake()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        targetController = GameObject.FindWithTag(Tags.GameController).GetComponent<TargetController>();
        
        healthBar = GetComponentInChildren<Slider>();
        healthText = GetComponentInChildren<Text>();

        healthController = targetController.currentTarget.GetComponentInChildren<HealthController>();
    }

    private void Update()
    {
        //TODO: Notify when Ui needs to be updated (see tp1)
        healthText.text = "HP: " + healthController.Health;
        healthBar.value = (healthController.Health * healthController.GetMaxHealth()) / 100f;
    }
}
