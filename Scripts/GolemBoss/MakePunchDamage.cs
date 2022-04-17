using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePunchDamage : MonoBehaviour
{
    [SerializeField] private Collider2D punchCollider;

    private void Start()
    {
        //  punchCollider.enabled = false; 
    }

    public void EnableCollision()
    {
        punchCollider.enabled = true;
    }

    public void DisAbleCollision()
    {
        punchCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            col.GetComponent<Health>().getDamage();
        }
    }
}