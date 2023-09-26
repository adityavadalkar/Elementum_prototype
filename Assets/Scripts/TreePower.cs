using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePower : MonoBehaviour
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
        if (!wasGrounded && currentlyGrounded)
        {
            OnLandedTree();
        }

        // Update the wasGrounded state for the next frame
        wasGrounded = currentlyGrounded;
    }

    private bool isGrounded(){
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, Color.white);
        return hit.collider != null;
    }

    private void OnLandedTree()
    {
        Vector3 effectPosition = transform.position - new Vector3(0, groundCheckDistance, 0); // Adjust based on your needs
        GameObject effect = Instantiate(landingEffectPrefab, effectPosition, Quaternion.identity);
        StartCoroutine(ScaleEffectY(effect, scaleValue));
    }

    private IEnumerator ScaleEffectY(GameObject obj, float targetScaleY)
    {
        float duration = 2.0f; // Time to scale over
        float elapsedTime = 0f;
        Vector3 initialScale = obj.transform.localScale;
        Vector3 targetScale = new Vector3(initialScale.x, targetScaleY, initialScale.z);

        while (elapsedTime < duration)
        {
            obj.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.localScale = targetScale; // Ensure the object reaches the target scale at the end
    }
}
