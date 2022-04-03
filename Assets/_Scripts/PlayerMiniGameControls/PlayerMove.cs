using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayerControls
{
    [SerializeField] float playerSpeed = 2f;
    Vector2 moveDir = Vector2.zero;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveDir = mousePos - (Vector2)transform.position;
        rb.velocity = new Vector2(moveDir.normalized.x * playerSpeed, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            GameManager.Instance.UpdateGameState(GameState.LevelLose);
        }
    }
}
