using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityScript.Lang;

public class TargetController : MonoBehaviour {

	[SerializeField] private GameObject targetPrefab;
	[SerializeField] public GameObject currentTarget;
	[SerializeField] private GameObject targetFrame;

	private RaycastHit2D[] hits;

	private void Awake()
	{
		DrawTargetUI();
	}

	void Update()
	{
		DetectAndSetTargetOnClick();
	}

	private void DetectAndSetTargetOnClick()
	{
		if (!Input.GetMouseButtonDown(0)) return;
		
		if (IsTargetableUnderMouse())
		{
			InstantiateTarget();
		}
		else
		{
			RemoveTarget();
		}
	}

	public void RemoveTarget()
	{
		if (!GameObject.FindWithTag(Tags.Target)) return;
		
		Destroy(GameObject.FindWithTag(Tags.Target).gameObject);
		currentTarget = null;
		DrawTargetUI();
	}

	private bool IsTargetableUnderMouse()
	{
		Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

		hits = Physics2D.RaycastAll(mousePos2D, Vector2.zero);

		return hits.Length > 0;
	}

	private void InstantiateTarget()
	{
		foreach (var hit2D in hits)
		{
			if (hit2D.collider.CompareTag(Tags.Targetable))
			{
				var other = hit2D.collider.transform.root;

				RemoveTarget();
				
				Instantiate(targetPrefab, other.position, Quaternion.identity, other);
				currentTarget = other.transform.root.gameObject;
				DrawTargetUI();
			}
			else
			{
				RemoveTarget();
			}
		}
	}

	public bool IsTargetAttackable()
	{
		return IsSomethingTargeted() && currentTarget.CompareTag(Tags.Enemy);
	}

	public void HitAttackableTarget(int hit)
	{
		if (IsTargetAttackable())
		{
			currentTarget.GetComponentInChildren<HealthController>().Hit(hit);
		}
		else
		{
			Debug.Log("No target or target is friendly!");
		}
	}

	public bool IsSomethingTargeted()
	{
		return currentTarget != null;
	}

	private void DrawTargetUI()
	{
		targetFrame.SetActive(IsSomethingTargeted());
	}
}
