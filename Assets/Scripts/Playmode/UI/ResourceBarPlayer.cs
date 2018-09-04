using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBarPlayer : MonoBehaviour
{
	[SerializeField] private Image fill;
	
	private Text resourceText;
	private Slider resourceBar;

	private Resource resource;

	private void Awake()
	{
		InitializeComponents();
		RefreshValues();
		SetResourceColor();
	}
	
	private void InitializeComponents()
	{
		resourceBar = GetComponentInChildren<Slider>();
		resourceText = GetComponentInChildren<Text>();

		resource = GameObject.FindWithTag(Tags.Player).GetComponentInChildren<Resource>();
	}

	private void Update()
	{
		RefreshValues();
	}

	private void RefreshValues()
	{
		if (resource == null) return;
		resourceText.text = resource.ResourceAmount.ToString();
		resourceBar.value = (resource.ResourceAmount * 100f) / resource.GetMaxResource();
	}

	private void SetResourceColor()
	{
		if (resource.IsResourceTypeOf(ResourceType.Mana))
		{
			fill.color = ResourceColor.Mana;
		}
		else if (resource.IsResourceTypeOf(ResourceType.Energy))
		{
			fill.color = ResourceColor.Energy;
		}
		else if (resource.IsResourceTypeOf(ResourceType.Rage))
		{
			fill.color = ResourceColor.Rage;
		}
	}
}
