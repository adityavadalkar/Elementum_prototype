using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePower : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    public float jumpAmount = 10;
    public float groundCheckDistance = 5f;
    public LayerMask groundLayer;
    public GameObject landingEffectPrefab; // The prefab to instantiate when the player lands
    public float scaleValue = 1f; // The y-scale value you want to set after instantiation
    private bool wasGrounded = false; // Keeps track of the previous grounded state

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bool currentlyGrounded = isGrounded();

        if (Input.GetKeyDown(KeyCode.Space) && currentlyGrounded)
        {
            rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
        }

        // Check if the player has just landed

        // Update the wasGrounded state for the next frame
        wasGrounded = currentlyGrounded;
    }

    private bool isGrounded(){
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, Color.white);
        return hit.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Destroyable")) // Assuming your object has the tag "Destroyable"
        {
            Destroy(collision.gameObject); // Destroy the GameObject
        }
    }

    // Or, if you are not using triggers:

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Destroyable")) // Assuming your object has the tag "Destroyable"
        {
            Destroy(collision.gameObject); // Destroy the GameObject
        }
    }

}
