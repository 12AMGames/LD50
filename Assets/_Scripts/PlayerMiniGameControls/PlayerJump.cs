using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : PlayerControls
{
    [SerializeField] float jumpForce = 2f;
    [SerializeField] float rayLength = 1f;
    [SerializeField] LayerMask groundLayer;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void MouseDown()
    {
        base.MouseDown();
        if (!Physics2D.Raycast(transform.position, Vector2.down, rayLength, groundLayer))
            return;
        AudioManager.instance.Play("PlayerJump");
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            GameManager.Instance.UpdateGameState(GameState.LevelLose);
        }
    }
}
