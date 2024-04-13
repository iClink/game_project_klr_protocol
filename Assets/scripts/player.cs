using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float speed;
    public float jumpforce;

    public bool isjumping;
    public bool doublejump;

    private Rigidbody2D rig;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
    }

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f,0f);
        transform.position += movement * Time.deltaTime * speed;

        if(Input.GetAxis("Horizontal") > 0f)
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f,0f,0f);
        }

        if(Input.GetAxis("Horizontal") < 0f)
        {
            transform.eulerAngles = new Vector3(0f,180f,0f);
            anim.SetBool("walk", true);
        }

        if(Input.GetAxis("Horizontal") == 0f)
        {
            anim.SetBool("walk", false);
        }

    }

    void Jump()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(isjumping == false)
        {
            rig.AddForce(new Vector2(0f, jumpforce), ForceMode2D.Impulse);
            doublejump = true;
            anim.SetBool("jump", true);
        }
        else
        {
            if(doublejump)
            {
                rig.AddForce(new Vector2(0f, jumpforce), ForceMode2D.Impulse);
                doublejump = false;
            }
        }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isjumping = false;
            anim.SetBool("jump", false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            isjumping = true;  
        }
    }
}
