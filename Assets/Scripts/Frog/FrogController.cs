using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
	[SerializeField] float explosionForceVertical = 10f;
	[SerializeField] float hitForceHorizontal = 5f;
	[SerializeField] float hitForceVertical = 10f;
	[SerializeField] float verticalJump = 5f;
	[SerializeField] float horizontalJump = 10f;
	[SerializeField] float jumpWait = 1f;
	[SerializeField] float jumpEdgeWait = 3f;
	[SerializeField] float fallingGravity = 3f;
	[SerializeField] float rightEdgeX;
	[SerializeField] float leftEdgeX;

	float jumpWaitPassed = 0f;
	int direction;
	bool canJump = true;
	bool nextJumpDirectionChange = false;
	float originalGravity;
	float originalJumpWait;

	PlayerController playerController;
	Rigidbody2D rigidBody;
	Animator animator;

	enum FrogState { idle, jumping, falling }
	FrogState frogState = FrogState.idle;

	private void Start()
	{
		animator = GetComponent<Animator>();
		playerController = FindObjectOfType<PlayerController>();
		rigidBody = GetComponent<Rigidbody2D>();
		originalGravity = rigidBody.gravityScale;
		direction = transform.localScale.x < 0 ? 1 : -1;
		originalJumpWait = jumpWait;
	}

	private void Update()
	{
		if (canJump)
		{
			Jump();
		}
		else if(frogState == FrogState.idle)
		{
			jumpWaitPassed += Time.deltaTime;
			if(jumpWaitPassed >= jumpWait)
			{
				canJump = true;
				jumpWaitPassed = 0;
				jumpWait = originalJumpWait;
			}
		}

		AdjustEdgesCap();
		AdjustGravity();
		AdjustAnimationState();
	}

	void Jump()
	{
		if (nextJumpDirectionChange)
		{
			nextJumpDirectionChange = false;
			transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -direction, transform.localScale.y, 1);
		}
		frogState = FrogState.jumping;
		rigidBody.gravityScale = originalGravity;
		rigidBody.velocity = new Vector2(horizontalJump * direction, verticalJump);
		canJump = false;
	}

	void AdjustGravity()
	{
		if(rigidBody.velocity.y < -0.01f)
		{
			frogState = FrogState.falling;
			rigidBody.gravityScale = fallingGravity;
		}
	}

	void AdjustEdgesCap()
	{
		if(transform.position.x < leftEdgeX)
		{
			transform.position = new Vector3(leftEdgeX, transform.position.y, 0);
			nextJumpDirectionChange = true;
			direction = 1;
			jumpWait = jumpEdgeWait;
		}
		else if (transform.position.x > rightEdgeX)
		{
			transform.position = new Vector3(rightEdgeX, transform.position.y, 0);
			nextJumpDirectionChange = true;
			direction = -1;
			jumpWait = jumpEdgeWait;
		}
	}

	void AdjustAnimationState()
	{
		if (rigidBody.velocity.y > -0.01f && rigidBody.velocity.y < 0.01f)
		{
			frogState = FrogState.idle;
		}

		animator.SetInteger("State", (int)frogState);
	}

	void Die()
	{
		Destroy(gameObject);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.tag == "Player")
		{
			if (playerController.playerState == PlayerController.PlayerState.falling)
			{
				playerController.Jump(explosionForceVertical);
				animator.SetTrigger("Death");
			}
			else
			{
				int direction = playerController.gameObject.transform.position.x < transform.position.x ? -1 : 1;
				playerController.canMove = false;
				playerController.Jump(hitForceVertical, hitForceHorizontal * direction);
				playerController.playerState = PlayerController.PlayerState.hurt;
			}
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawLine(new Vector3(leftEdgeX, transform.position.y - 5), new Vector3(leftEdgeX, transform.position.y + 5));
		Gizmos.DrawLine(new Vector3(rightEdgeX, transform.position.y - 5), new Vector3(rightEdgeX, transform.position.y + 5));
	}

}
