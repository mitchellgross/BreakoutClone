using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    
    Vector2 clickPos;
    Vector3 shotDirection;
    float speed;
    
    void Start ()
    {
        clickPos = transform.position;
        speed = 8;

        clickPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)); //Converting mouse position to usable Vector3
        shotDirection = (clickPos - ((Vector2)transform.position)); //Gets the mouse's direction

        GetComponent<Rigidbody2D>().AddForce(shotDirection.normalized * speed, ForceMode2D.Impulse);
    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * speed; //ensures the velocity is always equal to the inital velocity
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Target")
        {
            Destroy(col.gameObject); //Destroy any GameObject with tag "Target" upon collision
        }
        if (col.gameObject.name == "Bottom")
        {
            GameObject.Find("Blocker").GetComponent<Blocker>().SendMessage("Failure"); //Call the "Failure" function in the "Blocker" script
        }
    }
}
