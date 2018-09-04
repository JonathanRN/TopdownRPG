using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetCastBar : MonoBehaviour
{
	private Text castTimeLeft;
	private Text castableName;
	private Text[] texts;
	
	private Slider castBar;
	private Cast cast;
	private Target target;
	
	private void Awake()
	{
		InitializeComponents();
	}
	
	private void InitializeComponents()
	{
		texts = GetComponentsInChildren<Text>();
		castBar = GetComponentInChildren<Slider>();
		target = GameObject.FindWithTag(Tags.GameController).GetComponent<Target>();
		InitializeTextComponents();
		
		HideTargetCastBar();
	}

	private void InitializeTextComponents()
	{
		foreach (var text in texts)
		{
			if (text.name == "Time")
			{
				castTimeLeft = text.gameObject.GetComponent<Text>();
			}
			else
			{
				castableName = text.gameObject.GetComponent<Text>();
			}
		}
	}

	private void Update()
	{
		UpdateValues();
	}

	private void UpdateValues()
	{
		if (!target.IsSomethingTargeted()) return;
		
		//TODO this is bad, find a solution
		cast = target.currentTarget.GetComponentInChildren<Cast>();
		
		castTimeLeft.gameObject.SetActive(cast.IsCasting);
		castBar.gameObject.SetActive(cast.IsCasting);
		castableName.gameObject.SetActive(cast.IsCasting);

		if (!cast.IsCasting) return;
		castBar.value = (cast.CastTimeLeft * 100f) / cast.GetCastableCastTime();
		castableName.text = "Casting: " + cast.GetCastableName();
		castTimeLeft.text = cast.CastTimeLeft.ToString("0.##");
	}

	private void HideTargetCastBar()
	{
		castTimeLeft.gameObject.SetActive(false);
		castBar.gameObject.SetActive(false);
		castableName.gameObject.SetActive(false);
	}
}
