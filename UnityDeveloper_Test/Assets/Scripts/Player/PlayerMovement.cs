using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform childTransform; // Çocuğun referansı
    public float speed = 5.0f;
    public float jumpForce = 10.0f;
    public LayerMask groundLayer;
    public float groundCheckDistance = 1.1f; // Varsayılan değer, ihtiyaca göre ayarlayın
    public float rotationSpeed = 5.0f; // Çocuğun dönüş hızı

    private bool isGrounded;

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

        // Hareket yönünü hesapla
        Vector3 movement = (right * -moveHorizontal + forward * moveVertical).normalized * (speed * Time.deltaTime);

        // Yeni pozisyonu uygula
        transform.position += movement;

        // Çocuğun yönünü güncelle
      //  UpdateChildRotation(movement, gravityDirection);

        // Oyuncunun bakış yönünü yerçekimi yönüne dik olacak şekilde ayarla
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
        // Yere temas olup olmadığını kontrol et
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
}
