using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;


public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    Animator anim;
    private float direction;
    private float speed = 5f;
    private float jumpHeight = 8f;
    private bool isFacingRight = true;
    bool isGrounded = false;

    Vector2 moveValue;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        
    }

    // Update is called once per frame
    void Update()
    {
        Move(direction);

        if ((isFacingRight && direction == -1) || (!isFacingRight && direction == 1))
              Flip();

    }

    void OnJump()
    {
        if (isGrounded)
        {
            Jump();
        }
        Debug.Log(isGrounded);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
    }

    void OnMove(InputValue value)
    {
        float movDirection = value.Get<float>();
        direction = movDirection;
        // Move(direction.x)
    }

    private void Move(float x)
    {
        rb.velocity = new Vector2(x * speed, rb.velocity.y);

        anim.SetBool("IsRunning", x != 0);
        
    }

    private void Flip()
    {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
            Debug.Log(isGrounded);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
        Debug.Log(isGrounded);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Collectables"))
        {
            Destroy(other.gameObject);
        }
    }
}
