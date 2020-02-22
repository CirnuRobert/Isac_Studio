using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
	public float time = 3.5f;

	 void Update()
    {
		Destroy(gameObject, time);
    }
}
