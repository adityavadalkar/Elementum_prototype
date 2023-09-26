using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 10f;
    public float jumpForce = 10f;
    public float thrust = 10f;
    private Rigidbody2D rgbd;
    private bool isRotating = false;
    private bool canRotate = true;
    public LayerMask ground_layers;
    public float groundCheckDistance = 5f;
    private bool wasGrounded = true;

    private Vector3 respawnPoint;
    public GameObject fallDetector;


    //private bool isFacingRight = true; 
    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        bool currentlyGrounded = isGrounded();
        float moveInput = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(moveInput, 0, 0) * moveSpeed * Time.deltaTime;

        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space)) && (Mathf.Abs(rgbd.velocity.y) < 0.001f) && currentlyGrounded)
        {
            Jump();
            //rgbd.AddTorque(5.0f * -(moveInput)); 
            //rgbd.AddTorque(5.0f * -(jumpForce)); 

        }

        if (!currentlyGrounded)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                StartCoroutine(RotateCoroutine(-90, 0.35f));
            }


            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                StartCoroutine(RotateCoroutine(90, 0.35f));
            }
        }

        wasGrounded = currentlyGrounded;

    }



    void Jump()
    {
        rgbd.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }


    IEnumerator RotateCoroutine(float angle, float duration)
    {
        isRotating = true;

        Quaternion startRotation = rgbd.transform.rotation;
        Quaternion endRotation = Quaternion.Euler(0, 0, angle) * startRotation;

        float startTime = Time.time;

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            rgbd.MoveRotation(Quaternion.Slerp(startRotation, endRotation, t));
            yield return null;
        }

        rgbd.MoveRotation(endRotation);
        isRotating = false;
    }

    private bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, ground_layers);
        Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, Color.white);
        
        return hit.collider != null;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {   //respawn player back to starting point if hit with the fallDetector or an obstacle spike
        Debug.Log(collision.tag);
        if (collision.tag == "FallDetector" || collision.tag == "Obstacle")
        {
            transform.position = respawnPoint;
        }

        if (collision.tag == "Finish")
        {


            StartCoroutine(RespawnPlayer(0.75f));

        }

    }
    IEnumerator RespawnPlayer(float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.position = respawnPoint;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
