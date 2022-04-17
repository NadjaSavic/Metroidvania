using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMove : MonoBehaviour
{
    public Movement move;

    private Transform offset;
    private Transform offsetAttack;
    private Transform player;
    

    public float moveSpeed = 10f;
    public float comeBackSpeed = 20;
    public float attackSpeed = 50f;
    public float rotationSpeed = 2f;
    public float rotation = -270f;

    public float min = 5f;
    public float max = 5f;

    public float timeRemaining = 10;

    public bool isAttacking = false;

    // Start is called before the first frame update
    void Start()
    {

        move.GetComponent<Movement>();
        offset = GameObject.FindGameObjectWithTag("Offset").GetComponent<Transform>();
        offsetAttack = GameObject.FindGameObjectWithTag("OffsetAttack").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.x = Mathf.Clamp(mousePos.x, player.position.x - min, player.position.x + max );
        mousePos.y = Mathf.Clamp(mousePos.y, player.position.y- min, player.position.y + max);
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float angle = Mathf.Atan2(difference.x, difference.y) * Mathf.Rad2Deg;

        float direction;
        

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Quaternion _targetRotation = Quaternion.Euler(0f, 0f, angle * -1);

            //timeRemaining -= Time.deltaTime;
            transform.rotation = Quaternion.Euler(0f, 0f, angle * -1);
            transform.position = Vector2.Lerp(transform.position, mousePos, attackSpeed * Time.deltaTime);
            //transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, (angle * -1) * rotationSpeed * Time.deltaTime );
            
        }
        else
        {



            transform.position = Vector2.MoveTowards(transform.position, offset.position, comeBackSpeed * Time.deltaTime);

            
            if (transform.position == offset.position)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                
            }
            

        }
        
        
      
        
        

    }

    

    


}
