using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSystem : MonoBehaviour 
{
	public static Transform currentRoom;

	[SerializeField] GameObject exitRoom;
	[SerializeField] GameObject enterRoom;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			currentRoom = enterRoom.transform;
			enterRoom.SetActive (true);
			exitRoom.SetActive (false);
		}
	}
}
