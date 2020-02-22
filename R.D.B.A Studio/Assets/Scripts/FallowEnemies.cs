using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (UnityEngine.AI.NavMeshAgent))]
public class FallowEnemies : LivingEntity
{
	UnityEngine.AI.NavMeshAgent pathfinder;
	Transform target;
	public float refreshRate = 1f;


	public override void Start()
	{
		base.Start();
		pathfinder = GetComponent<UnityEngine.AI.NavMeshAgent>();
		target = GameObject.FindGameObjectWithTag("Player").transform;

		StartCoroutine (UpdatePath ());
	}

	IEnumerator UpdatePath()
	{	
		while (target != null)
		{
			Vector3 targetPosition = new Vector3(target.position.x, 0f, target.position.z);
			pathfinder.SetDestination(targetPosition);
			yield return new WaitForSeconds(refreshRate);
		}
	}
}
