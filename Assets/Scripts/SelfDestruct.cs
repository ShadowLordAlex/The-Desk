using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    // Start is called before the first frame update
    public float KillTimer = 4;
    void Start()
    {
        Object.Destroy(gameObject, KillTimer);
    }
}
