using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    public float speed = 1.0f;  // Speed of the movement
    public float speedIncrease = 0.5f; // Increase speed by this amount after each hit
    public float shrinkScale = 0.9f; // Scale down by this amount after each hit. 0.9 means reduce size by 10%
    public bool godMode = false; // Toggle for "God Mode"

    private Vector3 startPosition;
    private float amplitude;  // This will determine how much the target moves up and down

    private void Start()
    {
        startPosition = transform.position;  // Remember the starting position

        // Calculate the world space screen boundaries
        float topScreenBound = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        float bottomScreenBound = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;

        // Adjust for target's height to ensure it doesn't partially move off-screen
        float targetHeight = GetComponent<SpriteRenderer>().bounds.size.y;
        topScreenBound -= targetHeight / 2;
        bottomScreenBound += targetHeight / 2;

        // Set the amplitude based on the distance between the center position and the top or bottom boundary
        // (whichever is closer to the start position)
        amplitude = Mathf.Min(Mathf.Abs(topScreenBound - startPosition.y), Mathf.Abs(bottomScreenBound - startPosition.y));
    }

    private void Update()
    {
        if (!godMode)
        {
            MoveTarget();
        }
    }

    void MoveTarget()
    {
        // Calculate new y position based on a sinusoidal function
        float newY = startPosition.y + Mathf.Sin(Time.time * speed) * amplitude;

        // Apply the new position
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            if (godMode)
            {
                // Make the arrow hit the target's center.
                collision.gameObject.transform.position = transform.position;
            }

            else
            {
                ScoreManager.instance.IncrementScore(100);
            }

            speed += speedIncrease; // Increase the speed
            transform.localScale *= shrinkScale; // Shrink the target

            Destroy(collision.gameObject);
        }
    }
}
