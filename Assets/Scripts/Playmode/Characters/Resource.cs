using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
	[Header("Variables")]
	[SerializeField] private ResourceType resourceType;
	[SerializeField] private int maxResource;
	[SerializeField] private int amountToRegenerate;
	[SerializeField] private int timeBetweenRegens;

	private Cast cast;
	private StatsController statsController;
	private Coroutine regenResourceRoutine;

	private int resourceAmount;
	private float timeAfterHaste;

	public int ResourceAmount
	{
		get { return resourceAmount; }
		private set
		{
			if (value <= 0)
			{
				resourceAmount = 0;
			}
			else if (value >= maxResource)
			{
				resourceAmount = maxResource;
			}
			else
			{
				resourceAmount = value;
			}
		} 
	}
	
	private void Awake()
	{
		InitializeComponents();
		regenResourceRoutine = StartCoroutine(StartRegen());
	}

	private void InitializeComponents()
	{
		ResourceAmount = maxResource;
		cast = GetComponent<Cast>();
		statsController = GetComponent<StatsController>();

		//Initialize only on start for now
		timeAfterHaste = statsController.GetCalculatedHaste(timeBetweenRegens);
	}

	private IEnumerator StartRegen()
	{
		while (true)
		{
			yield return new WaitForSeconds(timeAfterHaste);
			RegenerateResource();
		}
	}

	private void OnEnable()
	{
		cast.OnCast += OnCast;
	}

	private void OnDisable()
	{
		cast.OnCast -= OnCast;
	}

	private void OnCast(int cost)
	{
		ResourceAmount -= cost;
	}

	public int GetMaxResource()
	{
		return maxResource;
	}

	public bool IsResourceTypeOf(ResourceType type)
	{
		return type == resourceType;
	}

	public ResourceType GetResourceType()
	{
		return resourceType;
	}

	private void RegenerateResource()
	{
		ResourceAmount += amountToRegenerate;
	}

	public bool CanCastSpell(Spell spellToCast)
	{
		return spellToCast.Cost <= ResourceAmount;
	}
}
