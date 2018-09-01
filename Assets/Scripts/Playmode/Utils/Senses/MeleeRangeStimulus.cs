using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void MeleeRangeEventHandler();

public class MeleeRangeStimulus : MonoBehaviour
{
	public event MeleeRangeEventHandler OnMeleeRangeEnter;
	public event MeleeRangeEventHandler OnMeleeRangeExit;
	
	private void NotifyMeleeEnter()
	{
		if (OnMeleeRangeEnter != null) OnMeleeRangeEnter();
	}
	
	private void NotifyMeleeExit()
	{
		if (OnMeleeRangeExit != null) OnMeleeRangeExit();
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<HitSensor>() == null) return;
		
		NotifyMeleeEnter();
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.GetComponent<HitSensor>() == null) return;

		NotifyMeleeExit();
	}
}
