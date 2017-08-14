using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInfo : MonoBehaviour 
{
	[SerializeField] float zoomFactor;
	[SerializeField] Transform cameraNode;
	private Camera2D camera;

	void Awake()
	{
		camera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera2D>();
	}

	void OnEnable()
	{
		camera.SetTrackingTarget(cameraNode);
		camera.SetZoom (zoomFactor);
	}
}
