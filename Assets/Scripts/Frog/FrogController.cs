using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
	PlayerController playerController;

	private void Start()
	{
		playerController = FindObjectOfType<PlayerController>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player")
		{
			if(playerController.playerState == PlayerController.PlayerState.falling)
			{
				playerController.Jump();
				Destroy(gameObject);
			}
			else
			{
				print("Damage Player");
			}
		}
	}
}
