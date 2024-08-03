using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] internal Transform childTransform;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance;

    private bool isGrounded;
    private bool isWalking;
    private float timeSinceGrounded;

    void Update()
    {
        if (GameManager.Instance.isStart && !GameManager.Instance.isFail)
        {
            MovePlayer();
            CheckGroundStatus();
            Jump();
            CheckIfGroundedForLong();
        }
    }

    void MovePlayer()
    {
        
        Vector3 gravityDirection = Physics.gravity.normalized;
        
        Vector3 right = Vector3.Cross(gravityDirection, transform.forward);
        Vector3 forward = Vector3.Cross(right, gravityDirection);

        // Get input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

       
        Vector3 movementDirection = (forward * moveVertical + right * -moveHorizontal).normalized;
        Vector3 movement = movementDirection * (GameManager.Instance.player.playerData.speed * Time.deltaTime);

        CheckIfWalking(movement);

       
        rb.MovePosition(rb.position + movement);

     
        UpdateChildRotation(movement, gravityDirection);

       
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, -gravityDirection) * transform.rotation;
        rb.MoveRotation(targetRotation);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Vector3 jumpDirection = -Physics.gravity.normalized;
            rb.AddForce(jumpDirection * GameManager.Instance.player.playerData.jumpForce, ForceMode.Impulse);
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
            childTransform.rotation = Quaternion.Slerp(childTransform.rotation, targetRotation, GameManager.Instance.player.playerData.rotationSpeed * Time.deltaTime);
        }
    }

    void CheckIfWalking(Vector3 movement)
    {
        isWalking = movement.magnitude > GameManager.Instance.player.playerData.movementThreshold;
        if (isWalking)
        {
            GameManager.Instance.player.playerAnimationController.PlayerRunAnim();
        }
        else
        {
            GameManager.Instance.player.playerAnimationController.PlayerIdleAnim();
        }
    }

    void CheckIfGroundedForLong()
    {
        if (!isGrounded)
        {
            timeSinceGrounded += Time.deltaTime;
            if (timeSinceGrounded >= 4.0f)
            {
               GameManager.Instance.Fail();
               GameManager.Instance.uiManager.failInfo.text = "You Fall For Too Long";
            }
        }
        else
        {
            timeSinceGrounded = 0f;
        }
        GameManager.Instance.RestartScene();
    }
}
