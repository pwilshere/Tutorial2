using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2D;
    public float speed;
    public float jump;
    public Text score;
    private int scoreValue = 0;
    public Text winText;
    public Text livesText;
    public int lives;

    public AudioSource sound;
    public AudioClip backgroundMusic;
    public AudioClip winMusic;

    public Animator anim;
    private bool faceRight = true;

    public float coinDelay = 0.25f; //bad temp fix for double coin pickups
    public float timeSinceCoin = 0;

    // Start is called before the first frame update
    void Start()
    {
        rd2D = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        winText.text = "";
        livesText.text = "Lives: " + lives.ToString();
        sound = GetComponent<AudioSource>();
        sound.clip = backgroundMusic;
        sound.loop = true;
        sound.Play();
        anim = GetComponent<Animator>();
        //animation.clip = idle;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //controls
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2D.AddForce(new Vector2(hozMovement * speed,verMovement));
        
        if (Input.GetKey("escape"))
        {
          Application.Quit();
        }
        //animation state
        if (!IsGrounded())
        {
            anim.SetInteger("State", 2);
        }else if (hozMovement != 0)
        {
            anim.SetInteger("State", 1);
        } else
        {
            anim.SetInteger("State", 0);
        }
        //facing check
        if (hozMovement < 0 && faceRight == true)
        {
            transform.localScale *= new Vector2 (-1,1);
            faceRight = false;
        } else if (hozMovement > 0 && faceRight == false)
        {
            transform.localScale *= new Vector2 (-1,1);
            faceRight = true;
        }
        //jumping
        if (IsGrounded())
        {
            if (Input.GetKey(KeyCode.W)){
                    rd2D.AddForce(new Vector2(0,jump), ForceMode2D.Impulse);
                }
        }
        timeSinceCoin += Time.deltaTime;
    }

    private bool IsGrounded()
    {
        return transform.Find("GroundCheck").GetComponent<GroundCheckScript>().isGrounded;
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin" && timeSinceCoin > coinDelay)
        {
            Destroy(collision.gameObject);
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            timeSinceCoin = 0;
            if (scoreValue == 4)
            {
                transform.position = new Vector2(25,1);
            } else if (scoreValue >= 8)
            {
                winText.text = "You Win!\nGame Created by Patrick Wilshere";
                sound.clip = winMusic;
                sound.loop = false;
                sound.Play();
            }
        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            lives -= 1;
            livesText.text = "Lives: " + lives.ToString();
            Destroy(collision.gameObject);
        }
        if (lives <= 0)
        {
            winText.text = "You Lose!\nGame Over";
            GetComponent<SpriteRenderer>().enabled = false;
            speed = 0;
            jump = 0;
            sound.Stop();
        }
    }
}
