using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Movement : MonoBehaviour {
    //character basic movements
    private Rigidbody2D myRigidBody;
    public float Speed;
    public float JumpPower;
    private bool FacingRight = false;
    private float MoveX;
    private bool isGrounded;
    //character auto attack and skills
    private bool autoAttack =false;
    private bool autoAttackCD = false;
    private float autoTime =0.5f;
    private float autoTimeCounter;
    //skill1
    private bool skill1 = false;
    private bool skill1CD = false;
    private float skill1Time = 5.0f;
    private float skill1TimeCounter;
    public Transform firePoint;
    public GameObject skill1Projectile;
    //skill2
    private bool skill2 = false;
    private bool skill2CD = false;
    private float skill2Time = 5.0f;
    private float skill2TimeCounter;
    private float skill2DashPower = 1000f;

    private Animator anim;


    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        PlayerMove();
        anim.SetFloat("MovementX", Input.GetAxisRaw("Horizontal"));
        PlayerAttack();
        
    }
    //class that controls character movement______________________________________________________
    void PlayerMove()
    {
        MoveX = Input.GetAxis("Horizontal");
        if (autoAttack == false)
        {
            //can only jump if on ground
            if (Input.GetButtonDown("Jump") && isGrounded == true)
            {
                Jump();
            }
            //make character face correct direction
            if (MoveX < 0.0f && FacingRight == false && skill1==false)
            {
                FlipPlayer();
            }
            else if (MoveX > 0.0f && FacingRight == true && skill1==false)
            {
                FlipPlayer();
            }
            //if character is on ground and not using skills then allow movement
            if (isGrounded == true && skill1==false)
            {
                anim.SetBool("InAir", false);
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(MoveX * Speed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
            }
        }
    }
    void Jump() {
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * JumpPower);
        anim.SetBool("InAir", true);
        
        isGrounded = false;
    }
    void FlipPlayer() {
        FacingRight = !FacingRight;
        Vector2 localScale = gameObject.transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    //class that controls character attacks and skills
    void PlayerAttack() {
        //auto attack (ground)___________________________________________________________________
        if (Input.GetButtonDown("Fire1") && autoAttackCD == false && isGrounded == true)
        {
            autoTimeCounter = autoTime;
            autoAttack = true;
            autoAttackCD = true;
            //cant move while auto attacking
            myRigidBody.velocity = Vector2.zero;
            anim.SetBool("Auto", true);
        }
        //auto attack (air)
        if (Input.GetButtonDown("Fire1") && autoAttackCD == false && isGrounded == false) {
            autoTimeCounter = autoTime;
            autoAttack = true;
            autoAttackCD = true;
            //can move while auto attacking air
            
            anim.SetBool("Auto", true);
        }

        if (autoTimeCounter > 0) {
            autoTimeCounter -= Time.deltaTime;
        }
        if (autoTimeCounter <= 0) {
            autoAttack = false;
            autoAttackCD = false;
            anim.SetBool("Auto", false);
        }
        //Skill1_______________________________________________________________________________________
        if (Input.GetButtonDown("Fire2") && isGrounded == true && skill1CD == false) {
            skill1TimeCounter = skill1Time;
            skill1 = true;
            skill1CD = true;
            anim.SetBool("Skill1", true);
            GameObject ShotProjectile= Instantiate(skill1Projectile, firePoint.position, firePoint.rotation) as GameObject;
            GameObject.Destroy(ShotProjectile.gameObject, 0.5f);// destroy gameObject that script is attached to
        }
        if (skill1TimeCounter > 0)
        {
            skill1TimeCounter -= Time.deltaTime;
        }
        //skill1 animation and skill1 itself ends
        if (skill1TimeCounter < 4.65)
        {
            anim.SetBool("Skill1", false);
            skill1 = false;
        }
        //skill1 cd ends
        if (skill1TimeCounter <= 0)
        {
            skill1CD = false;
            
        }
        //Skill2____________________________________________________________________________________
        if (Input.GetButtonDown("Fire3")  && skill2CD == false && isGrounded==false) {
            skill2TimeCounter = skill2Time;
            skill2 = true;
            skill2CD = true;
            anim.SetBool("Skill2", true);
            if (FacingRight == false)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.right * skill2DashPower);
            }
            if (FacingRight == true)
            {
                GetComponent<Rigidbody2D>().AddForce(Vector2.left * skill2DashPower);
            }
        }
        if (skill2TimeCounter > 0)
        {
            skill2TimeCounter -= Time.deltaTime;
        }
        //skill2 animation and skill2 itself ends
        if (skill2TimeCounter < 4.65)
        {
            skill2 = false;
            anim.SetBool("Skill2", false);
        }
        //skill2 cd ends
        if (skill2TimeCounter <= 0)
        {
           
            skill2CD = false;

        }
    }
    //detect if character is on ground with collider
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground") {
            isGrounded = true;
        }

    }
}
