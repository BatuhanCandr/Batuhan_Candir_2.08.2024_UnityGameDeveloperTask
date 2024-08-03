using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;  // Cinemachine Virtual Camera referansı
    public float mouseSensitivity = 10f;            // Fare hassasiyeti
    public float zoomSpeed = 2f;                    // Zoom hızını belirleyen değişken
    public float minFieldOfView = 20f;              // Minimum zoom seviyesi (Field of View)
    public float maxFieldOfView = 80f;              // Maksimum zoom seviyesi (Field of View)

    private float yRotation = 0f;  // Fare hareketi ile elde edilen y rotasyonu

    private void Start()
    {
        // Başlangıçta Virtual Camera'nın başlangıç konumunu ayarlayın
        if (virtualCamera != null)
        {
            virtualCamera.transform.position = virtualCamera.Follow.position - virtualCamera.transform.forward * 5f;
        }
    }

    private void Update()
    {
        if (virtualCamera == null) return;

        // Fare hareketini al
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // x ve y rotasyonlarını güncelle
        yRotation += mouseX;

        // Kamera rotasını hesapla
        Quaternion rotation = Quaternion.Euler(0f, yRotation, 0f);
        virtualCamera.transform.rotation = rotation;

        // Zoom işlemini al
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        AdjustZoom(scrollInput);
    }

    private void AdjustZoom(float scrollInput)
    {
        // Zoom işlemini gerçekleştir
        var lens = virtualCamera.m_Lens;
        lens.FieldOfView -= scrollInput * zoomSpeed;
        lens.FieldOfView = Mathf.Clamp(lens.FieldOfView, minFieldOfView, maxFieldOfView);
    }
}