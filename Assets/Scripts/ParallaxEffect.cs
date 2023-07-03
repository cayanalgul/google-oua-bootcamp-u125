using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform fallowTarget;

    Vector2 startingPosition;

    float startingZ;
    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition;

    float distanceFromTarget => transform.position.z - fallowTarget.transform.position.z;
    float clippingPlane => (cam.transform.position.z + (distanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    float parallaxFactor => Mathf.Abs(distanceFromTarget) / clippingPlane; 
    void Start()
    {
        
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = startingPosition + camMoveSinceStart / parallaxFactor;

        transform.position = new Vector3(newPosition.x,newPosition.y, startingZ);

    }
}
