using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMouse : MonoBehaviour
{
	public Camera fpsCam;
	public float range = 100f;
	public  Ray rayMouse;

	private Vector3 pos;
	private Vector3 direction;
	private Quaternion rotation;

	void Start()
	{
		fpsCam = GetComponentInParent<Camera>();
	}

	void Update()
    {
		if(fpsCam != null)
		{
			RaycastHit hit;
			var mousePos = Input.mousePosition;
			rayMouse = fpsCam.ScreenPointToRay(mousePos);
			if(Physics.Raycast(rayMouse.origin, rayMouse.direction, out hit, range))
			{
				RotateToMouseDirection(gameObject, hit.point);
			}else
			{
				var pos = rayMouse.GetPoint(range);
				RotateToMouseDirection(gameObject, pos);
			}
		}
    }

	void RotateToMouseDirection(GameObject obj, Vector3 destination)
	{
		direction = destination - obj.transform.position;
		rotation = Quaternion.LookRotation(direction);
		obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);
	}

	public Quaternion GetRotation()
	{
		return rotation;
	}
}
