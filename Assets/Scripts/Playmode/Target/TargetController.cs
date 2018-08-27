using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour {

	[SerializeField] private GameObject targetPrefab;
	[SerializeField] public GameObject currentTarget;

	void Update()
	{
		DetectAndSetTargetOnClick();
	}

	private void DetectAndSetTargetOnClick()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

			RaycastHit2D[] hit = Physics2D.RaycastAll(mousePos2D, Vector2.zero);

			if (hit.Length <= 0)
			{
				Debug.Log("You hit nothing!");
				RemoveTarget();
			}
			else
			{
				for (int i = 0; i < hit.Length; i++)
				{
					if (hit[i].collider.tag == Tags.Targetable)
					{
						var other = hit[i].collider.transform.root;
						Debug.Log(hit[i].collider.gameObject.name);

						RemoveTarget();
						Instantiate(targetPrefab, other.position, Quaternion.identity, other);
						currentTarget = other.gameObject;
					}
					else
					{
						RemoveTarget();
					}
				}
			}
		}
	}

	private void RemoveTarget()
	{
		if (GameObject.FindWithTag(Tags.Target))
		{
			Destroy(GameObject.FindWithTag(Tags.Target).gameObject);
			currentTarget = null;
		}
	}
}
