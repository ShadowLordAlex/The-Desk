using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioManager audioManager;
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            FindObjectOfType<AudioManager>().Play("Testing");
            Debug.Log("Shoot");
        }
    }
}
