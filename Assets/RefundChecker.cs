using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefundChecker : MonoBehaviour
{
    public LevelManager LevelManager;
    public int refundAmount = 25;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AmmoSmall"))
        {
            Destroy(other.gameObject);
            LevelManager.AddMoneyInNumber(refundAmount);
            
        }
    }
}
