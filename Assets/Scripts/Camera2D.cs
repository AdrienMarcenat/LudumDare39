using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2D : MonoBehaviour
{
	public float followSpeed;
	public Transform trackingTarget;
	public float zoomFactor = 1.0f;
	public float zoomSpeed = 5.0f;

	private Camera camera;

	void Start()
	{
		camera = GetComponent<Camera>();
	}

	void Update()
	{
		float xTarget = trackingTarget.position.x;
		float yTarget = trackingTarget.position.y;

		float xNew = Mathf.Lerp(transform.position.x, xTarget, Time.deltaTime * followSpeed);
		float yNew = Mathf.Lerp (transform.position.y, yTarget, Time.deltaTime * followSpeed);
	
		transform.position = new Vector3(xNew, yNew, transform.position.z);
	}
		
	public void SetZoom(float zoomFactor)
	{
		this.zoomFactor = zoomFactor;
		StartCoroutine (Zoom());
	}

	IEnumerator Zoom()
	{
		float targetSize = zoomFactor;
		if(targetSize < camera.orthographicSize)
			while (targetSize < camera.orthographicSize)
			{
				camera.orthographicSize -= Time.deltaTime * zoomSpeed;
				yield return null;
			}
		else
			while (targetSize > camera.orthographicSize)
			{
				camera.orthographicSize += Time.deltaTime * zoomSpeed;
				yield return null;
			}
	}
}
