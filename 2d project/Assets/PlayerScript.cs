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
    private Rigidbody2D rigidB;
    private BoxCollider2D boxC;
    private bool onGround = false;
    
    private float hsp;
    private float vsp;
    private float jumpTime;

    // Start is called before the first frame update
    void Start()
    {
        rigidB = GetComponent<Rigidbody2D>();
        boxC = GetComponent<BoxCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float left = Input.GetKey(KeyCode.A) ? 1f : 0f;
        float right = Input.GetKey(KeyCode.D) ? 1f : 0f;
        

        hsp = (right - left) * spd * Time.deltaTime;



        if (!onGround)
        {
            vsp -= gravity * Time.deltaTime;
        }
        else
        {
            vsp = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                vsp = jumpForce;
                onGround = false;
                jumpTime = 15f;
            }
        }
        if(jumpTime > 0f)
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground") onGround = true;
    }
    


}
