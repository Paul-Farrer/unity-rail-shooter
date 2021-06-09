using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Original time 18:00, New time 23:01 (Animation timeline)
// using UnityEngine.InputSystem;  Use this when using the new input system

public class PlayerController : MonoBehaviour
{
    // Unity's new input system. To make it work make sure the new system package is installed
    // [SerializeField] InputAction movement; 
    // [SerializeField] InputAction fire;

    [Header("General Game Setup Settings")]
    [Tooltip("How fast the ship moves up and down based upon player input")]
    [SerializeField] private float controlSpeed = 10f;
    [SerializeField] private float xRange = 5f;
    [SerializeField] private float yRange = 5f;

    [SerializeField] private GameObject[] lasers;


    [SerializeField] private float positionPitchFactor = -2f;
    [SerializeField] private float controlPitchFactor = -20f;

    [SerializeField] private float positionYawFactor = 2f;

    [SerializeField] private float controlRollFactor = 5f;

    private float horizontalThrow, verticalThrow;


    // Start is called before the first frame update
    private void Start()
    {

    }

    // enables & disables the new input system
    // void OnEnable()
    // {
    //     movement.Enable();    
    // }

    // void OnDisable() 
    // {
    //     movement.Disable();    
    // }

    // Update is called once per frame
    private void Update()
    {
        // float horizontalThrow = movement.ReadValue<Vector2>().x; This is part of the new input system
        // float verticalThrow = movement.ReadValue<Vector2>().y; This is part of the new input system

        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = verticalThrow * controlPitchFactor;

        float yawDueToPosition = transform.localPosition.x * positionYawFactor;

        float rollDueToControlThrow = horizontalThrow * controlPitchFactor;

        float pitch = verticalThrow + pitchDueToControlThrow;
        float yaw = yawDueToPosition;
        float roll = rollDueToControlThrow;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);

    }

    private void ProcessTranslation()
    {
        horizontalThrow = Input.GetAxis("Horizontal");
        verticalThrow = Input.GetAxis("Vertical");

        float xOffset = horizontalThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float yOffset = verticalThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void ProcessFiring()
    {
        foreach (var laser in lasers)
        {
            var emission = laser.GetComponent<ParticleSystem>().emission;
            emission.enabled = Input.GetButton("Fire1");
        }
    }
}
