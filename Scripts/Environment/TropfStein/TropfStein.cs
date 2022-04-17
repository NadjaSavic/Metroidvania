using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TropfStein : MonoBehaviour
{
    [SerializeField] private GameObject tropfen;
    [SerializeField] private float spawnTime;
    [SerializeField] private Transform spawnPos;

    void Start()
    {
        StartCoroutine("Spawn");
    }
    IEnumerator Spawn()
    {
        GameObject trop = Instantiate(tropfen,spawnPos.position,Quaternion.Euler(0f,0f,90f));
        trop.transform.parent = spawnPos; 
        
        yield return new WaitForSeconds(spawnTime);
        
        StartCoroutine("Spawn");
    }
}