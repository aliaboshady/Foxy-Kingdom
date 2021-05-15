using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 20f;
    [SerializeField] float gravityJumpDivider = 5f;
    [SerializeField] float gravityFallMultipler = 3f;
    [SerializeField] float hurtWait = 1f;
    public float edgeJumpWait = 0.2f;

    Rigidbody2D rigidBody;
    Animator animator;
    public enum PlayerState {idle, running, jumping, falling, hurt}
    [HideInInspector] public PlayerState playerState = PlayerState.idle;

    [HideInInspector] public bool canJump = false;
    [HideInInspector] public bool canMove = true;
    [HideInInspector] public bool increasedGravity = false;
    int facingDirection;
    float gravityScale;
    float hurtWaitPassed;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        facingDirection = transform.localScale.x < 0 ? -1 : 1;
        rigidBody.gravityScale = jumpSpeed / gravityJumpDivider;
        gravityScale = rigidBody.gravityScale;

        playerState = PlayerState.hurt;
    }

	void Update()
	{
        FaceRightDirection();

        if (playerState == PlayerState.hurt)
        {
            ResetHurtAfterTime();
        }
        else
        {
            hurtWaitPassed = 0;

            if (canMove)
            {
                Move();
            }

            if (canJump && Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
            else if (!canJump)
            {
                if (Input.GetKeyUp(KeyCode.Space) && !increasedGravity)
                {
                    IncreaseGravity();
                }
                if (rigidBody.velocity.y < -0.01f)
                {
                    Fall();
                }
                else if (rigidBody.velocity.y > 0.01f)
                {
                    playerState = PlayerState.jumping;
                }
            }
        }

        print(playerState);
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

    void ResetHurtAfterTime()
	{
        hurtWaitPassed += Time.deltaTime;
        if(hurtWaitPassed >= hurtWait)
		{
            playerState = PlayerState.idle;
            canMove = true;
        }
	}

    public void Jump(float jumpSpeedVertical = -1, float jumpSpeedHorizontal = -1)
	{
        ResetGravity();
        jumpSpeedVertical = jumpSpeedVertical == -1 ? jumpSpeed : jumpSpeedVertical;
        jumpSpeedHorizontal = jumpSpeedHorizontal == -1 ? 0 : jumpSpeedHorizontal;
        rigidBody.velocity = new Vector2(jumpSpeedHorizontal, jumpSpeedVertical);

        playerState = PlayerState.jumping;
        canJump = false;
    }

    void Fall()
	{
        playerState = PlayerState.falling;
        IncreaseGravity();
        increasedGravity = true;
    }

    void IncreaseGravity()
	{
        rigidBody.gravityScale = gravityScale * gravityFallMultipler;
    }

	public void ResetGravity()
	{
        rigidBody.gravityScale = gravityScale;
    }

	void FaceRightDirection()
	{
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * facingDirection, transform.localScale.y, transform.localScale.z);
	}

}
