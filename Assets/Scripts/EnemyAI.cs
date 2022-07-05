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
    protected int destinationNumber;
    public GameObject[] Points;
    public NavMeshAgent agent;
    public bool walking = false;
    public GameObject player;
    public int RotationSpeed = 5;
    public LevelManager LevelManager;
    public int enemyCase = 0;
    
    //Player damage valuables
    public float maxHitChance = 100;
    protected bool allowedToShoot;
    protected float shootCoolDown = 3f;
    public int enemyAmmoStart = 3;
    public int enemyAmmo;
    public float reloadSpeed = 3f;
    protected bool alreadyShooting;
    
    //Audio Stuff
    public AudioSource ReloadAudioSource;
    public AudioSource ShootAudioClip;

    

    enum EnemyState
    {
        SPAWNED,
        ARRIVED,
        SHOOTING,
        WALKING
        
    }

    //Press enter to make the player go to a random spot given in the list
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

    }

    private void Shoot()
    {
        alreadyShooting = true;
        if (allowedToShoot)
        {
            if (enemyAmmo >= 1)
            {
                ShootAudioClip.Play();
                enemyAmmo--;
                Debug.Log("I Shot a bullet!");
                double hitChance = Math.Round(Random.Range(0, maxHitChance), 1);
                
                LevelManager.PLayerHit(hitChance);
                allowedToShoot = false;
                alreadyShooting = false;
            }
            else
            {
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
        Debug.Log("Reloaded the Ammo");
        ReloadAudioSource.Play();
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
}