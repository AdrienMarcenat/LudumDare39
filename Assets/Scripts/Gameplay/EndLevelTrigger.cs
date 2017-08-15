using UnityEngine;
using System.Collections;

public class EndLevelTrigger : MonoBehaviour
{
	public delegate void SimpleEvent();
	public static event SimpleEvent EndLevel;

	private void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			if(EndLevel != null)
				EndLevel ();
		}
	}
}

