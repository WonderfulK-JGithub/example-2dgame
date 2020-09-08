using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.Experimental.GraphView;
using UnityEditor.U2D.Path;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{
    public float spd;
    public float gravity;
    public float jumpForce;
    public float groundDistance;

    public Transform attackPoint;
    public float attackRange;
    public LayerMask enemyLayers;
    public LayerMask groundLayers;

    private Rigidbody2D rigidB;
    private bool onGround = false;
    
    private float hsp;
    private float vsp;
    private float jumpTime;


    public enum PlayerState
    {
        free,
        attack
    }

    public PlayerState state;
    // Start is called before the first frame update
    void Start()
    {
        rigidB = GetComponent<Rigidbody2D>();
        
        state = PlayerState.free;
        
    }

    // Update is called once per frame
    void Update()
    {
        float left = 0;
        float right = 0;

        if (Physics2D.Raycast((Vector2)transform.position - Vector2.left, Vector2.down, groundDistance,groundLayers) || Physics2D.Raycast((Vector2)transform.position - Vector2.right, Vector2.down, groundDistance, groundLayers))
        {
            onGround = true;
        }
        else onGround = false;

        switch (state)
        {
            case PlayerState.free:
            {
                left = Input.GetKey(KeyCode.A) ? 1f : 0f;
                right = Input.GetKey(KeyCode.D) ? 1f : 0f;

                if(onGround && Input.GetKeyDown(KeyCode.Space))
                {
                    vsp = jumpForce;
                    onGround = false;
                    jumpTime = 15f;
                    
                }
                
                if(Input.GetKeyDown(KeyCode.LeftShift))
                {
                    state = PlayerState.attack;
                }
                break;
            }
            case PlayerState.attack:
            {
                    Debug.Log("attacked");
                Collider2D[] enemyHits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

                    

                foreach(Collider2D enemy in enemyHits)
                {
                    enemy.GetComponent<EnemySlime>().Damage();
                }

                if (!onGround)
                {
                    left = Input.GetKey(KeyCode.A) ? 1f : 0f;
                    right = Input.GetKey(KeyCode.D) ? 1f : 0f;
                }
               

                
                break;
            }

        }

        

        hsp = (right - left) * spd * Time.deltaTime;
        if (!onGround)
        {
            vsp -= gravity * Time.deltaTime;
        }
        else vsp = 0;

        if (jumpTime > 0f)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                jumpTime -= 60f * Time.deltaTime;
                vsp = jumpForce;
            }
            else jumpTime = 0f;

        }
    }
    void FixedUpdate()
    {
        //rigidB.velocity = new Vector2(hsp,vsp);
        rigidB.MovePosition((Vector2)transform.position + new Vector2(hsp, vsp));
    }

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
