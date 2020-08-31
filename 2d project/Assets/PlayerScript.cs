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

        float hsp = (right - left) * spd * Time.deltaTime * 60f;

        
        
        var vCol = Physics2D.Raycast(boxOrigin,Vector2.down,10f);

        if (vCol.collider != null && vCol.collider.tag == "Wall")
        {
            onGround = true;
        }
        else onGround = false;

        Debug.Log(vCol.collider);

        if (onGround)
        {
            vsp = 0;
        }
        else
        {
            vsp -= gravity;
        }
        
        

        rigidB.velocity = new Vector3(hsp,vsp,0f);

        
    }
}
