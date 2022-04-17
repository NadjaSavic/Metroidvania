using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCollision : MonoBehaviour
{
    public GameObject impactEffect;

    public void DestroyRock()
    {
          GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
                    Destroy(effectIns, 1f);
                    Destroy(this.gameObject);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Wall")
        {
          DestroyRock();
        }


    }
}
