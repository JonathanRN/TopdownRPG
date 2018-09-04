using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTarget : MonoBehaviour
{

	[SerializeField] private GameObject mouseTargetPrefab;

	private GameObject mouseObject;
	private Vector3 mousePos;

	private void Update()
	{
		SetMouseTargetPosition();
	}

	private void SetMouseTargetPosition()
	{
		if (!Input.GetMouseButton(1)) return;
		
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
		InstantiateMouseTarget();
	}

	private void InstantiateMouseTarget()
	{
		RemoveMouseTarget();
		mouseObject = Instantiate(mouseTargetPrefab, mousePos, Quaternion.identity);
	}

	private void RemoveMouseTarget()
	{
		if (IsMouseTargetSet())
		{
			DestroyMouseTarget();
		}
	}

	public GameObject GetLastMouseTarget()
	{
		return IsMouseTargetSet() ? mouseObject : null;
	}

	public bool IsMouseTargetSet()
	{
		return mouseObject != null;
	}

	public void DestroyMouseTarget()
	{
		Destroy(mouseObject);
	}
}
