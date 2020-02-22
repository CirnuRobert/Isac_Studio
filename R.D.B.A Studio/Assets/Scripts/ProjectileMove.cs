using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileMove : MonoBehaviour
{
  
	public float projSpeed;
	public float fireRate;
	public GameObject hitPrefab;
	public float range = 100f;
	private GameObject player;
	private GameObject firepoint;
	public LayerMask collisionMask; 
	public float damage = 10f;

	void Start()
	{
		player = GameObject.Find("Player");
	}

	void FixedUpdate()
	{
		DestroyProjectile();
		if(projSpeed != 0)
		{
			transform.position += transform.forward * (projSpeed * Time.deltaTime);
		}else
		{
			Debug.Log("No Speed!");
		}

		float moveDistance = projSpeed * Time.deltaTime;
		CheckCollisions(moveDistance);

    }

	 public void OnCollisionEnter(Collision co)
	{
		
		projSpeed = 0f;

		ContactPoint contact = co.contacts[0];
		Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
		Vector3 pos = contact.point;

		if(hitPrefab != null)
		{
			var hitVFX = Instantiate(hitPrefab, pos, rot);
			var psHit = hitVFX.GetComponent<ParticleSystem>();
			if(psHit != null)
			{
				Destroy(hitVFX, psHit.main.duration);
			}else
			{
				var psChild = hitVFX.transform.GetChild (0).GetComponent<ParticleSystem>();
				Destroy(hitVFX, psChild.main.duration);
			}
		}

		Destroy (gameObject);
	}

	void DestroyProjectile()
	{
		if(Vector3.Distance(player.transform.position, gameObject.transform.position) > range)
		{
			if(gameObject != null)
			{
				Destroy(gameObject);
			}
		}
	}

	 public void CheckCollisions(float moveDistance)
	{
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;

		if(Physics.Raycast(ray, out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide))
		{
			OnHitObject(hit);
		}
	}

	public void OnHitObject(RaycastHit hit)
	{
		IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();
		if(damageableObject != null)
		{
			damageableObject.TakeHit(damage, hit);
		} 
		Debug.Log(hit.collider.gameObject.name);
	}
}
