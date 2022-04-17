using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private int startingPoint;
    [SerializeField] private float maxDistance; 
    [SerializeField] private Transform[] points;
    
  

    private int i; 
    void Start()
    {
        transform.position = points[startingPoint].position; 
    }

    void Update()
    {
        
        float distance = Vector2.Distance(transform.position, points[i].position);
        if (distance < maxDistance)
        {
            i++;
            if (i == points.Length)
            {
                i = 0; 
            }
        }
        
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        
    }

}
