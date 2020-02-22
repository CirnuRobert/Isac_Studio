using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelsExplosion : DamgeableObjects
{
	public GameObject explosionEffect;
	public bool hasExploded = false;
	public float radius = 5f;
	public float force = 100f;
	public float damage = 100f;

   public override void Start()
	{
		base.Start();

	}

	public void Update()
	{
		if(dead == true && !hasExploded)
		{
			Explode();
			hasExploded = true;
		}

	}

	void Explode()
	{
		Debug.Log("Boom!");
		Instantiate(explosionEffect, transform.position, transform.rotation);
		Collider[] colliders = Physics.OverlapSphere(transform.position, radius);


			//add forces
		foreach(Collider nearbyObject in colliders)
		{
			Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
			if(rb != null)
			{
				rb.AddExplosionForce(force, transform.position, radius);
			}

			//add damage
			LivingEntity livingEntity = nearbyObject.GetComponent<LivingEntity>();

			if(livingEntity != null)
			{	
				livingEntity.health = livingEntity.health - damage;
				if(livingEntity.health <= 0f)
				{
					Die();
				}


			}



		}

		Destroy(gameObject);
	}
}

