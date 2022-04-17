using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSkript : MonoBehaviour
{

    [SerializeField] private SchuhSkript schuh;

    private void OnTriggerEnter2D(Collider2D col)
    {
        schuh.enabled = true; 
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        schuh.enabled = false; 
    }
}
