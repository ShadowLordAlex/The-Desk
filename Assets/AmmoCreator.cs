using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCreator : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform spawnLocation;
    public GameObject ammo;

    private int ammoLeft = 2;
    public Text ammoCount;

    private void Update()
    {
        ammoCount.text = ammoLeft.ToString();
    }


    public void DropAmmo()
    {
        if (ammoLeft >= 1)
        {
            Instantiate(ammo, spawnLocation);
            ammoLeft--;
        }
    }

    public void RefundAmmo(int What)
    {
        switch (What)
        {
            case 1:
               ammoLeft++;
               break;
            
            default:
                Debug.Log(What.ToString());
                break;
        }
        
    }
}
