using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeRangeController : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform.root.gameObject == transform.root.gameObject) return;
		if (!other.gameObject.CompareTag(Tags.Targetable)) return;
		
		Debug.Log("Enemy entered melee range!");
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.transform.root.gameObject == transform.root.gameObject) return;
		if (!other.gameObject.CompareTag(Tags.Targetable)) return;

		Debug.Log("Enemy exited melee range!");
	}
}