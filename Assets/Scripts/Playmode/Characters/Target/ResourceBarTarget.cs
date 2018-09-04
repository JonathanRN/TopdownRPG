using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceBarTarget : MonoBehaviour 
{
	[SerializeField] private Image fill;
	
	private Text resourceText;
	private Slider resourceBar;
	private Target target;
	private Resource resource;

	private void Awake()
	{
		InitializeComponents();
		RefreshValues();
	}
	
	private void InitializeComponents()
	{
		resourceBar = GetComponentInChildren<Slider>();
		resourceText = GetComponentInChildren<Text>();
		target = GameObject.FindWithTag(Tags.GameController).GetComponent<Target>();
	}

	private void Update()
	{
		//TODO getcomponent in update
		RefreshValues();
	}

	private void RefreshValues()
	{
		if (!target.IsSomethingTargeted()) return;
		
		resource = target.currentTarget.GetComponentInChildren<Resource>();
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
