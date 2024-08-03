using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player", menuName = "Player/Player", order = 2)]
public class PlayerData : ScriptableObject
{
    internal float speed;
    internal float jumpForce;
    internal float rotationSpeed;
    internal float movementThreshold;
}
