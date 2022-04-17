using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tropfen : MonoBehaviour
{
    [SerializeField] private ParticleSystem _impact;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Ground"))
        {
            _impact.Play();
            Destroy(gameObject);
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Player>().Die();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player") || col.collider.CompareTag("Ground"))
        {
            _impact.Play();
            Destroy(gameObject);
            if (col.collider.CompareTag("Player"))
            {
                col.collider.GetComponent<Player>().Die();
            }
        }
    }
}