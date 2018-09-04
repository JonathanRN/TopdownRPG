using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsController : MonoBehaviour
{
	[Header("Primary Stats")] 
	[SerializeField] public float Strength = 1;
	[SerializeField] public float Intelligence = 1;
	[SerializeField] public float Agility = 1;
	
	[Header("Secondary Stats (Percentage)")] 
	[SerializeField] public float Haste = 1;
	[SerializeField] public float CriticalStrike = 1;
	[SerializeField] public float Stamina = 1;
	
	public float GetCalculatedHaste(float time)
	{
		var haste = (Haste * time) / 100f;
		return (time - haste);
	}
}
