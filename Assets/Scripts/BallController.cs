using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameManager gameManager;

    private Rigidbody2D rb;

    public float speed = 7f;

    public AudioSource hitSound;
    public AudioSource scoreSound;

    public void PlaceAtCenter()
    {
        // Resets the ball to the center and stops its movement.
        transform.position = Vector3.zero;

        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        rb.velocity = Vector2.zero;
    }

    public void Launch()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();

        // Launches the ball in a random direction.
        rb.velocity = GetRandomDirection() * speed;
    }

    private Vector2 GetRandomDirection()
    {
        float angle;

        // Avoids launching the ball too close to a vertical direction.
        do
        {
            angle = Random.Range(0f, 360f);
        }
        while (Mathf.Abs(Mathf.Cos(angle * Mathf.Deg2Rad)) < 0.3f);

        float x = Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = Mathf.Sin(angle * Mathf.Deg2Rad);

        return new Vector2(x, y).normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // Reflects the ball vertically when it hits a wall.
            Vector2 newVelocity = rb.velocity;
            newVelocity.y = -newVelocity.y;
            rb.velocity = newVelocity;
        }

        if (collision.gameObject.CompareTag("Player") ||
            collision.gameObject.CompareTag("Enemy"))
        {
            // Reflects the ball horizontally when it hits a paddle.
            rb.velocity = new Vector2(
                -rb.velocity.x,
                rb.velocity.y
            );

            // Adds some variation to the paddle hit sound.
            hitSound.pitch = Random.Range(0.7f, 1.3f);
            hitSound.Play();
        }

        if (collision.gameObject.CompareTag("WallPlayer"))
        {
            // The ball reached the player's side, so the enemy scores.
            gameManager.ScoreEnemy();

            scoreSound.pitch = Random.Range(0.7f, 1.3f);
            scoreSound.Play();
        }

        if (collision.gameObject.CompareTag("WallEnemy"))
        {
            // The ball reached the enemy's side, so the player scores.
            gameManager.ScorePlayer();

            scoreSound.pitch = Random.Range(0.7f, 1.3f);
            scoreSound.Play();
        }
    }
}