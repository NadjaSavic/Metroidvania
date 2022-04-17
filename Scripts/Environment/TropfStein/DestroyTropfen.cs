using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DestroyTropfen : MonoBehaviour
{
    [SerializeField] private float destructionTime; 
    void Start()
    {
        Destroy(gameObject,destructionTime);
    }
}
