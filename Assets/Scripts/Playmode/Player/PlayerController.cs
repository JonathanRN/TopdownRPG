using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] private float playerMoveSpeed = 5f;

	private TargetController targetController;

	private void Awake()
	{
		InitializeComponents();
	}

	private void InitializeComponents()
	{
		targetController = GameObject.FindWithTag(Tags.GameController).GetComponent<TargetController>();
	}

	private void Update()
	{
		ProcessPlayerMovement();

		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			targetController.HitAttackableTarget(2);
		}
	}

	private void ProcessPlayerMovement()
	{
		var root = transform.root;
		if (Input.GetKey(KeyCode.A))
		{
			root.Translate(Vector3.left * playerMoveSpeed * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.D))
		{
			root.Translate(Vector3.right * playerMoveSpeed * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.W))
		{
			root.Translate(Vector3.up * playerMoveSpeed * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.S))
		{
			root.Translate(Vector3.down * playerMoveSpeed * Time.deltaTime);
		}
	}
}
