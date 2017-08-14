using UnityEngine;
using System.Collections;

public class LevelMusic : MonoBehaviour
{
	[SerializeField] AudioClip levelMusic;

	void Start ()
	{
		SoundManager.PlayMusic (levelMusic);
	}
}

