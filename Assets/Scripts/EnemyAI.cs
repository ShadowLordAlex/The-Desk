using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    /// <summary>
    /// Greetings
    /// This code is meant for the project "The Desk" Feel free to use it for the other projects
    /// </summary>
    // Start is called before the first frame update
    [Header("====General Stats====")]
    public int enemyCase = 0;
    public bool walking = false;
    public GameObject player;
    public int RotationSpeed = 5;

    //Player damage valuables
    [Header("====WeaponStats====")]
    public float maxHitChance = 100;
    public int enemyAmmoStart = 3;
    public int enemyAmmo;
    protected bool allowedToShoot;
    
    [Header("=====Speed Stats======")]
    public float shootCoolDown = 3f;
    public float reloadSpeed = 3f;
    protected bool alreadyShooting;
    
    //other
    [HideInInspector]
    public GameObject[] Points;
    public NavMeshAgent agent;
    public LevelManager LevelManager;
    protected int destinationNumber;
    
    enum EnemyState
    {
        SPAWNED,
        ARRIVED,
        SHOOTING,
        WALKING
        
    }


    void Start()
    {
        Points = GameObject.FindGameObjectsWithTag("MovePoint");
        MovePlayer();
        player = GameObject.FindGameObjectWithTag("Player");
        LevelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        enemyAmmo = enemyAmmoStart;
    }

    // Update is called once per frame
    void Update()
    {
       

        if (walking)
        {
            if (!agent.hasPath)
            {
            //ToDo Make a shoot player script
            var targetRotation = Quaternion.LookRotation(player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
            
            if(!alreadyShooting)
            Shoot();
            }
        }
    }
    //END REMOVAL PART

    protected void MovePlayer()
    {
        destinationNumber = Random.Range(0, Points.Length);
        //Debug.Log("I am going to posistion " + destinationNumber);
        agent.destination = Points[destinationNumber].transform.position;
        walking = true;
        //AudioManager.Play("SmallGun");

    }

    private void Shoot()
    {
        alreadyShooting = true;
        if (allowedToShoot)
        {
            if (enemyAmmo >= 1)
            {
                AudioManager.instance.Play("SmallGun");
                enemyAmmo--;
                Debug.Log("I Shot a bullet!");
                double hitChance = Math.Round(Random.Range(0, maxHitChance), 1);
                LevelManager.PLayerHit(hitChance);
                allowedToShoot = false;
                alreadyShooting = false;
                
            }
            else
            {
                ReloadSound();
                Debug.Log("Reloading");
                Invoke(nameof(ReloadGun), reloadSpeed);
            }
        }
        else
        {
            Invoke(nameof(GunCoolDown),shootCoolDown);
            Debug.Log("CoolingDown The Gun :D");
            
        }
    }

    protected void ReloadGun()
    {
        AudioManager.instance.Play("ReloadDone");
        Debug.Log("Reloaded the Ammo");
        enemyAmmo = enemyAmmoStart;
        alreadyShooting = false;
    }

    protected void GunCoolDown()
    {
        allowedToShoot = true;
        alreadyShooting = false;
    }

    private void OnDestroy()
    {
        LevelManager.KillScore();
        LevelManager.AddMoney(enemyCase);
    }

    public void ReloadSound()
    {
        int audioNumber = Random.Range(1, 3);
        AudioManager.instance.Play("Reload" + audioNumber);
        Debug.Log("Reload" + audioNumber);

    }
    
}