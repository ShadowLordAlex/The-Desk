using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class EnemyHitEvent : MonoBehaviour
{
    public void EnemyHit(GameObject other)
    {
        if (other.CompareTag("Enemy") == true)
        {
            Destroy(other);
        }
    }
}
