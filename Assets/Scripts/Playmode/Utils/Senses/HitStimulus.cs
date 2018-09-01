using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HitStimulusEventHandler();

public class HitStimulus : MonoBehaviour {

	public event HitStimulusEventHandler OnHit;

	private Castable castable;

	private void Awake()
	{
		castable = transform.root.GetComponentInChildren<Castable>();
	}

	private void NotifyHit()
	{
		if (OnHit != null) OnHit();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<HitSensor>() == null) return;

		if (castable.targetToCastTo == Targets.Enemy)
		{
			if (!other.transform.root.CompareTag(Tags.Enemy)) return;
		}
		else if (castable.targetToCastTo == Targets.Player)
		{
			if (!other.transform.root.CompareTag(Tags.Player)) return;
		}

		var hit = castable.Damage;

		other.GetComponent<HitSensor>()?.Hit(hit);
		NotifyHit();
	}
}
