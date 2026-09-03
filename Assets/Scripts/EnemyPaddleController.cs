using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPaddleController : MonoBehaviour
{
    private Rigidbody2D rb;

    // Controls how fast the paddle follows the ball.
    public float speed = 3f;

    private GameObject ball;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ball = GameObject.Find("Ball");
    }

    private void Update()
    {
        if (ball != null)
        {
            // Keeps the paddle within the playable vertical area.
            float targetY = Mathf.Clamp(ball.transform.position.y, -4f, 4f);

            Vector2 targetPosition = new Vector2(
                transform.position.x,
                targetY
            );

            // Smoothly moves the paddle toward the ball's Y position.
            transform.position = Vector2.MoveTowards(
                transform.position,
                targetPosition,
                Time.deltaTime * speed
            );
        }
    }
}