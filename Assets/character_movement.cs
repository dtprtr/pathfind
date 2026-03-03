using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using NUnit.Framework.Internal.Commands;
public class character_movement : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 5f;
    public float gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3f;
    private float velocityY;

    public LayerMask groundLayer;
    public Vector3 boxSize;
    public float groundCheckDistance;

    public float currentHealth;
    public float maxHealth;
    

    private float invinciblity;
    public float invincibiltyTime;

    private Vector2 input;
    private Vector3 _direction;
    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {

        ApplyMovement();
        ApplyGravity();



        invinciblity -= Time.deltaTime;

    }
    public void ApplyMovement()
    {
        controller.Move(_direction * speed * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        _direction = new Vector3(input.x, 0, input.y);
    }

    private void ApplyGravity()
    {
        if (IsGrounded())
        {
            velocityY = 0f;
        }
        else
        {
            velocityY += gravity * gravityMultiplier * Time.deltaTime;
            controller.Move(new Vector3(0, velocityY, 0) * Time.deltaTime);
        }
    }
    public void TakeDamage(float damage)
    {
        
        if (invinciblity > 0)
            return;

        invinciblity = invincibiltyTime;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }

    }

    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, groundCheckDistance, groundLayer);

        return hit.collider;


    }


    void Die()
    {
        Destroy(gameObject);
        Debug.Log("Player has died.");
    }


}


   