using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   
	public CharacterController controller;
	public float speed;
	Vector3 velocity;
	public float gravity = -9.81f;
	public Transform groundCheck;
	public float groundDistance = 0.4f;
	public LayerMask groundMask;
	bool isGrounded;
	public float jumpHeight = 3f; 
	public float sprintspeed = 15f;
	public float walkspeed = 12f;

	void Start()
	{
		controller = GetComponent<CharacterController>();
	}

	void Update()
	{
		speed = walkspeed;
	
		if(isGrounded && Input.GetKey("left shift") || Input.GetKeyDown("right shift") )
		{
			speed = sprintspeed;
		}

		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

		if(isGrounded && velocity.y < 0)
		{
			velocity.y = -2f * speed;
		}

		float X = Input.GetAxis("Horizontal");
		float Z = Input.GetAxis("Vertical");

		Vector3 move = (transform.right * X + transform.forward * Z).normalized * speed + transform.up * velocity.y;

		controller.Move(move * speed * Time.deltaTime);

		if(Input.GetButtonDown("Jump") && isGrounded)
		{
			velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
		}

		velocity.y += gravity * Time.deltaTime;

		controller.Move(velocity * Time.deltaTime);





	}


}
