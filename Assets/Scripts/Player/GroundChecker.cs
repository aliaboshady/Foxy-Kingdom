using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
	PlayerController playerController;
	float edgeJumpWait;
	float edgeJumpWaitPassed;
	bool isFalling = false;
	private void Start()
	{
		playerController = transform.parent.GetComponent<PlayerController>();
		edgeJumpWait = playerController.edgeJumpWait;
	}

	private void Update()
	{
		EdgeJump();
	}

	void EdgeJump()
	{
		if (isFalling && playerController.playerState != PlayerController.PlayerState.hurt)
		{
			edgeJumpWaitPassed += Time.deltaTime;
			if (edgeJumpWaitPassed >= edgeJumpWait)
			{
				playerController.canJump = false;
				playerController.playerState = PlayerController.PlayerState.jumping;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Ground")
		{
			playerController.canJump = true;
			playerController.canMove = true;
			playerController.playerState = PlayerController.PlayerState.idle;
			isFalling = false;
			edgeJumpWaitPassed = 0;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Ground")
		{
			isFalling = true;
			playerController.ResetGravity();
			playerController.increasedGravity = false;
		}
	}
}
