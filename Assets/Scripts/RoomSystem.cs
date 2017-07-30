using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSystem : MonoBehaviour 
{
	public GameObject room;
	public float zoomFactor;
	public Camera2D camera;
	public Transform cameraNode;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player") 
		{
			room.SetActive (true);
			camera.trackingTarget = cameraNode;
			camera.SetZoom (zoomFactor);
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			room.SetActive (false);
			camera.trackingTarget = null;
		}
	}
}
