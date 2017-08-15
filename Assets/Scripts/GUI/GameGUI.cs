using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameGUI : MonoBehaviour
{
	private Image fadeInOutImage;
	[SerializeField] float fadeSpeed;

	void Awake()
	{
		fadeInOutImage = GetComponent<Image> ();
	}

	void OnEnable()
	{
		GameManager.ChangeScene  += ChangeScene;
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	void OnDisable()
	{
		GameManager.ChangeScene  -= ChangeScene;
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	IEnumerator FadeIn()
	{
		SetFadeInOutImageAlpha (1);
		yield return null;

		while (fadeInOutImage.color.a > 0)
		{
			AddToFadeInOutImageAlpha (-fadeSpeed);
			yield return null;
		}
	}

	IEnumerator FadeOut()
	{
		SetFadeInOutImageAlpha (0);
		yield return null;

		while (fadeInOutImage.color.a < 1)
		{
			AddToFadeInOutImageAlpha (fadeSpeed);
			yield return null;
		}
	}

	private void AddToFadeInOutImageAlpha(float a)
	{
		Color c = fadeInOutImage.color;
		c.a += a;
		fadeInOutImage.color = c;
	}

	private void SetFadeInOutImageAlpha(float a)
	{
		Color c = fadeInOutImage.color;
		c.a = a;
		fadeInOutImage.color = c;
	}

	private void ChangeScene()
	{
		StartCoroutine (FadeOut ());
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		StartCoroutine (FadeIn ());
	}
}

