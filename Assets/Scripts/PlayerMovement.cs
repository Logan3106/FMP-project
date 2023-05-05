using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;

    public float speedMove = 50f;
    public float gravity = -9.80f;
    public float Jump = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 speed;
    bool isgrounded;

    public float health;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        TakeDamage(10);
        PlayerMove();
    }


    void PlayerMove()
    {
        isgrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isgrounded && speed.y < 0)
        {
            speed.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        characterController.Move(move * speedMove * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isgrounded)
        {
            speed.y = Mathf.Sqrt(Jump * -2f * gravity);
        }


        speed.y += gravity * Time.deltaTime;

        characterController.Move(speed * Time.deltaTime); 
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyPlayer), .5f);
    }

    public void DestroyPlayer()
    {
        Destroy(gameObject);
    }
}
