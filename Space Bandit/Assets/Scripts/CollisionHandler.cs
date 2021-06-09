using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{
    private float timeDelay = 1f;
    private bool isTransitioning = false;
    [SerializeField] private ParticleSystem shipCrashParticles;
    [SerializeField] private AudioClip shipCrashSound;
    [SerializeField] private float timeToDie = 5f;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        // if isTransitioning is true the code below will not run
        if (isTransitioning)
        {
            return;
        }

        // method is run depending upon the case. Based on the name of the object not tag
        switch (other.gameObject.name) 
        {
            case "Enemy":
                Debug.Log("Hit Enemy");
                StartCrashSequence();
                break;
            case "Terrain":
                StartCrashSequence();
                break;
            case "Finish":
                StartFinishSequence();
                break;
        }
    }

    private void StartCrashSequence()
    {
        shipCrashParticles.Play(); // plays referenced particles
        GetComponent<MeshRenderer>().enabled = false; // disables the mesh renderer making the ship disappear
        isTransitioning = true; // sets is transitioning to true
        GetComponent<PlayerController>().enabled = false; // sets player controller to false disabling the ships movement
        audioSource.PlayOneShot(shipCrashSound);
        Destroy(gameObject, timeToDie); // Would be better to remove collsions but would have to set up an entire script for that since they are not attached to the ship
        Invoke("ReloadLevel", timeDelay); // invokes the ReloadLevel() method with a specified delay
    }

    private void ReloadLevel()
    {
        // reloads the same scene
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevelIndex);
    }

    private void StartFinishSequence()
    {
        Application.Quit(); 
    }

}
