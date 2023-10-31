using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BowController : MonoBehaviour
{
    public GameObject arrowPrefab;  // Drag your arrow prefab here in the inspector
    public float chargeSpeed = 1.0f;  // Adjust for how fast the shot charges
    public float maxCharge = 10.0f;  // Maximum charge power

    public Sprite bowStage1; // Empty bow
    public Sprite bowStage2; // Arrow knocked
    public Sprite bowStage3; // Arrow drawn back

    private float currentCharge = 0.0f;
    private bool isCharging = false;
    private SpriteRenderer spriteRenderer;

    public Slider chargeSlider;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        chargeSlider.maxValue = maxCharge; // Set the maximum value for the slider
        chargeSlider.value = 0; // Initialize the slider to 0
    }

    void Update()
    {
        HandleMovement();
        HandleCharging();
        HandleShooting();
        UpdateBowSprite();
        UpdateChargeSlider();
    }

    void HandleMovement()
    {
        // Convert mouse position to world position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Get the top and bottom borders
        float topBorder = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        float bottomBorder = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;

        // Adjust the borders to account for the bow's height, assuming the pivot point of the sprite is in the center
        topBorder -= GetComponent<SpriteRenderer>().bounds.size.y / 2;
        bottomBorder += GetComponent<SpriteRenderer>().bounds.size.y / 2;

        // Clamp the y position of the mouse to be within the borders
        float clampedY = Mathf.Clamp(mousePos.y, bottomBorder, topBorder);

        // Set the bow's vertical position, while keeping its x position constant
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
    }

    void HandleCharging()
    {
        if (Input.GetMouseButtonDown(0))  // Left mouse button pressed
        {
            isCharging = true;
        }

        if (isCharging)
        {
            // Increase charge value up to the maximum
            currentCharge += chargeSpeed * Time.deltaTime;
            currentCharge = Mathf.Clamp(currentCharge, 0, maxCharge);
        }
    }

    void HandleShooting()
    {
        if (Input.GetMouseButtonUp(0) && isCharging)  // Left mouse button released and was charging
        {
            FireArrow();
            isCharging = false;
            currentCharge = 0.0f;  // Reset charge
        }
    }

    void FireArrow()
    {
        // Instantiate the arrow at the bow's position
        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();

        if (arrowRb)
        {
            float launchForce = 10.0f;  // Adjust this value to your liking
            arrowRb.AddForce(new Vector2(launchForce * currentCharge, 0), ForceMode2D.Impulse);
        }
    }

    void UpdateBowSprite()
    {
        if (!isCharging)
        {
            spriteRenderer.sprite = bowStage1;
        }
        else if (isCharging && currentCharge < 1.0f)
        {
            spriteRenderer.sprite = bowStage2;
        }
        else if (isCharging && currentCharge >= 1.0f)
        {
            spriteRenderer.sprite = bowStage3;
        }
    }

    void UpdateChargeSlider()
    {
        if (isCharging)
        {
            // Update the charge slider to match the current charge value
            chargeSlider.value = currentCharge;
        }
        else
        {
            // Reset the slider to 0 when not charging
            chargeSlider.value = 0;
        }
    }
}
