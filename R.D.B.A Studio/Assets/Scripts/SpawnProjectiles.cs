using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectiles : MonoBehaviour
{
	public GameObject firePoint;
	public List<GameObject> vfx = new List<GameObject>();
	public RotateMouse rotateMouse;
	public Camera fpsCam;
	public float range = 100f;
	public Ray rayMouse;
	public ParticleSystem muzzleFlash;

	private GameObject effectToSpawn;
	private float timeToFire = 0f;
	private Vector3 pos;
	private Vector3 direction;
	private Quaternion rotation;

   
	void Start()
    {
		effectToSpawn = vfx [0]; 
    }

    
    void FixedUpdate()
    {
		if(Input.GetMouseButton(0) && Time.time > timeToFire)
		{
			timeToFire = Time.time + 1 / effectToSpawn.GetComponent<ProjectileMove>().fireRate;
			SpawnVFX();
			CameraRay();
			muzzleFlash.Play();

		}
    }

	void SpawnVFX()
	{
		GameObject vfx;

		if(firePoint != null)
		{
			vfx = Instantiate(effectToSpawn, firePoint.transform.position, Quaternion.identity);
			if(rotateMouse != null)
			{
				vfx.transform.rotation = rotateMouse.GetRotation();
			}

		}else
		{
			Debug.Log("No fire point!");	
		}
	}

	void CameraRay()
	{
		if(fpsCam != null)
		{
			var mousePos = Input.mousePosition;
			rayMouse = fpsCam.ScreenPointToRay(mousePos);
		}
	}
}






