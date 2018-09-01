using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityScript.Lang;

public delegate void TargetUiChangeEventHandler();

public class Target : MonoBehaviour {

	[SerializeField] private GameObject targetPrefab;
	[SerializeField] public GameObject currentTarget;
	[SerializeField] private GameObject targetFrame;

	private RaycastHit2D[] hits;
	
	public event TargetUiChangeEventHandler OnChangeUi;

	private void NotifyUiChange()
	{
		if (OnChangeUi != null) OnChangeUi();
	}

	private void Awake()
	{
		UpdateTargetUi();
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
			UpdateTargetUi();
		}
		else
		{
			RemoveTarget();
		}
	}

	public void RemoveTarget()
	{
		if (IsSomethingTargeted())
		{
			Destroy(GameObject.FindWithTag(Tags.Target).gameObject);
		}
		currentTarget = null;
		UpdateTargetUi();
	}

	private bool IsTargetableUnderMouse()
	{
		hits = null;
		
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
				currentTarget = other.gameObject;
				return;
			}
			RemoveTarget();
		}
	}

	public bool IsTargetAttackable()
	{
		return IsSomethingTargeted() && currentTarget.CompareTag(Tags.Enemy);
	}

	public bool IsSomethingTargeted()
	{
		return currentTarget != null;
	}

	private void UpdateTargetUi()
	{
		targetFrame.SetActive(IsSomethingTargeted());
		NotifyUiChange();
	}
}
