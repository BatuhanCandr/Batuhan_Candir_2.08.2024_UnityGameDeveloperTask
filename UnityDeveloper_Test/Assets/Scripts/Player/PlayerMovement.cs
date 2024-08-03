using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    
    [SerializeField]  private Rigidbody rb;
    public float speed = 5.0f;
    public float jumpForce = 10.0f; 
    public LayerMask groundLayer; 
    public float groundCheckDistance; 

  
    private bool isGrounded;

    

    void Update()
    {
        MovePlayer();
        CheckGroundStatus();
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    void MovePlayer()
    {
        Vector3 gravityDirection = Physics.gravity.normalized;
        Vector3 right = Vector3.Cross(gravityDirection, transform.forward);
        Vector3 forward = Vector3.Cross(right, gravityDirection);

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = (right * -moveHorizontal + forward * moveVertical).normalized * (speed * Time.deltaTime);

      
        transform.position += movement;

      
        Vector3 newUp = -gravityDirection;
        transform.rotation = Quaternion.FromToRotation(transform.up, newUp) * transform.rotation;
    }

    void Jump()
    {
        Vector3 jumpDirection = -Physics.gravity.normalized;
        rb.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
    }

    void CheckGroundStatus()
    {
        
        Vector3 raycastDirection = Physics.gravity.normalized;
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, raycastDirection, out hit, groundCheckDistance, groundLayer);
        
        if (!isGrounded)
        {
            isGrounded = Physics.Raycast(transform.position + raycastDirection * 0.1f, raycastDirection, out hit, groundCheckDistance, groundLayer);
        }
    }
}
