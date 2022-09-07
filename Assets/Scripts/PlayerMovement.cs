using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float speedMultiplier;
    private bool canDash = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        speedMultiplier = 1;
    }


    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * Mathf.RoundToInt(Input.GetAxis("Horizontal")));

        //Resets Position if Out of Play Area
        if (Vector2.Distance(this.transform.position, Vector2.zero) > 7)
        {
            this.transform.position = Vector2.zero;
        }

        //Movement
        rb.velocity = Vector2.zero;

        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed * speedMultiplier * Time.deltaTime * 100, rb.velocity.y);
        
        rb.velocity = new Vector2(rb.velocity.x, Input.GetAxis("Vertical") * speed * speedMultiplier * Time.deltaTime * 100);

        //Reset Speed Boost
        
        //Speed Boosts
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            speedMultiplier = 2.5f;
            //Wall Dash
            if (hit.collider.gameObject.CompareTag("Wall") && canDash)
            {
                Debug.Log(hit.collider.gameObject.name);

                StartCoroutine(TurnWallOn(hit.collider.GetComponent<BoxCollider2D>()));
            }
        }
       
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            speedMultiplier = 1;
        }
            
        

    }

    //Turns Wall Back On
    IEnumerator TurnWallOn(BoxCollider2D col)
    {
        canDash = false;    
        col.enabled = false;
        
        yield return new WaitForSeconds(0.5f);
        col.enabled = true;
        
        canDash = true;
    }
}

