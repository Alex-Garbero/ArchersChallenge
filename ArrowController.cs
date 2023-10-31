using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Arrow collided with: " + other.gameObject.name);

        // Check if the arrow has collided with the target
        if (other.gameObject.CompareTag("Target"))
        {
            // Use the instance of the ScoreManager to add points
            ScoreManager.instance.IncrementScore(100);  // Add 100 points

            // Make the arrow stick to the target
            StickToTarget(other.transform);
        }
    }

    void StickToTarget(Transform target)
    {
        // Stop the arrow's physics interactions
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        // Make the arrow a child of the target, so it follows the target's movements
        transform.SetParent(target);

        // Optionally: Deactivate the arrow's collider so it doesn't trigger more hits
        GetComponent<Collider2D>().enabled = false;
    }
}
