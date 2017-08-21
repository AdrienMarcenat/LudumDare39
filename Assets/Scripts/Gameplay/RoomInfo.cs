using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInfo : MonoBehaviour 
{
	[SerializeField] float zoomFactor;
	[SerializeField] Transform cameraNode;
	private Camera2D mainCamera;

	void Awake()
	{
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera2D>();
	}

	void OnEnable()
	{
		mainCamera.SetTrackingTarget(cameraNode);
		mainCamera.SetZoom (zoomFactor);
	}
}
