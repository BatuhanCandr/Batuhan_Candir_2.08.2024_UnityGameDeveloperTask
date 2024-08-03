using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;          // Karakterin transformu
    public float mouseSensitivity = 10f;  // Fare hassasiyeti
    public float distance = 5f;          // Kamera ile karakter arasındaki mesafe
    public float height = 2f;             // Kameranın yüksekliği
    public float zoomSpeed = 2f;          // Zoom hızını belirleyen değişken
    public float minDistance = 2f;        // Minimum uzaklık
    public float maxDistance = 10f;       // Maksimum uzaklık

    private float xRotation = 0f;  // Fare hareketi ile elde edilen x rotasyonu
    private float yRotation = 0f;  // Fare hareketi ile elde edilen y rotasyonu

    private void Start()
    {
        // İlk konumlandırma
        UpdateCameraPosition();
    }

    private void Update()
    {
        if (player == null) return;

        // Fare hareketini al
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // x ve y rotasyonlarını güncelle
        xRotation -= mouseY;
        yRotation += mouseX;

        // Zoom işlemini al
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        distance -= scrollInput * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);

        // Kameranın konumunu ve rotasını hesapla
        UpdateCameraPosition();

        // Kamerayı karaktere doğru hizala
        transform.LookAt(player.position + Vector3.up * height);

        // Kamerayı düz tut
        AlignCameraWithPlayer();
    }

    private void UpdateCameraPosition()
    {
        // Kamera pozisyonunu hesapla ve güncelle
        Vector3 offset = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        Vector3 rotatedOffset = rotation * offset;
        transform.position = player.position + rotatedOffset + Vector3.up * height;
    }

    private void AlignCameraWithPlayer()
    {
        // Kamerayı karakterin arkasında ve yer çekimiyle hizala
        Vector3 direction = player.position - transform.position;
        direction.y = 0; // Y eksenindeki rotasyonu sıfırla
        transform.rotation = Quaternion.LookRotation(-direction, Vector3.up);
    }
}
