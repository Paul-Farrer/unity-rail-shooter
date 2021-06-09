using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] private float timeToDestroy = 3f;

    // Start is called before the first frame update
    private void Start()
    {
        // destroyes the game objects (e.g. particle effects) so they do not stay during runtime.
        Destroy(gameObject, timeToDestroy);
    }

}
