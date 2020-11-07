using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]

public class CameraTracker : MonoBehaviour

{
    public Transform target;
    public float smoothTime;
    public float targetZPosition = -10f;

    private Vector3 interpolatedPosition;
    private Vector3 cameraVelocity = Vector3.zero;


    // LateUpdate is called after all other objects have moved
    void LateUpdate()
    {
        if (target != null)
        {
            interpolatedPosition = Vector3.SmoothDamp(transform.position, target.position, ref cameraVelocity, smoothTime);
            interpolatedPosition.z = targetZPosition;
            transform.position = interpolatedPosition;
        }
    }

}
