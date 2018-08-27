using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] private float playerSpeed = 5f;

	private void Update()
	{
		ProcessPlayerMovement();
	}

	private void ProcessPlayerMovement()
	{
		var root = transform.root;
		if (Input.GetKey(KeyCode.A))
		{
			root.Translate(Vector3.left * playerSpeed * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.D))
		{
			root.Translate(Vector3.right * playerSpeed * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.W))
		{
			root.Translate(Vector3.up * playerSpeed * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.S))
		{
			root.Translate(Vector3.down * playerSpeed * Time.deltaTime);
		}
	}
}
