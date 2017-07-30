using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour 
{
	public GameObject blinkingText;
	public float blinkingRate;

	private float blinkingTimer;

	void Update () 
	{
		blinkingTimer += Time.deltaTime;

		if (blinkingTimer > blinkingRate) 
		{
			blinkingTimer = 0f;
			blinkingText.SetActive (!blinkingText.activeSelf);
		}

		if (Input.anyKeyDown)
			SceneManager.LoadScene (1);
	}
}
