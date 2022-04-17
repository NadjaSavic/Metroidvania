using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private float interval;
    [SerializeField] private GameObject ball;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private Transform ballHolder;
    [SerializeField] private float shootForce;
    [SerializeField] private Transform shootDirection;
    [SerializeField] private ParticleSystem shoot; 
    void Start()
    {
        StartCoroutine(ShootRepeatedly());
    }


    private IEnumerator ShootRepeatedly()
    {
        while (true)
        {
            ShootBullet();
            yield return new WaitForSeconds(interval);
        }
    }


    private void ShootBullet()
    {
        GameObject inst = Instantiate(ball, spawnPos.position, Quaternion.identity,ballHolder);
        inst.GetComponentInChildren<Rigidbody2D>().AddForce((shootDirection.position-spawnPos.position)*shootForce,ForceMode2D.Force);
        shoot.Play();
    }
}