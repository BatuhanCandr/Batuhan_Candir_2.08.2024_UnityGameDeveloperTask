using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform camTarget;
    public float pLerp = .01f;
    public float rLerp = .02f;

    public Vector2 turn;
    public float sensivity = .5f;
    
    

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, camTarget.position, pLerp);
        transform.rotation = Quaternion.Lerp(transform.rotation, camTarget.rotation, rLerp);


        turn.y += Input.GetAxis("Mouse Y") * sensivity;
        turn.x += Input.GetAxis("Mouse X") * sensivity;
        transform.localRotation = Quaternion.Euler(-turn.y,turn.x,0);
    }
}
