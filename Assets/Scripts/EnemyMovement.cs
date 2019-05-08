using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	[SerializeField] float moveSpeed = 1f;
	Rigidbody2D myRigidBody;
	SpriteRenderer mySpriteRenderer;


	// Use this for initialization
	void Start () {

		myRigidBody = GetComponent<Rigidbody2D>();
		mySpriteRenderer = GetComponent<SpriteRenderer>();

	}

	private void OnTriggerExit2D(Collider2D collision) {
		mySpriteRenderer.flipX = true;
		//print ("flipped");
		//transform.localScale = new Vector2 (-(Mathf.Sign (myRigidBody.velocity.x)), 1f);
	}

	// Update is called once per frame
	void Update () {

		if (mySpriteRenderer.flipX) {
			myRigidBody.velocity = new Vector2 (moveSpeed, 0f);
		}else {
			myRigidBody.velocity = new Vector2 (-moveSpeed, 0f);
		}

	}

	private bool IsFacingLeft() {
	
		return transform.localScale.x > 0;

	}


}
