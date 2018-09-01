using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class ErrorMessage : MonoBehaviour
{
	[SerializeField] private float errorMessageTimer = 2f;
	
	private Coroutine errorTimerRoutine;
	private Text message;
	
	public bool IsCurrentlyShowing { get; set; }

	private void Awake()
	{
		message = GetComponentInChildren<Text>();		
	}

	private IEnumerator ErrorMessageTimer()
	{
		while (true)
		{
			yield return new WaitForSeconds(errorMessageTimer);
			Hide();
		}
	}

	private void Hide()
	{
		message.text = "";
		StopCoroutine(errorTimerRoutine);
		IsCurrentlyShowing = false;
	}

	public void Show(string message)
	{
		if (IsCurrentlyShowing) return;
		
		this.message.text = message;
		errorTimerRoutine = StartCoroutine(ErrorMessageTimer());
		IsCurrentlyShowing = true;
	}
}
