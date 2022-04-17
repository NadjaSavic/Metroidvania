using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour
{

    private float length, startpos;
    [SerializeField] GameObject cam;
    [SerializeField]  float parralexeffect; 
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponentInChildren<SpriteRenderer>().bounds.size.x;
        
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parralexeffect));
        float dist = (cam.transform.position.x * parralexeffect);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if (temp > startpos + length) startpos += length; 
        else if (temp < startpos - length) startpos -= length; 
    }
}
