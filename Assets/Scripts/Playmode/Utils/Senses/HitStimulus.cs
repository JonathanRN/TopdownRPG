using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HitStimulusEventHandler();

public class HitStimulus : MonoBehaviour {

	public event HitStimulusEventHandler OnHit;

	private Spell _spell;

	private void Awake()
	{
		_spell = transform.root.GetComponentInChildren<Spell>();
	}

	private void NotifyHit()
	{
		if (OnHit != null) OnHit();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<HitSensor>() == null) return;

		if (_spell.targetToCastTo == Targets.Enemy)
		{
			if (!other.transform.root.CompareTag(Tags.Enemy)) return;
		}
		else if (_spell.targetToCastTo == Targets.Player)
		{
			if (!other.transform.root.CompareTag(Tags.Player)) return;
		}

		var hit = _spell.Damage;

		other.GetComponent<HitSensor>()?.Hit(hit);
		NotifyHit();
	}
}
