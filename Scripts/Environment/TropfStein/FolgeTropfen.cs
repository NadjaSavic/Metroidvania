using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FolgeTropfen : MonoBehaviour
{
    [SerializeField] private Transform _pos;
    void Update()
    {
        if(_pos!=null) transform.position = _pos.position; 
    }
}
