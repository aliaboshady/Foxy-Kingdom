using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
	[SerializeField] float explosionForceVertical = 10f;
	[SerializeField] float hitForceHorizontal = 5f;
	[SerializeField] float hitForceVertical = 10f;
	PlayerController playerController;

	private void Start()
	{
		playerController = FindObjectOfType<PlayerController>();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.transform.tag == "Player")
		{
			if (playerController.playerState == PlayerController.PlayerState.falling)
			{
				playerController.Jump(explosionForceVertical);
				Destroy(gameObject);
			}
			else
			{
				playerController.canMove = false;
				playerController.Jump(hitForceVertical, hitForceHorizontal);
			}
		}
	}

}
