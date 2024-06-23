using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : Character
{
    [SerializeField] private Rigidbody rb;
    [HideInInspector] public Joystick Joystick;
    [SerializeField] private float speed = 5;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsState(GameState.GamePlay))
        {
            Move();
        }
    }

    public void Move()
    {
        // Check Input
        float horizontal = Joystick.Horizontal;
        float vertical = Joystick.Vertical;

        // Convert to vector3
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical);

        // If there's input, rotate the player
        if (moveDirection != Vector3.zero)
        {
            
            Quaternion lookRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

            // Check if the player is on the ground
            Vector3 nextPoint = transform.position + transform.forward * speed * Time.deltaTime;
            ChangeAnim("running");
            if(CanMove(nextPoint))
            {
                if(CheckGround(nextPoint))
                {
                    // If the player is on the ground, move them
                    rb.MovePosition(nextPoint);
                }
            }
        }
        else
        {
            ChangeAnim("idle");
        }    
    }
}
