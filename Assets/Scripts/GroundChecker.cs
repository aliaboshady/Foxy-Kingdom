using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
	PlayerController playerController;
	private void Start()
	{
		playerController = transform.parent.GetComponent<PlayerController>();
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		playerController.canJump = true;
		playerController.playerState = PlayerController.PlayerState.idle;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		playerController.canJump = false;
	}
}
