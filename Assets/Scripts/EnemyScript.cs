using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float speed;
    private int facing = 1; //1 = right
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (HitSomething())
        {
            facing *= -1;
            transform.localScale *= new Vector2(-1,1);
        }
        rb.velocity = new Vector2(speed*facing,rb.velocity.y);
    }
    private bool HitSomething()
    {
        return transform.Find("FaceCheck").GetComponent<FaceCheckScript>().hit;
    }
}
