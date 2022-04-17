using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] public int numOfHearts;

    [SerializeField] public Image[] hearts;
    [SerializeField] public Sprite fullHeart;
    [SerializeField] public Sprite emptyHeart;
    [SerializeField] private Player player;
    [SerializeField] private float damageBuffer;

    private bool canGetDamaged = true;


    void Update()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "RockThrow" || collision.gameObject.tag == "Rock" ||
            collision.gameObject.tag == "Golem")
        {
            getDamage();
            StartCoroutine(Timer());
        }
    }

    public void getDamage()
    {
        if (canGetDamaged)
        {
            health -= 1;
            StartCoroutine(Timer());
        }

        if (health <= 0f)
        {
            player.Die();
            Debug.Log("tot");
        }
    }


    private IEnumerator Timer()
    {
        canGetDamaged = false;
        yield return new WaitForSeconds(damageBuffer);
        canGetDamaged = true;
    }
}