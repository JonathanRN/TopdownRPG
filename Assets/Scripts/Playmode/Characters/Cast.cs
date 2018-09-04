using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CastEventHandler(int cost);

public class Cast : MonoBehaviour
{
	[SerializeField] private GameObject[] castablePrefabs;

	public event CastEventHandler OnCast;

	private Target target;
	private Spell spell;
	private Resource resource;
	private StatsController statsController;
	private ErrorMessage errorMessage;
	
	public float CastTimeLeft { get; set; }
	public bool IsCasting { get; set; }

	private float castTimeAfterHaste;

	private void Awake()
	{
		InitializeComponents();
	}

	private void InitializeComponents()
	{
		target = GameObject.FindWithTag(Tags.GameController).GetComponent<Target>();
		statsController = GetComponent<StatsController>();
		errorMessage = GameObject.FindWithTag(Tags.ErrorMessage).GetComponent<ErrorMessage>();
		resource = GetComponent<Resource>();
	}

	public void CastSpellAtAttackableTarget(Spells name)
	{
		if (target.IsTargetAttackable())
		{
			spell = GetCastableWithName(name);
			if (!resource.CanCastSpell(spell))
			{
				errorMessage.Show("Not enough " + resource.GetResourceType() + "!");
			}
			StartCasting();
		}
		else
		{
			errorMessage.Show("No target or target is friendly.");
		}
	}

	public void CastSpellAtPlayer(Spells name)
	{
		spell = GetCastableWithName(name);
		StartCasting();
	}

	private void StartCasting()
	{
		if (!resource.CanCastSpell(spell)) return;
		
		castTimeAfterHaste = statsController.GetCalculatedHaste(spell.CastTime);

		IsCasting = true;
		CastTimeLeft = castTimeAfterHaste;
	}

	private void Update()
	{
		if (!IsCasting) return;
		if (CastTimeLeft > 0)
		{
			CastTimeLeft -= Time.deltaTime;
		}
		else
		{
			StopCasting();
			InstantiateProjectile();
		}
	}

	private void StopCasting()
	{
		IsCasting = false;
		CastTimeLeft = 0;
	}

	private void InstantiateProjectile()
	{
		var instance = Instantiate(spell.transform.parent.gameObject, transform.position, transform.rotation);
		
		if (transform.root.gameObject.CompareTag(Tags.Enemy))
		{
			instance.GetComponentInChildren<Spell>().targetToCastTo = Targets.Player;
		}
		else if (transform.root.gameObject.CompareTag(Tags.Player))
		{
			instance.GetComponentInChildren<Spell>().targetToCastTo = Targets.Enemy;
		}
		NotifyCast();
	}

	public Spell GetCastableWithName(Spells name)
	{
		//TODO Ugly as fuck, but it works for now
		foreach (var castablePrefab in castablePrefabs)
		{
			var castable = castablePrefab.GetComponentInChildren<Spell>();
			if (castable.Name == name)
			{
				return castable;
			}
		}

		return null;
	}

	public float GetCastableCastTime()
	{
		return spell != null ? castTimeAfterHaste : -1f;
	}

	public Spells GetCastableName()
	{
		return spell != null ? spell.Name : Spells.None;
	}

	public void InterruptCasting()
	{
		if (!IsCasting) return;
		StopCasting();
		errorMessage.Show("Casting interrupted!");
	}

	public Spells ChooseRandomCastable()
	{
		int random = Random.Range(0, castablePrefabs.Length);

		return castablePrefabs[random].GetComponentInChildren<Spell>().Name;
	}
	
	private void NotifyCast()
	{
		if (OnCast != null) OnCast(spell.Cost);
	}
}