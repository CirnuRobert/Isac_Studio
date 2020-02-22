using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShoot : MonoBehaviour
{
	public Transform debugHitPointTransform;
	public Camera camera;
	public CharacterController characterController;
	private State state;
	private Vector3 hookshotPosition;


	private enum State{
		Normal,
		HookshotFlyingPlayer
	}

    void Start()
    {
		camera = transform.Find("Camera").GetComponent<Camera>();
		characterController = GetComponent<CharacterController>();
		state = State.Normal;
    }

    private void Update()
    {
		switch (state)
		{
		default:
		case State.Normal:
		HandleHookshotStart();
		break;
	
		case State.HookshotFlyingPlayer:
		HandleHookshotMovement();
		break;
		}

    }

	private void HandleHookshotStart()
	{
		if(Input.GetMouseButtonDown(1))
		{
			if(Physics.Raycast(camera.transform.position, camera.transform.forward, out RaycastHit raycastHit))
			{
				debugHitPointTransform.position = raycastHit.point;
				hookshotPosition = raycastHit.point;
				state = State.HookshotFlyingPlayer;
			}
		}
	}

	private void HandleHookshotMovement()
	{
		Vector3 hookshotDir = (hookshotPosition - transform.position).normalized;

		float hookshotSpeed = Vector3.Distance(transform.position, hookshotPosition);
		float hookshotSpeedMultiplier = 2f; 

		characterController.Move (hookshotDir * hookshotSpeed * hookshotSpeedMultiplier * Time.deltaTime);

		float reachedHookshotPosDis = 1.51f;
		if(Vector3.Distance(transform.position, hookshotPosition) < reachedHookshotPosDis)
		{
			state = State.Normal;
		}
	}
}
