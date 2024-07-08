using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class CameraScript : MonoBehaviour
{
    public Transform[] transforms = new Transform[2];
    public Transform targetgroup;
    private CinemachineFreeLook freeLookCamera;

    public float minRadius = 1f;
    public float maxRadius = 10f;
    public float minDistance = 1f;
    public float maxDistance = 20f;
    public float smoothingSpeed = 2f;

    private float distance; private float normalizedDistance;
    private float targetRadius; private float currentRadius;

    private void Start()
    {
        freeLookCamera = GetComponent<CinemachineFreeLook>();
    }
    private void Update()
    {
        if (transforms[0] != null && transforms[1] != null && targetgroup != null)
        {
            Vector3 positionA = transforms[0].position; Vector3 positionB = transforms[1].position;
            distance = Vector3.Distance(positionA, positionB);
            normalizedDistance = Mathf.InverseLerp(minDistance, maxDistance, distance);
            targetRadius = Mathf.Lerp(minRadius, maxRadius, normalizedDistance);
            currentRadius = Mathf.Lerp(currentRadius, targetRadius, Time.deltaTime * smoothingSpeed);
            SetRigRadius(currentRadius);
        }
    }
    void SetRigRadius(float radius)
    {
        freeLookCamera.m_Orbits[0].m_Radius = radius;
        freeLookCamera.m_Orbits[1].m_Radius = radius;
        freeLookCamera.m_Orbits[2].m_Radius = radius;
    }
}
