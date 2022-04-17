using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Variables")]

    [SerializeField] private float _length;
    [SerializeField] private float _startPos;
    [SerializeField] public float _parallaxEffect;

    public GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        //float temp = (cam.transform.position.x * (1 - _parallaxEffect));
        float distance = (cam.transform.position.x * _parallaxEffect);
        transform.position = new Vector3(_startPos + distance, transform.position.y, transform.position.z);

        //if (temp > _startPos + _length) _startPos += _length;
        //else if (temp < _startPos - _length) _startPos -= _length;

        
    }
}
