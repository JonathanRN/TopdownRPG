using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCastBar : MonoBehaviour
{
	private Text castTimeLeft;
	private Text castableName;
	private Text[] texts;
	
	private Slider castBar;
	private Cast cast;
	
	private void Awake()
	{
		InitializeComponents();
	}
	
	private void InitializeComponents()
	{
		texts = GetComponentsInChildren<Text>();
		castBar = GetComponentInChildren<Slider>();
		cast = GameObject.FindWithTag(Tags.Player).GetComponentInChildren<Cast>();
		InitializeTextComponents();
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
		castTimeLeft.gameObject.SetActive(cast.IsCasting);
		castBar.gameObject.SetActive(cast.IsCasting);
		castableName.gameObject.SetActive(cast.IsCasting);

		if (!cast.IsCasting) return;
		castBar.value = (cast.CastTimeLeft * 100f) / cast.GetCastableCastTime();
		castableName.text = "Casting: " + cast.GetCastableName();
		castTimeLeft.text = cast.CastTimeLeft.ToString("0.##");
	}
}
