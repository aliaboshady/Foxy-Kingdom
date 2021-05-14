using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 20f;

    Rigidbody2D rigidBody;
    Animator animator;
    bool canJump = true;
    int facingDirection;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        facingDirection = transform.localScale.x < 0 ? -1 : 1;
    }

	void Update()
	{
        FaceRightDirection();
        Move();
        if(canJump && Input.GetKeyDown(KeyCode.Space))
		{
            Jump();
		}
    }

    void Move()
	{
        float horAxis = Input.GetAxis("Horizontal") * moveSpeed;

        animator.SetBool("isRunning", true);
        if (horAxis > 0)
		{
            facingDirection = 1;
		}
        else if(horAxis < 0)
		{
            facingDirection = -1;
		}
		else
		{
            animator.SetBool("isRunning", false);
        }
        
        rigidBody.velocity = new Vector2(horAxis, rigidBody.velocity.y);
	}

    void Jump()
	{
        rigidBody.velocity += Vector2.up * jumpSpeed;
	}

    void FaceRightDirection()
	{
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * facingDirection, transform.localScale.y, transform.localScale.z);
	}
}