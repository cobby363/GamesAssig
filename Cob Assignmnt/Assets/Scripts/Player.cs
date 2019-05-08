using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

	[SerializeField] float runSpeed = 5f;
	[SerializeField] float jumpSpeed = 5f;
	[SerializeField] float climbSpeed = 5f;
	[SerializeField] Vector2 deathKick = new Vector2 (25f, 25f);

	bool isAlive = true;

	Rigidbody2D myRigidBody;
	Animator myAnimator;
	CapsuleCollider2D myBodyCollider2D;
	BoxCollider2D myFeetCollider2D;
	SpriteRenderer mySpriteRenderer;
	float gravityScaleAtStart;

	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator> ();
		myBodyCollider2D = GetComponent<CapsuleCollider2D> ();
		myFeetCollider2D = GetComponent<BoxCollider2D> ();
		mySpriteRenderer = GetComponent<SpriteRenderer>();
		gravityScaleAtStart = myRigidBody.gravityScale;
	}

	// Update is called once per frame
	void Update () {

		if (!isAlive) { 
			return;
		}

		Run ();
		FlipSprite ();
		Jump ();
		ClimbLadder ();
		Die ();
	}

	private void Run() {

		float movement = CrossPlatformInputManager.GetAxis ("Horizontal"); //value is between -1 to +1
		Vector2 playerVelocity = new Vector2(movement * runSpeed, myRigidBody.velocity.y);
		myRigidBody.velocity = playerVelocity;
		// print (playerVelocity);

		bool playerHasHorizontalSpeed = Mathf.Abs (myRigidBody.velocity.x) > 0;
		myAnimator.SetBool ("Running", playerHasHorizontalSpeed);

	}

	private void ClimbLadder() {

		if (!myFeetCollider2D.IsTouchingLayers (LayerMask.GetMask ("Climbing"))) {

			myAnimator.SetBool ("Climbing", false);
			myRigidBody.gravityScale = gravityScaleAtStart;
			return;
		}

		float movement = CrossPlatformInputManager.GetAxis ("Vertical"); //value is between -1 to +1
		Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, movement * climbSpeed);
		myRigidBody.velocity = climbVelocity;
		myRigidBody.gravityScale = 0f;

		bool playerHasVerticalSpeed = Mathf.Abs (myRigidBody.velocity.y) > 0;
		myAnimator.SetBool ("Climbing", playerHasVerticalSpeed);

	}

	private void Jump() {

		if (!myFeetCollider2D.IsTouchingLayers (LayerMask.GetMask ("Ground"))) {

			return;
		}

		if (CrossPlatformInputManager.GetButtonDown("Jump")) {

			Vector2 jumpVelocityToAdd = new Vector2 (0f, jumpSpeed);
			myRigidBody.velocity += jumpVelocityToAdd;

		}

	}

	private void Die(){

		if (myBodyCollider2D.IsTouchingLayers (LayerMask.GetMask ("Enemy", "Hazards"))) {

			myAnimator.SetTrigger ("Dying");
			myRigidBody.velocity = deathKick;
			isAlive = false;
			FindObjectOfType<GameSession> ().ProcessPlayerDeath ();

		}

	}

	private void FlipSprite() {

		// if the A key was pressed this frame
		if(Input.GetKeyDown(KeyCode.A))
		{
			// if the variable isn't empty (we have a reference to our SpriteRenderer
			if(mySpriteRenderer != null)
			{
				// flip the sprite
				mySpriteRenderer.flipX = true;
			}
		}

		if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			// if the variable isn't empty (we have a reference to our SpriteRenderer
			if(mySpriteRenderer != null)
			{
				// flip the sprite
				mySpriteRenderer.flipX = true;
			}
		}

		if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			// if the variable isn't empty (we have a reference to our SpriteRenderer
			if(mySpriteRenderer != null)
			{
				// flip the sprite
				mySpriteRenderer.flipX = false;
			}
		}

		if(Input.GetKeyDown(KeyCode.D))
		{
			// if the variable isn't empty (we have a reference to our SpriteRenderer
			if(mySpriteRenderer != null)
			{
				// flip the sprite
				mySpriteRenderer.flipX = false;
			}
		}

		//bool playerHasHorizontalSpeed = Mathf.Abs (myRigidBody.velocity.x) > 0;  //true if player has velocity
		//if (playerHasHorizontalSpeed) {  // if the player is moving
			//SpriteRenderer.flipX = true;
		//	transform.localScale = new Vector2 (Mathf.Sign (myRigidBody.velocity.x), 1f);  // get sign of velocity, apply it to x scale value
		}
	}

