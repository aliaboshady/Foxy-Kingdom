using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleController : MonoBehaviour
{
	[SerializeField] float explosionForceVertical = 15f;
	[SerializeField] float hitForceHorizontal = 5f;
	[SerializeField] float hitForceVertical = 10f;

	PlayerController playerController;
	Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();
		animator.Play("Flying", -1, Random.Range(0.0f, 1.0f));
		playerController = FindObjectOfType<PlayerController>();
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
}
