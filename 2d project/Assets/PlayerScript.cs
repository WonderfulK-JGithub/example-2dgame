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
    private Rigidbody2D rigidB;
    private BoxCollider2D boxC;
    private float vsp;
    private bool onGround;
    private Vector2 boxOrigin;
    // Start is called before the first frame update
    void Start()
    {
        rigidB = GetComponent<Rigidbody2D>();
        boxC = GetComponent<BoxCollider2D>();
        boxOrigin = new Vector2(transform.position.x + boxC.offset.x, transform.position.y + boxC.offset.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float left = Input.GetKey(KeyCode.A) ? 1f : 0f;
        float right = Input.GetKey(KeyCode.D) ? 1f : 0f;
        float up = Input.GetKey(KeyCode.W) ? 1f : 0f;
        float down = Input.GetKey(KeyCode.S) ? 1f : 0f;

        float hsp = (right - left) * spd * Time.deltaTime * 60f;
        float vsp = (up - down) * spd * Time.deltaTime * 60f;

       

        RaycastHit2D[] vCol = Physics2D.RaycastAll(transform.position, Vector2.down, 10f);




        Debug.Log(onGround);



        rigidB.velocity = new Vector2(hsp,vsp);
        
    }

    

}
