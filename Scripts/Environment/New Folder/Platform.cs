using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            col.collider.transform.SetParent(gameObject.transform);
        }
    }
    
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            col.collider.transform.SetParent(null);
        }
    }
}
