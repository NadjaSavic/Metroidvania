using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private int keyValue;

    private void OnTriggerEnter2D(Collider2D col)
    {
        GameManager.Instance.AddKey(keyValue);
        Destroy(gameObject);
    }
}
