using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    
    public static UIManager Instance;
    [SerializeField] private Canvas canvas;
    private bool canvasIsOn; 

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
        canvas.enabled = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !canvasIsOn)
        {
            Time.timeScale = 0;
            canvas.enabled = true;
            canvasIsOn = true;
        }
        

        else if (Input.GetKeyDown(KeyCode.Escape) && canvasIsOn)
        {
            Time.timeScale = 1;
            canvas.enabled = false;
            canvasIsOn = false;
        }

    }
}
