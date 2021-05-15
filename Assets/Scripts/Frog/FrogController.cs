using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
	[SerializeField] float explosionForce = 10f;
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
				playerController.Jump(explosionForce);
				Destroy(gameObject);
			}
			else
			{
				print("Damage Player");
			}
		}
	}

}
