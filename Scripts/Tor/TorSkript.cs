using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TorSkript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI trittEin;
    [SerializeField] private TextMeshProUGUI esFehltDerSchlüssel;

    private void Start()
    {
        trittEin.enabled = false;
        esFehltDerSchlüssel.enabled = false; 
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E) && !GameManager.Instance.KeyIstVollständig)
            {
                trittEin.enabled = false;
                esFehltDerSchlüssel.enabled = true; 
            }
            
            else if (Input.GetKey(KeyCode.E) && GameManager.Instance.KeyIstVollständig)
            {
                GameManager.Instance.LoadNextScene(2);
            }
            else
            {
                trittEin.enabled = true;
                esFehltDerSchlüssel.enabled = false;
            } 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        trittEin.enabled = false;
        esFehltDerSchlüssel.enabled = false; 
    }
}

