using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
	public float startingHealth;
	public float health;
	private bool dead;

	public virtual void Start()
	{
		health = startingHealth;
		BarrelsExplosion be = GetComponent<BarrelsExplosion>();
	}

	public void TakeHit(float damage, RaycastHit hit)
	{
		health -= damage;

		if(health <= 0f && !dead)
		{
			Die();
		}
	}

	public void Update()
	{
		if(health <= 0f && !dead)
		{
			Die();
		}

	}

	protected void Die()
	{
		dead = true;
		GameObject.Destroy(gameObject);
	}

}
