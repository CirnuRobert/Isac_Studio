using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamgeableObjects : MonoBehaviour, IDamageable
{
	public float startingHealth;
	protected float health;
	protected bool dead;

	public virtual void Start()
	{
		health = startingHealth;

	}

	public void TakeHit(float damage, RaycastHit hit)
	{
		health -= damage;

		if(health <= 0f && !dead)
		{
			Die();
		}
	}

	protected void Die()
	{
		dead = true;

	}

}
