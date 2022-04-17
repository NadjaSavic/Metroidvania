using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Android;

public class Player : MonoBehaviour
{
    public static Player Instance;
    [SerializeField] private ParticleSystem particles;
    private Rigidbody2D rb; 

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else Instance = this; 
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }
    public void Die()
    {
        GameManager.Instance.Die();
        Destroy(gameObject.GetComponent<Movement>());
        Destroy(gameObject.GetComponentInChildren<SpriteRenderer>());
        Destroy(gameObject.GetComponent<CapsuleCollider2D>());
        Destroy(gameObject.GetComponent<Rigidbody2D>());
        rb.velocity = Vector2.zero;
        particles.Play();
        AudoManager.instance.Play("Death");
    }
}
