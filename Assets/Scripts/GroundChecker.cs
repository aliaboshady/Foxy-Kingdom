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
		if (isFalling)
		{
			edgeJumpWaitPassed += Time.deltaTime;
			if (edgeJumpWaitPassed >= edgeJumpWait)
			{
				playerController.canJump = false;
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		playerController.canJump = true;
		playerController.playerState = PlayerController.PlayerState.idle;
		isFalling = false;
		edgeJumpWaitPassed = 0;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		isFalling = true;
	}
}
