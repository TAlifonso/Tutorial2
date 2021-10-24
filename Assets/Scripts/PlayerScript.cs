using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    private float fixedDeltaTime;

    public Text score;
    private int scoreValue = 0;

    public GameObject win;
    public GameObject Player;
    public GameObject lose;
    public GameObject scene;

    public Text life;
    private int lifeValue = 3;

    private bool facingRight = true;

    Animator anim;

    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;

    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;


    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();

        score.text = "Score: " + scoreValue.ToString();
        win.gameObject.SetActive(false);
        lose.gameObject.SetActive(false);
        life.text = "Lives: " + lifeValue.ToString();

        anim = GetComponent<Animator>();

        musicSource.clip = musicClipOne;
        musicSource.Play();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);
        
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if(scoreValue == 4 && collision.collider.tag == "Coin")
        {
            transform.position = new Vector2(55, 1);
            lifeValue = 3;
            life.text = "Lives: " + lifeValue;
        }

        if(collision.collider.tag == "Enemy")
        {
            lifeValue -= 1;
            life.text = "Lives: " + lifeValue.ToString();
            Destroy(collision.collider.gameObject);
        }

        if(scoreValue == 8 && collision.collider.tag == "Coin")
        {
            win.gameObject.SetActive(true);
            rd2d.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            anim.SetInteger("State", 0);
            musicSource.Stop();
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            
        }
        else if(lifeValue == 0 && scoreValue <= 7)
        {
            lose.gameObject.SetActive(true);
            Destroy(Player);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground" && isOnGround)
        {
            
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 4), ForceMode2D.Impulse);
                anim.SetBool("IsJumping", true);
                anim.SetInteger("State", 2);
            }
            else if(collision.collider.tag == "Ground" && isOnGround)
            {
                anim.SetBool("IsJumping", false);
            }
            
        }
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }   

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }


}
