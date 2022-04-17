using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Android;

public class SchuhSkript : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private float stompSpeed;
    [SerializeField] private float followSpeed;
    [SerializeField] private Transform stompPos; 

    [SerializeField] private float minGeschwindigkeit = 0.5f;


    [SerializeField] private Transform fußSohle;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask floorLayer;

    private Rigidbody2D rb;
    private Vector2 targetPosition; 
    private float hoeheY;
    private bool _isTouchingGround;
    private float geschwindigkeit = 0.5f;
    private bool _wantsToStomp;
    private bool _isStomping;
    private bool isRetrieving;
    private bool isFollowingPlayer = true;

    private Transform _stompStartPos; 

    void Start()
    {
        this.enabled = false;
        rb = player.GetComponent<Rigidbody2D>();
        _stompStartPos = stompPos;
        hoeheY = transform.position.y; 
    }

    // Update is called once per frame
    async void Update()
    {
        SetStates();
        SetPosition();
        Move(targetPosition, geschwindigkeit);
        stompPos.transform.position = new Vector2(stompPos.position.x, _stompStartPos.position.y);
    }

    private void SetPosition()
    {
        RaycastHit2D bodenUnterDenFueßen = Physics2D.Raycast(fußSohle.position, Vector2.down, rayDistance, floorLayer);
        if (isFollowingPlayer)
        {
            targetPosition = new Vector2(player.position.x, transform.position.y);
            geschwindigkeit = followSpeed;
        }
        else if (_isStomping)
        {
            targetPosition = new Vector2(transform.position.x,bodenUnterDenFueßen.point.y);
            geschwindigkeit = stompSpeed;
        }
        else if (isRetrieving)
        {
            targetPosition = new Vector2(transform.position.x, hoeheY);
            geschwindigkeit = followSpeed;
        }
    }


    private void Move(Vector2 position, float movSpeed)
    {
        transform.position = Vector2.Lerp(transform.position, position, movSpeed * Time.deltaTime);
    }

    private void SetStates()
    {
        
        RaycastHit2D bodenUnterDenFueßen = Physics2D.Raycast(fußSohle.position, Vector2.down, rayDistance, floorLayer);

        if (isFollowingPlayer && Mathf.Abs(rb.velocity.x) < minGeschwindigkeit)
        {
            isFollowingPlayer = false;
            _isStomping = true;
        }
        //&& Vector2.Distance(transform.position, bodenUnterDenFueßen.point) < 0.1f
        else if (_isStomping  &&
                 _isTouchingGround)
        {
            _isStomping = false;
            isRetrieving = true;
        }
        else if (isRetrieving && Vector2.Distance(transform.position, new Vector2(transform.position.x,hoeheY)) < 0.5f)
        {
            isRetrieving = false;
            isFollowingPlayer = true;
            _isTouchingGround = false;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(fußSohle.position, (Vector2) fußSohle.position + Vector2.down * rayDistance);

        RaycastHit2D bodenUnterDenFueßen = Physics2D.Raycast(fußSohle.position, Vector2.down, rayDistance, floorLayer);

        Gizmos.DrawWireSphere(bodenUnterDenFueßen.point, 0.5f);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            _isTouchingGround = true;
        }
        else if (col.gameObject.CompareTag("Player"))
        {
            player.GetComponent<Player>().Die();
        }
    }
}