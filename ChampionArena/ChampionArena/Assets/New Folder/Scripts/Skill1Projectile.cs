using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill1Projectile : MonoBehaviour {
    private Rigidbody2D myRigidBody;
    //projectile speed
    public float speed;

	// Use this for initialization
	void Start () {
    myRigidBody = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
   
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
      
    }
}
