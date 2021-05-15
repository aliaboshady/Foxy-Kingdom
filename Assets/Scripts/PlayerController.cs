using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 20f;
    [SerializeField] float gravityFallMultipler = 3f;

    Rigidbody2D rigidBody;
    Animator animator;
    public enum PlayerState {idle, running, jumping, falling}
    [HideInInspector] public PlayerState playerState = PlayerState.idle;

    [HideInInspector] public bool canJump = true;
    int facingDirection;
    float gravityScale;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        facingDirection = transform.localScale.x < 0 ? -1 : 1;
        gravityScale = rigidBody.gravityScale;
    }

	void Update()
	{
        FaceRightDirection();
        Move();
        Jump();

        animator.SetInteger("State", (int)playerState);
    }

    void Move()
	{
        float horAxis = Input.GetAxis("Horizontal") * moveSpeed;

        if (horAxis > 0)
		{
            facingDirection = 1;
            playerState = PlayerState.running;
		}
        else if(horAxis < 0)
		{
            facingDirection = -1;
            playerState = PlayerState.running;
        }
		else
		{
            playerState = PlayerState.idle;
        }

        rigidBody.velocity = new Vector2(horAxis, rigidBody.velocity.y);
	}

    void Jump()
	{
        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            rigidBody.gravityScale = gravityScale;
            rigidBody.velocity = Vector2.up * jumpSpeed;
            playerState = PlayerState.jumping;
        }

        if(rigidBody.velocity.y < -0.01f)
		{
            playerState = PlayerState.falling;
            rigidBody.gravityScale = gravityScale * gravityFallMultipler;
        }
	}

    void FaceRightDirection()
	{
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * facingDirection, transform.localScale.y, transform.localScale.z);
	}
}
