using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEditor;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Transform schwertHalter;
    [SerializeField] private float speed;
    [SerializeField] private float maxDistance;
    [SerializeField] private float attackSpeed;
    [SerializeField] private int damage = 5;
    [SerializeField] private float bufferTime;

    private bool _isAttacking;
    private Vector2 mousePos;
    private bool _canAttack = true;

    public GameObject swordImpactEffekt;

    void Start()
    {
    }

    void Update()
    {
        if (schwertHalter == null) return;
        checkInput();

        if (!_isAttacking) followPlayer();

        else if (_isAttacking) attack();
    }

    private void checkInput()
    {
        if (_canAttack)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isAttacking = true;
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isAttacking = false;
            }
        }
    }

    private void followPlayer()
    {
        transform.position =
            UnityEngine.Vector3.Lerp(transform.position, schwertHalter.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }


    private void attack()
    {
        Vector2 _position = transform.position;
        if (Vector2.Distance(transform.position, schwertHalter.position) < maxDistance)
        {
            Vector2 attackDirection = Vector2.ClampMagnitude(mousePos - _position, maxDistance);

            transform.position = Vector2.Lerp(_position, _position + attackDirection, attackSpeed * Time.deltaTime);
        }

        else
        {
            _isAttacking = false;
        }

        StartCoroutine(AttackTimer());
    }
    
    IEnumerator AttackTimer()
    {
        _canAttack = false;
        yield return new WaitForSeconds(bufferTime);
        _canAttack = true; 
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (_isAttacking && col.CompareTag("Golem"))
        {
            GolemBoss.instance.TakeDamage(damage);
            GameObject effectIns = (GameObject)Instantiate(swordImpactEffekt, transform.position, transform.rotation);
            Destroy(effectIns, 1f);
        }
        else if (_isAttacking&&col.CompareTag("Rock"))
        {
            col.GetComponent<RockCollision>().DestroyRock();
        }
    }
}