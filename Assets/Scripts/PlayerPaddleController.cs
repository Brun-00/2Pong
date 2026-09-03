using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPaddleController : MonoBehaviour
{
    public float speed = 5f;

    public bool isPlayer = true;
    public SpriteRenderer spriteRenderer;

    public string movementAxisName = "Vertical";

    void Start()
    {
        // Applies the color selected by the player to the correct paddle.
        if (isPlayer)
            spriteRenderer.color = SaveController.Instance.colorPlayer;
        else
            spriteRenderer.color = SaveController.Instance.colorEnemy;
    }

    private void Update()
    {
        float moveInput = Input.GetAxis(movementAxisName);

        Vector3 newPosition = transform.position
            + Vector3.up * moveInput * speed * Time.deltaTime;

        // Keeps the paddle inside the playable area.
        newPosition.y = Mathf.Clamp(newPosition.y, -4f, 4f);

        transform.position = newPosition;
    }
}