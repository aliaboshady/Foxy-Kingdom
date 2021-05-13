using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    Rigidbody2D rigidBody;
    float horAxis;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetMoveInput();
    }

	void FixedUpdate()
	{
        Move();
    }

	void GetMoveInput()
	{
        horAxis = Input.GetAxis("Horizontal") * moveSpeed;
    }
    void Move()
	{
        rigidBody.velocity = new Vector2(horAxis, rigidBody.velocity.y);
	}
}
