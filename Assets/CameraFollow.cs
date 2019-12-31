using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerObject;
    public float distanceFromObject = 4f;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - playerObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        /*//Allow the camera to follow the sphere
        Vector3 lookOnObject = playerObject.position - transform.position;
        lookOnObject = playerObject.position - transform.position;
        transform.forward = lookOnObject.normalized;

        Vector3 playerLastPosition;
        playerLastPosition = playerObject.position - lookOnObject.normalized * distanceFromObject;
        transform.position = playerLastPosition;*/

        transform.position = playerObject.transform.position + offset;
    }
}
