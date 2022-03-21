using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;

	public float smoothSpeed = 0.125f;
	public Vector2 offset;

	public bool lookAt = false;

	void FixedUpdate()
	{
		Vector2 desiredPosition = (Vector2)target.position + offset;
		Vector2 smoothedPosition = Vector2.Lerp(transform.position, desiredPosition, smoothSpeed);
		transform.position = smoothedPosition;

		if (lookAt)
			transform.LookAt(target);
	}
}
