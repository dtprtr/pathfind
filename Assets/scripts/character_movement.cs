using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class character_movement : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    private CharacterController controller;
    public float jumpHeight = 2f;
    public float speed = 5f;
    public float gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3f;
    private float velocityY;
    [SerializeField] private bool shouldFaceMove = false;

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
        Jump(new InputAction.CallbackContext());

        if (shouldFaceMove &&_direction.sqrMagnitude > 0.001f)
        {
           Quaternion toRotation = Quaternion.LookRotation(_direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 10f);
        }

        invinciblity -= Time.deltaTime;

    }

    public void ApplyMovement()
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();
       
        Vector3 moveDirection = forward * input.y + right * input.x;
        controller.Move(moveDirection * speed * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
       _direction = new Vector3(input.x, 0, input.y);
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity * gravityMultiplier);
        }
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

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.down * groundCheckDistance, boxSize);
    }

    void Die()
    {
        Destroy(gameObject);
        Debug.Log("Player has died.");
    }




}



   