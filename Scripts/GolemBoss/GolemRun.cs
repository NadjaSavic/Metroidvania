using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemRun : StateMachineBehaviour
{
    public float speed;
    public float jumpRange = 15;
    Transform player;
    GameObject Golem;
    Rigidbody2D rb;
    GolemBoss boss;
    [SerializeField] private float range;
    [SerializeField] private float phase2Speed; 

    private bool kannSpringen; 
    

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Golem = GameObject.FindGameObjectWithTag("Golem");
        rb = Golem.GetComponent<Rigidbody2D>();
        boss = Golem.GetComponent<GolemBoss>();
        GolemBoss.instance.getAggro += MakeGolemFaster;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!animator.GetBool("isDead"))
        {
            boss.LookAtPlayer();

            Vector2 target = new Vector2(player.position.x, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
            
            if(kannSpringen)
            {
                if (Vector2.Distance(player.position, rb.position) <= jumpRange)
                {
                    animator.SetTrigger("Jump");
                }
            }
            else
            {
                if (Vector2.Distance(player.position, rb.position) <= range)
                {
                    animator.SetTrigger("Punch");
                }
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Jump");
        animator.ResetTrigger("Punch");
    }

    private void MakeGolemFaster()
    {
        speed = phase2Speed;
        kannSpringen = true; 
    }
}