using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// MAKE:
/// Vector direction that is modified by:
/// 1. GroupDir
/// 2. AvoidanceDir
/// 3. IntendedDir
/// 
/// The boids will have to follow a target as a group
/// </summary>
public class Boid : MonoBehaviour
{

    public Vector3 boidVelocityDir;

    [Header("Settings")]
    [SerializeField] private float boidSpeed = 10;

    [Header("Center Settings")]
    [SerializeField] private float centerDistance = 6;
    
    [Header("Target Settings")]
    [SerializeField] private float targetDistance = 6;
    [SerializeField] private float targetStrength = 3;

    [Header("Avoid Settings")]
    [SerializeField] private float avoidDistance = 1.5f;
    [SerializeField] private float avoidStrength = 5;

    [Header("alignment Settings")]
    [SerializeField] private float alignmentDistance = 6;
    [SerializeField] private float alignmentStrength = 5;

    [Header("Other boids")]
    public Transform TargetTrans;
    public List<GameObject> boidList = new List<GameObject>();
    private List<Boid> boids = new List<Boid>();

    private void Start()
    {
        foreach (var item in boidList)
        {
            boids.Add(item.GetComponent<Boid>());
        }
    }
    private void FixedUpdate()
    {
        boidVelocityDir = Vector3.zero;

        CalculateVelocity();

        transform.position += boidVelocityDir.normalized * boidSpeed * Time.fixedDeltaTime;
    }

    private void CalculateVelocity()
    {

        boidVelocityDir += Seperation().normalized * avoidStrength;
        boidVelocityDir += Alignment().normalized * alignmentStrength;

        if((Cohesion() - transform.position).magnitude > centerDistance)
            boidVelocityDir += (Cohesion() - transform.position).normalized;

        if((TargetTrans.position - transform.position).magnitude > targetDistance)
            boidVelocityDir += (TargetTrans.position - transform.position).normalized * targetStrength;
    }

    /// <summary>
    /// Avoiding other boids
    /// </summary>
    /// <returns></returns>
    private Vector3 Seperation()
    {
        Vector3 v = Vector3.zero;
        Vector3 dist;
        int index = 0;
        foreach (GameObject obj in boidList)
        {
            dist = obj.transform.position - transform.position;
            if (dist.magnitude < avoidDistance)
            {
                ++index;
                v -= obj.transform.position - transform.position;
            }
        }
        v /= index;



        return v;
    }
    /// <summary>
    /// Following the same direction as the group
    /// </summary>
    /// <returns></returns>
    private Vector3 Alignment()
    {
        Vector3 v = Vector3.zero;
        Vector3 dist;
        int index = 0;

        foreach (Boid obj in boids)
        {
            dist = obj.transform.position - transform.position;
            if (dist.magnitude > alignmentDistance)
            {
                v += obj.boidVelocityDir;
                ++index;
            }
        }
        
        v /= index;
        Debug.DrawRay(transform.position, v, Color.green);

        return v;
    }

    /// <summary>
    /// Staying as a group through calculating the middle of the flock
    /// </summary>
    /// <returns></returns>
    private Vector3 Cohesion()
    {
        Vector3 center = Vector3.zero;
        foreach (GameObject obj in boidList)
        {
            center += obj.transform.position;
        }

        center /= boidList.Count;


        return center;
    }
}
