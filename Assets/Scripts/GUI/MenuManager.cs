using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour 
{
	[SerializeField] GameObject blinkingText;
	[SerializeField] float blinkingRate;

	void Start()
	{
		StartCoroutine (BlinkRoutine ());
	}

	void Update () 
	{
		if (Input.GetButtonDown("Escape"))
			#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
			#else
			Application.Quit ();
			#endif
		
		if (Input.GetButtonDown("Space"))
			SceneManager.LoadScene (1);
	}

	IEnumerator BlinkRoutine()
	{
		while(true)
		{
			BlinkText ();
			yield return new WaitForSeconds (blinkingRate); 
		}
	}

	private void BlinkText()
	{
		blinkingText.SetActive (!blinkingText.activeSelf);
	}
}
