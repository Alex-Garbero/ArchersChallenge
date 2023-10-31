using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetZoneEdge : MonoBehaviour
{
    public int zoneScore = 0;  // Set this value in the inspector for each zone

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            ScoreManager.instance.IncrementScore(zoneScore);
            Destroy(collision.gameObject); // Optionally destroy the arrow to prevent multiple hits
        }
    }
}
