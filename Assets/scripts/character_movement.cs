using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class character_movement : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
  

    private CharacterController controller;
    public float jumpHeight = 2f;
    public float speed = 5f;
    private float gravity = -9.81f;
    [SerializeField] private float gravityMultiplier = 3f;
    private float velocityY;
    private float velocity;
    [SerializeField] private bool shouldFaceMove = false;

    public LayerMask groundLayer;
    public Vector3 boxSize;
    public float groundCheckDistance;

    public float currentHealth;
    public float maxHealth;


    private float invinciblity;
    public float invincibiltyTime;

    private Vector2 moveInput;
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

        ApplyGravity();

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * moveInput.y + right * moveInput.x;
        controller.Move(moveDirection * speed * Time.deltaTime);

        if (shouldFaceMove && moveDirection.sqrMagnitude > 0.001f)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 10f);
        }

        velocityY += gravity * gravityMultiplier * Time.deltaTime;
        controller.Move(new Vector3(0, velocityY, 0) * Time.deltaTime);




        invinciblity -= Time.deltaTime;

    }
   

    private void ApplyGravity()
    {
        if (controller.isGrounded && velocityY < 0.0f)
        {
            velocity = -1.0f;

        }
        else 
        {
            velocity+=gravity * gravityMultiplier * Time.deltaTime;
            _direction.y = velocity;
        }
    }
    public void ApplyMovement(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        _direction = new Vector3(moveInput.x, 0, moveInput.y);

       
    }

    public void Jump(InputAction.CallbackContext context)
    {
       if (context.started && controller.isGrounded)
       {
            velocityY = Mathf.Sqrt(jumpHeight * -2f * gravity * gravityMultiplier);
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

    private bool isGrounded() => controller.isGrounded;
    


}



   