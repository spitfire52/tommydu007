using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1Projectile : MonoBehaviour {
    private Rigidbody2D myRigidBody;
    //projectile speed
    public float speed;

    public Character_Movement player;

	// Use this for initialization
	void Start () {
    //myRigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Character_Movement>();
        if (player.transform.localScale.x < 0) {
            speed = -speed;
        }

    }
	
	// Update is called once per frame
	void Update () {

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
   
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Destroy(gameObject);             //currently needs fixing since it collides with player and 
                                           //skill1 projectile disappear, need to chenge so it only disappear 
                                           //when hitting terrian or enemy.
            
    }
}
