using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemJump : StateMachineBehaviour
{
    
    public float jumpHeight = 10f;
    public Transform player;
    public GameObject Golem;
    public Rigidbody2D rb;
    public float speed = 5f;

    [SerializeField] private float _airLinearDrag = 2.5f;
    [SerializeField] private float _fallMultiplier = 8f;
    [SerializeField] private float _lowJumpFallMultiplier = 5f;

    private bool zweitePhaseEnter; 

    GolemBoss boss;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Golem = GameObject.FindGameObjectWithTag("Golem");
        rb = Golem.GetComponent<Rigidbody2D>();
        boss = Golem.GetComponent<GolemBoss>();
        float offset;
        GolemBoss.instance.getAggro += EnterZweitePhase; 


        if (!animator.GetBool("isDead"))
        {
            Debug.Log("test2");

            if (boss.isFlipped)
            {
                offset = 30;

            }
            else
            {
                offset = -30;
            }
            rb.velocity = new Vector2(rb.velocity.x + offset,0f);
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!animator.GetBool("isDead"))
        {
            Debug.Log("test");
            boss.LookAtPlayer();
            ApplyAirLinearDrag();
            FallMultiplier();
        }
    }
    
    private void ApplyAirLinearDrag()
    {
        rb.drag = _airLinearDrag;
    }
    private void FallMultiplier()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = _fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = _lowJumpFallMultiplier;
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }

    private void EnterZweitePhase()
    {
        zweitePhaseEnter = true; 
    }
}
