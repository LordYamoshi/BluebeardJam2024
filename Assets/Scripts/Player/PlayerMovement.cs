using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public CinemachineVirtualCamera virtualCamera;
    public float cameraFollowSpeed = 3f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private CinemachineTransposer transposer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        if (virtualCamera != null)
        {
            transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            if (transposer != null)
            {
                transposer.m_XDamping = cameraFollowSpeed;
                transposer.m_YDamping = cameraFollowSpeed;
            }
        }
    }

    void Update()
    {
        // Get input from the player
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        
        // Normalize the movement vector to ensure consistent speed in all directions
        movement = movement.normalized;
    }

    void FixedUpdate()
    {
        // Move the player using the Rigidbody2D component
        rb.velocity = movement * moveSpeed;

        // Update the Cinemachine Virtual Camera's follow target to follow the player
        if (virtualCamera != null)
        {
            virtualCamera.Follow = transform;
        }
    }
}
