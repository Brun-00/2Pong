using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public GameManager gameManager;
    private Rigidbody2D rb;
    public float speed = 7f;

    public void PlaceAtCenter()
    {
        transform.position = Vector3.zero;
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
    }

    public void Launch()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        rb.velocity = GetRandomDirection() * speed;
    }

    private Vector2 GetRandomDirection()
    {
        float angle;
        do
        {
            angle = Random.Range(0f, 360f);
        } while (Mathf.Abs(Mathf.Cos(angle * Mathf.Deg2Rad)) < 0.3f);

        float x = Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = Mathf.Sin(angle * Mathf.Deg2Rad);

        return new Vector2(x, y).normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector2 newVelocity = rb.velocity;
            newVelocity.y = -newVelocity.y;
            rb.velocity = newVelocity;
        }
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
        }
        if (collision.gameObject.CompareTag("WallPlayer"))
        {
            gameManager.ScoreEnemy();
        }
        if (collision.gameObject.CompareTag("WallEnemy"))
        {
            gameManager.ScorePlayer();
        }
    }
}