using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cast : MonoBehaviour
{
	[SerializeField] private GameObject[] castablePrefabs;

	private Target target;
	private Castable castable;
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
	}

	private void CalculateHaste()
	{
		var haste = (statsController.Haste * castable.CastTime) / 100f;
		castTimeAfterHaste = castable.CastTime - haste;
	}

	public void CastSpellAtAttackableTarget(Castables name)
	{
		if (target.IsTargetAttackable())
		{
			castable = GetCastableWithName(name);
			StartCasting();
		}
		else
		{
			errorMessage.Show("No target or target is friendly.");
		}
	}

	public void CastSpellAtPlayer(Castables name)
	{
		castable = GetCastableWithName(name);
		StartCasting();
	}

	private void StartCasting()
	{
		CalculateHaste();

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
		var instance = Instantiate(castable.transform.parent.gameObject, transform.position, transform.rotation);
		
		if (transform.root.gameObject.CompareTag(Tags.Enemy))
		{
			instance.GetComponentInChildren<Castable>().targetToCastTo = Targets.Player;
		}
		else if (transform.root.gameObject.CompareTag(Tags.Player))
		{
			instance.GetComponentInChildren<Castable>().targetToCastTo = Targets.Enemy;
		}
		
	}

	public Castable GetCastableWithName(Castables name)
	{
		//Ugly as fuck, but it works for now
		foreach (var castablePrefab in castablePrefabs)
		{
			var castable = castablePrefab.GetComponentInChildren<Castable>();
			if (castable.Name == name)
			{
				return castable;
			}
		}

		return null;
	}

	public float GetCastableCastTime()
	{
		return castable != null ? castTimeAfterHaste : -1f;
	}

	public Castables GetCastableName()
	{
		return castable != null ? castable.Name : Castables.None;
	}

	public void InterruptCasting()
	{
		if (!IsCasting) return;
		StopCasting();
		errorMessage.Show("Casting interrupted!");
	}

	public Castables ChooseRandomCastable()
	{
		int random = Random.Range(0, castablePrefabs.Length);

		return castablePrefabs[random].GetComponentInChildren<Castable>().Name;
	}
}