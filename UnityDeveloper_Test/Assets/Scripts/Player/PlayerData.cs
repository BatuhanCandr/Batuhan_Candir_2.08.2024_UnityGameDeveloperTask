using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player", menuName = "Player/Player", order = 2)]
public class PlayerData : ScriptableObject
{
    public float speed;
    public float jumpForce;
    public float rotationSpeed;
    public float movementThreshold;
}
