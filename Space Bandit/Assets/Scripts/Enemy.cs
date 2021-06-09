using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject deathVFX;
    [SerializeField] private GameObject damageVFX;
    private GameObject parentGameObject;

    private ScoreBoard scoreBoard;

    [SerializeField] private int enemyHitScore = 1;
    [SerializeField] private int hitPoints = 100;
    [SerializeField] private int damageReceived = 50;

    private void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>(); // finds the object of type scoreboard and caches it
        AddRigidbody(); // calls the method when the game starts
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime"); // when the game starts it finds the object with the tag "SpawnAtRunTime" and caches it

    }

    private void AddRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>(); // adds a rigidbody to the game object
        rb.useGravity = false; // sets gravity to false so the ships dont crash into the ground
    }

    private void OnParticleCollision(GameObject other)
    {
        // when the ships particle laser collides both of these methods are called
        ReceiveDamage();
        ProcessHit();
    }

    private void ReceiveDamage()
    {
        // reduces the hitpoints by the damaged received
        hitPoints -= damageReceived;
        GameObject vfx = Instantiate(damageVFX, transform.position, Quaternion.identity);        // if hit points are less than or equal to 0 
        if (hitPoints <= 0)
        {

            vfx.transform.parent = parentGameObject.transform;
            KillEnemy();
        }
    }

    private void ProcessHit()
    {
        // when an enemy is hit the enemy hit score will increase
        scoreBoard.IncreaseScore(enemyHitScore);
    }

    private void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        float t = 0.0f;
        Destroy(gameObject, t);
    }
}
