using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform childTransform;
    public float speed = 5.0f;
    public float jumpForce = 10.0f;
    public LayerMask groundLayer;
    public float groundCheckDistance;
    public float rotationSpeed ;
    public float movementThreshold; 

    private bool isGrounded;
    private bool isWalking;

    void Update()
    {
        MovePlayer();
        CheckGroundStatus();
        Jump();
    }

    void MovePlayer()
    {
        
        Vector3 gravityDirection = Physics.gravity.normalized;
        Vector3 right = Vector3.Cross(gravityDirection, transform.forward);
        Vector3 forward = Vector3.Cross(right, gravityDirection);

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = (right * -moveHorizontal + forward * moveVertical).normalized * (speed * Time.deltaTime);

       
       CheckIfWalking(movement);

        transform.position += movement;

        UpdateChildRotation(movement, gravityDirection);

      
        transform.rotation = Quaternion.FromToRotation(transform.up, -gravityDirection) * transform.rotation;
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Vector3 jumpDirection = -Physics.gravity.normalized;
            rb.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
        }
    }

    void CheckGroundStatus()
    {
        Vector3 raycastDirection = Physics.gravity.normalized;
        isGrounded = Physics.Raycast(transform.position, raycastDirection, groundCheckDistance, groundLayer) ||
                     Physics.Raycast(transform.position + raycastDirection * 0.1f, raycastDirection, groundCheckDistance, groundLayer);
    }

    void UpdateChildRotation(Vector3 movement, Vector3 gravityDirection)
    {
        if (movement != Vector3.zero && childTransform != null)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement, -gravityDirection);
            childTransform.rotation = Quaternion.Slerp(childTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void CheckIfWalking(Vector3 movement)
    {
        isWalking = movement.magnitude > movementThreshold;
        if (isWalking)
        {
            GameManager.Instance.player.playerAnimationController.PlayerRunAnim();
        }
        else
        {
            GameManager.Instance.player.playerAnimationController.PlayerIdleAnim();
        }
    }
}
