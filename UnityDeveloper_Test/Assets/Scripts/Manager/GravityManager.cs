using System.Collections;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    public Transform player; // Player objenizi buraya atayın
    public float rotationSpeed = 1.0f; // Döndürme hızı
    public float jumpForce = 5.0f; // Zıplama kuvveti

    private Vector3 currentGravity;

    void Start()
    {
        // Başlangıç yerçekimi ayarı
        currentGravity = new Vector3(0f, -9.81f, 0f);
        Physics.gravity = currentGravity;
    }

    void Update()
    {
        // Yön tuşlarına basıldığında gravity ve rotasyon değişikliği yap
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetGravityAndRotation(player.transform.forward);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetGravityAndRotation(-player.transform.forward);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetGravityAndRotation(-player.transform.right);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetGravityAndRotation(player.transform.right);
        }

        // Güncellenmiş gravity değerini fizik motoruna ilet
        Physics.gravity = currentGravity;
    }

    private void SetGravityAndRotation(Vector3 direction)
    {
        // Yönü yuvarlama
        Vector3 roundedDirection = RoundVector3(direction);

        // Gravity ayarla
        SetGravity(roundedDirection);

        // Rotasyonu güncelle
        UpdateRotation();
    }

    private Vector3 RoundVector3(Vector3 vector)
    {
        return new Vector3(
            Mathf.Round(vector.x),
            Mathf.Round(vector.y),
            Mathf.Round(vector.z)
        );
    }

    private void SetGravity(Vector3 gravityDirection)
    {
        currentGravity = 9.81f * gravityDirection;
    }

    private void UpdateRotation()
    {
        // Yerçekimi yönünün tersine bakacak yukarı yönünü hesapla
        Vector3 upDirection = -currentGravity.normalized;

        // İleri yönü (forward) mevcut bakış yönüne (camera veya player yönü) göre ayarla
        Vector3 forwardDirection = Vector3.Cross(player.right, upDirection).normalized;

        // Rotasyonu ayarla
        Quaternion targetRotation = Quaternion.LookRotation(forwardDirection, upDirection);
        player.rotation = targetRotation;
    }
}
