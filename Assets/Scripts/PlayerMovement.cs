using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //[SerializeField] private float speed; 
    // Part 1 - Movement
    public float speed;
    private Rigidbody2D body;

    // Part 3 - Animation
    private Animator anim;
    private bool isGrounded;
    private bool isFalling;

    // LEVEL 2 - PART 1 - TRAPS
    private float currentHealth;
    private float maxHealth;

    private void Awake()
    {
        // Part 1 - Movement
        body = GetComponent<Rigidbody2D>();

        // Part 3 - Animation
        anim = GetComponent<Animator>();

        // LEVEL 2 - PART 1 - TRAPS
        currentHealth = maxHealth;
    }

    private void Update()
    {
        // PART 1 - Movement ** 
        float x = Input.GetAxis("Horizontal") * speed; // value defined by Unity, left or A key becomes -1 or 1, 0 when nothing presse
                                                       // x, y, z
        body.velocity = new Vector2(x, body.velocity.y);

        // **

        // PART 2 - Jump **
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            // moved in part 3 (animation) body.velocity = new Vector2(body.velocity.x, speed);
            Jump();

        }


        // Flipping 
        if (x > 0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (x < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // ** 

        // Part 3 - Animation
        anim.SetBool("isRunning", x != 0);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isFalling", isFalling);

        // Falling 
        if (body.velocity.y < 0)
        {
            isFalling = true; 
        }

        // ** 
    }


    // PART 3 - ANIMATION
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        isGrounded = false;
        isFalling = false; 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Animation Controls for Jump
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            isFalling = false; 
        }
    }

    // LEVEL 2 - PART 1 - TRAPS
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Debug.Log("DEAD");
        }
    }
}