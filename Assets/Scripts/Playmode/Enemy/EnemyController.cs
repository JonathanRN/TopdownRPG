using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	private GameObject target;

	private void Awake()
	{
		target = GameObject.FindWithTag(Tags.Target).gameObject;
	}

	private void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Instantiate(target, transform.position, Quaternion.identity);
		}
	}
}
