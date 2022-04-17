using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrow : StateMachineBehaviour
{   
    
    public Transform rockSpawn;
    public Transform player;
    public GameObject rock;
    public GameObject rockInMap;
    public Rigidbody2D rb;
    public float speed = 10f;
    public GameObject Golem;
    GolemBoss boss;
    

    public float timeRemaining = 0.6f;
    
        

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
            rockSpawn = GameObject.FindGameObjectWithTag("RockSpawn").transform;
            player = GameObject.FindGameObjectWithTag("Player").transform;
            Golem = GameObject.FindGameObjectWithTag("Golem");
            boss = Golem.GetComponent<GolemBoss>();



            if (!GameObject.FindGameObjectWithTag("RockThrow"))
            {
                Instantiate(rock, rockSpawn.position, Quaternion.identity);
            }

            rockInMap = GameObject.FindGameObjectWithTag("RockThrow");
            rb = rockInMap.GetComponent<Rigidbody2D>();


            rb.gravityScale = 0;
            timeRemaining = 0.6f;


        if (!animator.GetBool("isDead"))
        {
            boss.GetComponent<MonoBehaviour>().StartCoroutine(ExampleCoroutine());
        }


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!animator.GetBool("isDead"))
        {
            boss.LookAtPlayer();
        }
        
        

    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    void MoveRock()
    {
        if (GameObject.FindGameObjectWithTag("RockThrow"))
        {
            rb.gravityScale = 1;
            //Vector2 aimPosition = player.transform.position - rockSpawn.position;
            /*
            if (!boss.isFlipped)
            {
                rb.AddForce(aimPosition.normalized * speed, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(aimPosition.normalized * speed, ForceMode2D.Impulse);
            }
            */
            if (!boss.isFlipped)
            {
                rb.AddForce(Vector2.left * speed, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(Vector2.right * speed, ForceMode2D.Impulse);
            }

        }
        
    }

    IEnumerator ExampleCoroutine()
    {
        

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(0.6f);
        
        MoveRock();
        
    }


}
