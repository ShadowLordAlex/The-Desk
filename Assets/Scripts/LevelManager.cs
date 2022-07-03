using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 3;
    public int kills;
    public int hitTreshold = 50;
    public bool outOfCover;
    public GameObject[] Healthbar;
    
    public GameObject ScoreObject;
    [HideInInspector]
    public Text Score;

    private void Start()
    {
        Score = ScoreObject.GetComponent<Text>();
    }

    public void PLayerHit(double hitchance)
    {
        Debug.Log(hitchance);
        if (hitchance >= hitTreshold)
        {
            if (outOfCover)
            {
                Debug.Log("That hurts");
                Destroy(Healthbar[health - 1]);
                health--;
            }
        }
    }

    private void Update()
    {
        Score.text = "Score: " + kills;
    }

    //KillScore
    public void KillScore()
    {
        kills++;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            outOfCover = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            outOfCover = false;

    }
}
