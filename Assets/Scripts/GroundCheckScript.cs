using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckScript : MonoBehaviour
{
    public bool isGrounded;
    public float bounce;
    private void OnTriggerStay2D (Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            isGrounded = true;
            Destroy(collision.gameObject);
            GetComponentInParent<Rigidbody2D>().velocity = new Vector2(GetComponentInParent<Rigidbody2D>().velocity.x,bounce);
        }
    }
    private void OnTriggerExit2D (Collider2D collision)
    {
        isGrounded = false;
    }
}
