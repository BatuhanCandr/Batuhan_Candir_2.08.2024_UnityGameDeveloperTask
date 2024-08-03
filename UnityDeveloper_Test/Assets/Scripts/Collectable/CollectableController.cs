using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.collectCount++;
            GameManager.Instance.Win();
            Destroy(gameObject);
           // transform.DOScale(Vector3.zero, .5f).SetEase(Ease.OutBounce);
        }
    }
}
