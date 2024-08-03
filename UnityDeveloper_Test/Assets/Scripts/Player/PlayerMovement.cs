using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] internal float speed = 6.0f;
    [SerializeField] internal float rotationSpeed = 10.0f;
    [SerializeField] internal float jumpForce = 5.0f;
    [SerializeField] internal float groundCheckDistance = 0.1f;
    [SerializeField] internal LayerMask groundLayer;
    [SerializeField] internal Rigidbody rb;
    
  
    private bool _isGrounded = true;


    void Update()
    {
        Movement();
        
    }

    private void Movement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(-moveHorizontal, 0.0f, -moveVertical).normalized;

        // Karakterin zıplamasını kontrol et
        Jump();

        // Hareket ve dönüş
        RotationCheck(movement);
        Vector3 velocity = movement * speed;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;

        // Yer ile temasa göre aşağı iten kuvvet uygulama
      
    }

    
    private void ApplyDownwardForce()
    {
        if (!_isGrounded)
        {
            // Karakterin yerel Y ekseninin tersini al
            Vector3 localDownward = -transform.up;
            rb.AddForce(localDownward * 10, ForceMode.Acceleration);
        }
    }
    private void RotationCheck(Vector3 _movement)
    {
        if (_movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(_movement);
            GameManager.Instance.player.playerAnimationController.PlayerRunAnim();
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            GameManager.Instance.player.playerAnimationController.PlayerIdleAnim();
        }
    }

    private void Jump()
    {  _isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
        if (_isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    
   
}