using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    [SerializeField] internal List<GameObject> holoList;
    [SerializeField] internal float rotationSpeed = 1.0f;
    private bool isUpdatingHoloList = false;
    [Space] [Header("Current Gravity")] private float currentXGravity = 0f;
    private float currentYGravity = -9.81f;
    private float currentZGravity = 0f;

    private bool jumpRequested = false;

    void Update()
    {
        if (GameManager.Instance.isStart && !GameManager.Instance.isFail)
        {
            ChangeGravity();
            CheckInputForHoloList();
        }
        
    }

    private void ChangeGravity()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            var forwardDirection = RoundVector3Position(GameManager.Instance.player.transform.forward);
            SetGravity(forwardDirection);
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            var backDirection = RoundVector3Position(-GameManager.Instance.player.transform.forward);
            SetGravity(backDirection);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            var leftDirection = RoundVector3Position(-GameManager.Instance.player.transform.right);
            SetGravity(leftDirection);
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            var rightDirection = RoundVector3Position(GameManager.Instance.player.transform.right);
            SetGravity(rightDirection);
        }

        Physics.gravity = new Vector3(currentXGravity, currentYGravity, currentZGravity);
        UpdateRotation();
        UpdateHoloList();
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
        Quaternion targetRotation = Quaternion.FromToRotation(GameManager.Instance.player.transform.up, gravityUp) *
                                    GameManager.Instance.player.transform.rotation;
        GameManager.Instance.player.transform.rotation = Quaternion.Lerp(GameManager.Instance.player.transform.rotation,
            targetRotation, Time.deltaTime * rotationSpeed);
    }

    private void CheckInputForHoloList()
    {
        bool newIsUpdatingHoloList = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
                                     Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) ||
                                     Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) ||
                                     Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow);


      
        if (newIsUpdatingHoloList != isUpdatingHoloList)
        {
            isUpdatingHoloList = newIsUpdatingHoloList;
            if (isUpdatingHoloList)
            {
                UpdateHoloList();
            }
        }
    }

    private void UpdateHoloList()
    {
       
        holoList.ForEach(holo => holo.SetActive(false));

        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (holoList.Count > 0)
                holoList[0].SetActive(true);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (holoList.Count > 1)
                holoList[1].SetActive(true);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (holoList.Count > 2)
                holoList[2].SetActive(true);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (holoList.Count > 3)
                holoList[3].SetActive(true);
        }
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