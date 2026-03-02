using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
public class character_movement : MonoBehaviour
{
    private CharacterController controller;
    public float speed = 5f;
    public float gravity;
 

    public float currentHealth;
    public float maxHealth;
    

    private float invinciblity;
    public float invincibiltyTime;

  
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
        


        // horizontal movement (normalized to avoid faster diagonal movement)
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (move.sqrMagnitude > 1f) // more efficient than magnitude
        {
            move = move.normalized;
        }
        controller.Move(move * speed * Time.deltaTime);


        invinciblity -= Time.deltaTime;

        ApplyGravity();
    }

    private void ApplyGravity()
    {
        gravity -= 9.81f * Time.deltaTime;
        controller.Move(new Vector3(0, gravity, 0));
        if (controller.isGrounded) gravity = 0;
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



    void Die()
    {
        Destroy(gameObject);
        Debug.Log("Player has died.");
    }


}


   