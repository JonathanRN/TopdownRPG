using UnityEngine;


public class RootMover : Mover
{
	private Transform rootTransform;

	private new void Awake()
	{
		base.Awake();

		InitializeComponent();
	}

	private void InitializeComponent()
	{
		rootTransform = transform.root;
	}

	public override void Move(Vector3 direction)
	{
		rootTransform.Translate(direction.normalized * moveSpeed * Time.deltaTime);
	}

	public override void Rotate(float direction)
	{
		rootTransform.Rotate(
			Vector3.forward,
			(direction < 0 ? rotateSpeed : -rotateSpeed) * Time.deltaTime
		);
	}

	public override void MoveTowardsTarget(Transform target)
	{
		Move(new Vector3(0, 1));
		RotateTowardsTarget(target);
	}

	public override void RotateTowardsTarget(Transform target)
	{
		var vectorBetweenEnemy = new Vector3(rootTransform.position.x - target.transform.position.x,
			rootTransform.position.y - target.transform.position.y);
		if (Vector3.Dot(vectorBetweenEnemy, rootTransform.right) < -0.5)
		{
			Rotate(1f * Time.deltaTime);
		}
		else if (Vector3.Dot(vectorBetweenEnemy, rootTransform.right) > 0.5)
		{
			Rotate(-1f * Time.deltaTime);
		}
	}

	public override void MoveToExactTarget(Transform target)
	{
		rootTransform.position = Vector2.MoveTowards(new Vector2(rootTransform.position.x, rootTransform.position.y),
			target.position, moveSpeed * Time.deltaTime);
		RotateTowardsTarget(target);
	}
}