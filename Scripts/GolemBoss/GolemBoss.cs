using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GolemBoss : MonoBehaviour
{
    [Header("RockFalling")] public GameObject rock;

    public float timeRemaining = 0.2f;

    [SerializeField] private Transform grenzeLinks;
    [SerializeField] private Transform grenzeRechts;

    private float rand; 


    public bool inZweiterPhase;

    [Header("GolemStuff")] public static GolemBoss instance;
    public event Action getAggro;
    public bool isFlipped = false;
    public Transform player;


    [SerializeField] private Animator anim;
    [SerializeField] private Slider slider;
    [SerializeField] private int health;
    
    
    
    

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else instance = this;
    }

    private void Start()
    {
        slider.value = health;
    }

    private void Update()
    {
        if (!anim.GetBool("isDead") && inZweiterPhase)
        {
            rand = UnityEngine.Random.Range(grenzeLinks.position.x, grenzeRechts.position.x);

            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                Instantiate(rock, new Vector2(rand, grenzeLinks.position.y), Quaternion.identity);
                timeRemaining = 0.2f;
            }
        }
    }


    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void TakeDamage(int damage)
    {
        if (health > 0f)
        {
            health -= damage;
        }
        else anim.Play("GolemDying");
            
        if (health <= 50&&!inZweiterPhase) 
        {
            getAggro?.Invoke();
            anim.Play("GolemRoar");
            inZweiterPhase = true; 
        }
        if(health <= 0f)
        {
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY;
            gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
            inZweiterPhase = false;
            StartCoroutine(WaitBeforeDeath());
        }
        slider.value = health;
    }

    IEnumerator WaitBeforeDeath()
    {
        anim.Play("GolemDying");
        yield return new WaitForSeconds(4f);
        GameManager.Instance.LoadNextScene(3);
    }
}