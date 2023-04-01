using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();


    int cooldown = 2;
    bool keyPressed = false;

    void Awake()
    {        
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {   
        HandlePageCollection();
    }

    void FixedUpdate()
    {
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity =new Vector2( Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        // Apply movement.
        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
    }

    void HandlePageCollection()
    {
        // if the key is pressed, thortlling is over, 
        // and player collided with the page, then collect page
        if (!keyPressed
            && Input.GetMouseButtonDown(0)
            && GameManager.Instance.pageCollision)
        {

            GameManager.Instance.pageCollected = true;

            if (GameManager.Instance.IsLastPage())
                GameManager.Instance.lastPageCollected = true;

            // using a coroutine to handle throtlling
            StartCoroutine(KeyPressCooldown());
        }
    }

    IEnumerator KeyPressCooldown() 
    {
        keyPressed = true;
        yield return new WaitForSeconds(cooldown);
        keyPressed = false;
    }
}