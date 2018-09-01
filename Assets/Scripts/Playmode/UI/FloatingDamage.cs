using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// https://www.youtube.com/watch?v=fbUOG7f3jq8
/// </summary>
public class FloatingDamage : MonoBehaviour
{
	[SerializeField] private Animator animator;
	private GameObject canvas;

	private Text damageText;

	private void Awake()
	{
		InitializeComponents();
		
		var clipInfo = animator.GetCurrentAnimatorClipInfo(0);
		Destroy(gameObject, clipInfo[0].clip.length);
	}

	private void InitializeComponents()
	{
		damageText = animator.GetComponent<Text>();
		canvas = GameObject.FindWithTag(Tags.Canvas);
	}

	public void SetText(string text)
	{
		damageText.text = text;
		
		transform.SetParent(canvas.transform, false);
	}
}
