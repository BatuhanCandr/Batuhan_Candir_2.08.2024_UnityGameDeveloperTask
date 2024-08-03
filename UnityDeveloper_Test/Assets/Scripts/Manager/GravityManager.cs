using System.Collections;
using UnityEngine;
using DG.Tweening; // DoTween kütüphanesi için

public class GravityManager : MonoBehaviour
{
    public Transform player; // Player objenizi buraya atayın
    public float rotationSpeed = 1.0f; // Döndürme hızı
    public float jumpForce = 5.0f; // Zıplama kuvveti
    
    public float currentXGravity = 0f;
    public float currentYGravity = -9.81f;
    public float currentZGravity = 0f;

    private bool jumpRequested = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            var forwardDirection = RoundVector3Position(player.transform.forward);
            SetGravity(forwardDirection);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            var backDirection = RoundVector3Position(-player.transform.forward);
            SetGravity(backDirection);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            var leftDirection = RoundVector3Position(-player.transform.right);
            SetGravity(leftDirection);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            var rightDirection = RoundVector3Position(player.transform.right);
            SetGravity(rightDirection);
        }

        Physics.gravity = new Vector3(currentXGravity, currentYGravity, currentZGravity);
        UpdateRotation();
    }

    private void SetGravity(Vector3 gravityDirection)
    {
        currentXGravity = 9.81f * gravityDirection.x;
        currentYGravity = 9.81f * gravityDirection.y;
        currentZGravity = 9.81f * gravityDirection.z;
    }

    private void UpdateRotation()
    {
        Vector3 gravityUp = -Physics.gravity.normalized;
        Quaternion targetRotation = Quaternion.FromToRotation(player.up, gravityUp) * player.rotation;
        player.rotation = Quaternion.Lerp(player.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }

    Vector3 RoundVector3Position(Vector3 position)
    {
        return new Vector3(
            Mathf.Round(position.x),
            Mathf.Round(position.y),
            Mathf.Round(position.z)
        );
    }

    
}